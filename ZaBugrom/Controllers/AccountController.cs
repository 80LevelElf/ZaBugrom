using System;
using System.IO;
using System.Web.Helpers;
using System.Web.Mvc;
using CommonDAL.Managers;
using EmitMapper;
using Models.Data;
using Models.Data.Settings;
using Models.InputModels.Account;
using WebMatrix.WebData;
using ZaBugrom.Managers;

namespace ZaBugrom.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            //If user is auth - log out
            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.IsAccountLockedOut(model.Name, 6, 3600))
                {
                    ModelState.AddModelError("Password",
                       "Вы пытались ввести пароль слишком много раз. Вам придеться подождать час, прежде чем попытаться войти снова.");
                }

                if (WebSecurity.Login(model.Name, model.Password, true))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Вы ввели неправильный логин или пароль!");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegistrationInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (WebSecurity.UserExists(model.Name))
            {
                ModelState.AddModelError("Name", "Пользователь с таким логином уже существует!");
            }

            if (RepositoryManager.UserRepository.IsEmailExist(model.Email))
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже существует!");
            }

            //To show new errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            WebSecurity.CreateUserAndAccount(model.Name, model.Password, new 
            {
                Email = model.Email,
                AddTime = DateTime.Now
            });
            WebSecurity.Login(model.Name, model.Password, true);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfileSettings()
        {
            var userData = UserManager.GetCurrentUser();
            var model = ObjectMapperManager.DefaultInstance.GetMapper<UserData, ProfileSettingsInputModel>().Map(userData);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfileSettings(ProfileSettingsInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userData = UserManager.GetCurrentUser();

            //We change settings only if everything is ok
            bool isContinue = true;
            bool isNeedToChangePassword = false;

            //If password change
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                if (UserManager.MembershipProvider.ValidateUser(userData.Name, model.OldPassword))
                {
                    isNeedToChangePassword = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Вы ввели неправильный старый пароль!");
                    isContinue = false;
                }
            }

            //If email change
            if (!string.Equals(userData.Email, model.Email))
            {
                if (RepositoryManager.UserRepository.IsEmailExist(model.Email))
                {
                    ModelState.AddModelError("Email", "Пользователь с данным email уже существует!");
                    isContinue = false;
                }
                else
                {
                    userData.Email = model.Email;
                }
            }

            //If gender change
            if (userData.Gender != model.Gender)
            {
                userData.Gender = model.Gender;
            }

            //If avatar change
            var avatarPostedFile = model.AvatarPostedFile;
            if (avatarPostedFile != null)
            {
                var extension = Path.GetExtension(avatarPostedFile.FileName);

                if (extension != ".jpg" && extension != ".png")
                {
                    ModelState.AddModelError("AvatarPostedFile", "Аватар должен быть в формате jpg или png!");
                    isContinue = false;
                }

                //Scale avatar
                var image = new WebImage(avatarPostedFile.InputStream);
                //We use x*SettingsManager.AvatarSize to have the best quality of image
                image.Resize(5*SettingsManager.AvatarSize, 5*SettingsManager.AvatarSize);

                if (isContinue)
                {
                    var newAvatarName = string.Concat(Guid.NewGuid(), extension);
                    var newAvatarPath = Path.Combine(Server.MapPath(ContentPathManager.UserAvatarFolder), newAvatarName);

                    //Save image as real hard disk path (as C:/MyFolder/image)
                    image.Save(newAvatarPath);

                    //Save avatar path by virtual image name
                    userData.AvatarName = newAvatarName;
                }
            }

            //Do changes
            if (isContinue)
            {
                RepositoryManager.UserRepository.Update(userData);

                if (isNeedToChangePassword)
                {
                    UserManager.MembershipProvider.ChangePassword(userData.Name, model.OldPassword, model.NewPassword);
                    model.NewPassword = string.Empty;
                    model.OldPassword = string.Empty;
                }
            }

            return View(model); //If username have changed - redirect to Login and then to settings again
        }

        [Authorize]
        [HttpGet]
        public ActionResult Messages()
        {
            MessageSettingsData messageSettings = RepositoryManager.MessageSettingsRepository.GetById(UserManager.UserId);
            var model = RepositoryManager.MessageRepository.GetList(UserManager.UserId, 1, 10, messageSettings);
            return View(model);
        }
    }
}
