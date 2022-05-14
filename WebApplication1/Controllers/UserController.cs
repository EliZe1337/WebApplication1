using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.ApplicationContext;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRep _repository;
        public UserController(IUserRep rep)
        {
            _repository = rep;
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(userscard model)
        {
            string name = model.Login;
            var user = await _repository.GetByName(name);
            if(user == null)
            {
                return Redirect("/User/loginerror");
            }
            else if(user.logid == 0)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Login),
                    new Claim(ClaimTypes.Role, "User")
                };
                var claimidentity = new ClaimsIdentity(claims, "Cookie");
                var claimprincipial = new ClaimsPrincipal(claimidentity);
                await HttpContext.SignInAsync("Cookie", claimprincipial);
                return Redirect("/Home/Home");
            }
            else if(user.logid == 1)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Login),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var claimidentity = new ClaimsIdentity(claims, "Cookie");
                var claimprincipial = new ClaimsPrincipal(claimidentity);
                await HttpContext.SignInAsync("Cookie", claimprincipial);
                return Redirect("/Home/Home");
            }
            return Redirect("idi nahuy");
            
        }
        [HttpGet]
        public IActionResult AddBalance() => View();
        [HttpPost]
        public async Task<IActionResult> AddBalance(userscard model)
        {
            int balance = model.Balance;
            string name = User.Identity.Name;
            await _repository.AddBalance(balance, name);
            return Redirect("/Home/AddBalance");
        }
        public IActionResult loginerror() => View();
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookie");
            return Redirect("/Home/Home");
        }
        public async Task<IActionResult> Buy(int price)
        {
            string name = User.Identity.Name;
            await _repository.Buy(price, name);
            return null;
        }
        public async Task<IActionResult> Profile()
        {
            string name = User.Identity.Name;
            var res = await _repository.GetByName(name);
            return View(res);
        }
        public IActionResult Index() => View();
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(userscard model)
        {
            await _repository.Register(model);
            return await Login(model);
           
        }
    
    
    }
}
