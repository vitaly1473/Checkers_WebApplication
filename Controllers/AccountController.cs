// file Controllers/AccountController.cs

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CheckersClub.Data;
using CheckersClub.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CheckersClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        // обрабатываем POST-запрос на вход пользователя в систему
        [HttpPost] // атрибут указывает, что метод Login должен обрабатывать только POST-запросы.

        // метод принимает два параметра: login и password, которые пользователь вводит в форму входа.
        //public IActionResult Login(string login, string password)

        // метод обработки входа пользователя на сайт
        public async Task<IActionResult> Login(string login, string password)
        { // поиск пользователя в базе данных, у которого логин и пароль совпадают с введенными
            var user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == password); //Поиск пользователя в базе данных
         // если такой пользователь найден, перенаправляем его на страницу игры
            if (user != null)
            //{
            // // при успешной авторизации пользователя, сохраняем его логин в сессии
            //    HttpContext.Session.SetString("UserLogin", user.Login); // Сохраняем логин пользователя в сессии
            //    return RedirectToAction("Game");
            //}
            {
                // создание и установка coocie аутентификации
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login), // ClaimTypes.Name — хранит логин пользователя
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // ClaimTypes.NameIdentifier — хранит уникальный идентификатор пользователя
                    // можно добавить дополнительные claims, если необходимо
                };
                // создаем объект типа ClaimsIdentity
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                // создаём свойства для аутентификации
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // cookie будет постоянным (не будет удалено при закрытии браузера)
                };
                // метод устанавливает куки для пользователя: 
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Game"); // при успехе, направляем на страницу игры
            }

            //  если пользователь не найден, добавляем сообщение об ошибке в модельное состояние
            ModelState.AddModelError("", "Invalid login or password");
            return View();
        }

        // чтобы вывести логин пользователя в приветствии на странице игры, нужно передать логин через ViewBag или ViewData в методе Game контроллера
        //public IActionResult Game()
        //{
        //    var login = HttpContext.Session.GetString("UserLogin");
        //    ViewBag.Login = login;
        //    return View();
        //}
        // чтобы вывести логин пользователя в приветствии на странице игры, нужно передать логин через ViewBag или ViewData в методе Game контроллера
        public IActionResult Game()
        {
            var login = User.Identity.Name; // получаем логин пользователя из cookies
            ViewBag.Login = login;
            return View();
        }
        // метод для выхода пользователя из системы авторизации:
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // на главную страницу Index
        }

    }
} // закрыли namespace CheckersClub.Controllers
