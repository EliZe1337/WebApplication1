using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ApplicationContext;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ICardRepository _baserep;
        public HomeController(ICardRepository rep)
        {
            _baserep = rep;
        }
        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _baserep.GetEntity(id);  
            if (item == null)
            {
                return Redirect("/home/idi nahyi");
            }
            return View(item);  
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(shopcard model)
        {
            await _baserep.Edit(model);
            return Redirect($"/Home/GetById/{model.type}");
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "Admin")]
        public IActionResult RegisterCardItem() => View();
        [HttpPost]
        public async Task<IActionResult> RegisterCardItem(shopcard model)
        {
            var id = model.type;
            await _baserep.Create(model);
            return Redirect($"/Home/GetById/{id}");
        }
        public async Task<IActionResult> GetEntity(int id)
        {
            var item = await _baserep.GetEntity(id);
            
            return View(item);
        }
        public async Task<IActionResult> filter2(string filter)
        {
            return await Filter(filter);
            
        }
        public async Task<IActionResult> AddBalance(int balance)
        {
            if(User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("Admin"))
                {
                    

                }
            }
            return null;
        }
        public IActionResult GetById(int id)
        {
            var item = _baserep.GetbyId(id);
            return View(item);  
        }
        [AllowAnonymous]
        public async Task<IActionResult> Home()
        {
            var list = await _baserep.GetAll();
            return View(list);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string filter)
        {
            var s = await _baserep.filter(filter);
            return View(s);
        }
            
        public IActionResult Shop() => View();  
        public IActionResult Details() => View();
        public IActionResult cart() => View();    
        public IActionResult checkout() => View();    
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
