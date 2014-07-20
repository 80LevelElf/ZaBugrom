using System.Web.Mvc;
using BLToolkit.Reflection;
using CommonDAL.SqlDAL;
using EmitMapper;
using Models.Data;
using Models.InputModels.Account;
using WebMatrix.WebData;
using ZaBugrom.Managers;

namespace ZaBugrom.Controllers
{
    public class AccountController : Controller
    {
        private static readonly UserRepository _userRepository = TypeAccessor<UserRepository>.CreateInstance();

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

            if (_userRepository.IsEmailExist(model.Email))
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже существует!");
            }

            //To show new errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            WebSecurity.CreateUserAndAccount(model.Name, model.Password, new {Email = model.Email});
            WebSecurity.Login(model.Name, model.Password, true);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ProfileSettings()
        {
            var userData = GetCurrentUser();
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

            var userData = GetCurrentUser();

            //We change settings only if everything is ok
            bool isContinue = true;
            bool isNeedToLogout = false;
            bool isNeedToChangePassword = false;

            //If password change
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                if (MembershipManager.MembershipProvider.ValidateUser(userData.Name, model.OldPassword))
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
                if (_userRepository.IsEmailExist(model.Email))
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

            //If login change
            if (!string.Equals(userData.Name, model.Name))
            {
                if (WebSecurity.UserExists(model.Name))
                {
                    ModelState.AddModelError("Name", "Юзер с логином " + model.Name + " уже существует!");
                    isContinue = false;
                }
                else
                {
                    userData.Name = model.Name;
                    isNeedToLogout = true;
                }
            }

            //Do changes
            if (isContinue)
            {
                _userRepository.Update(userData);

                if (isNeedToChangePassword)
                {
                    MembershipManager.MembershipProvider.ChangePassword(userData.Name, model.OldPassword, model.NewPassword);
                    model.NewPassword = string.Empty;
                    model.OldPassword = string.Empty;
                }

                if (isNeedToLogout)
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Login");
                }
            }

            return View(model); //If username have changed - redirect to Login and then to settings again
        }

        private static UserData GetCurrentUser()
        {
            return _userRepository.GetById(WebSecurity.CurrentUserId);
        }
    }
}
