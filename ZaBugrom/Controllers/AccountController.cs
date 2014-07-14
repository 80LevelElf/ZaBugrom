using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models.InputModels;
using WebMatrix.WebData;

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
                if (WebSecurity.IsAccountLockedOut(model.Login, 6, 3600))
                {
                    ModelState.AddModelError("",
                       "Вы пытались ввести пароль слишком много раз. Вам придеться подождать час, прежде чем попытаться войти снова.");
                }

                if (WebSecurity.Login(model.Login, model.Password, true))
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

        public ActionResult Register(RegistrationInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (WebSecurity.UserExists(model.Login))
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже существует!");
                return View(model);
            }

            WebSecurity.CreateUserAndAccount(model.Login, model.Password, new {Email = model.Email});
            WebSecurity.Login(model.Login, model.Password, true);

            return RedirectToAction("Index", "Home");
        }
    }
}
