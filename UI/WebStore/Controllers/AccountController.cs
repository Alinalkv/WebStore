using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger) {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Register
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);
            
            var user = new User
            {
                UserName = Model.UserName
            };
            using (_Logger.BeginScope("регистрации нового пользователя {0}", user.UserName))
            {
                _Logger.LogInformation("Начинается процесс регистрации нового пользователя {0}", user.UserName);
                var registration_result = await _UserManager.CreateAsync(user, Model.Password);
                if (registration_result.Succeeded)
                {
                    _Logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);
                    //добавление роли новому пользователю
                    await _UserManager.AddToRoleAsync(user, Role.User);
                    //var add_role = await _UserManager.AddToRoleAsync(user, Role.User);
                    //if(add_role.Succeeded)
                    //{
                    //    _Logger.LogInformation("Пользователю {0} добавлена роль {1}", user.UserName, Role.User);
                    //}
                    //else
                    //{
                    //    _Logger.LogError("Ошибка при добавлении пользователю {0} роли {1}: {2}", user.UserName, Role.User,
                    //        string.Join(",", add_role.Errors.Select(error => error.Description)));
                    //    throw new ApplicationException("Ошибка наделения нового пользователя ролью");
                    //}
                    await _SignInManager.SignInAsync(user, false);
                    _Logger.LogInformation("Пользователь {0} успешно вошёл в систему", user.UserName);
                    return RedirectToAction("Index", "Home");
                }
                _Logger.LogError("Ошибка при добавлении пользователю {0} роли {1}: {2}", user.UserName, Role.User,
                            string.Join(",", registration_result.Errors.Select(error => error.Description)));
                foreach (var error in registration_result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(Model);
        }
        #endregion

        #region Login
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                true);

            if (login_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} успешно вошёл в систему", Model.UserName);
                if (Url.IsLocalUrl(Model.ReturnUrl))
                {
                    _Logger.LogInformation("Пользователь {0} перенаправлен на {1}", Model.UserName, Model.ReturnUrl);
                    return Redirect(Model.ReturnUrl);
                }
                _Logger.LogInformation("Пользователь {0} перенаправлен на главную страницу", Model.UserName);
                return RedirectToAction("Index", "Home");
            }
            _Logger.LogWarning("Пользователь {0} произвёл некорректную попытку входа", Model.UserName);
            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль");
            return View(Model);
        }
        #endregion
        public async Task<IActionResult> Logout()
        {
            await _SignInManager.SignOutAsync();
            _Logger.LogInformation("Пользователь {0} вышел из системы", User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied() => View();
    }
}
