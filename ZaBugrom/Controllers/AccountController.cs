using System.Web.Mvc;
using System.Web.Security;
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
        private static UserRepository _userRepository = new UserRepository();

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
                    ModelState.AddModelError("",
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
                ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует!");
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
            var id = WebSecurity.CurrentUserId;
            var userData = _userRepository.GetById(id);
            var model = ObjectMapperManager.DefaultInstance.GetMapper<UserData, ProfileSettingsInputModel>().Map(userData);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ProfileSettings(ProfileSettingsInputModel model)
        {
            var id = WebSecurity.CurrentUserId;
            var userData = _userRepository.GetById(id);

            bool isContinue = true;

            //If login change
            if (!string.Equals(userData.Name, model.Name))
            {
                if (WebSecurity.UserExists(model.Name))
                {
                    ModelState.AddModelError(string.Empty, "Юзер с логином" + model.Name +" уже существует!");
                    isContinue = false;
                }

                if (isContinue)
                {
                    userData.Name = model.Name;
                    _userRepository.Update(userData);

                    WebSecurity.Logout();
                    //TODO почему-то разлогинивает только после обновления
                }
            }

            //If email change
            if (!string.Equals(userData.Email, model.Email))
            {
                //TODO: сделать sp на проверку существования email
            }

            return View(model); //If username have changed - redirect to Login and then to settings again
        }
    }
}
