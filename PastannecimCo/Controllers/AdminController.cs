using Microsoft.AspNetCore.Mvc;
using PastannecimCo.Models.ViewModels;
using PastannecimCo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICakeOrderService _cakeOrderService;

        public AdminController(ICakeOrderService cakeOrderService)
        {
            _cakeOrderService = cakeOrderService ?? throw new ArgumentNullException(nameof(cakeOrderService));
        }

        public IActionResult Index()
        {
            var cakeOrders = _cakeOrderService.GetAllCakeOrders();

            return View(cakeOrders.Content);
        }

        [HttpGet]
        public async  Task<IActionResult> Update(int id)
        {
            var cakeOrder = await _cakeOrderService.GetOrderByIdAsync(id);
            return View(cakeOrder.Content);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CakeOrderViewModel cakeOrder)
        {
            //var order = await _cakeOrderService.UpdateCakeOrderAsync(cakeOrder);
            // yukarıdaki gibiydi ancak update sonrası bunu bir yerde kullanmayacağımız için /eşitliğin sağ tarafı update servis üzerindne veritabanına yazıp geldi) burada yeniden herhangi bir değişkene atmaya gerek olmadığında discard yaptı.
            
            var _ = await _cakeOrderService.UpdateCakeOrderAsync(cakeOrder);

            //Sonra da aşağıdaki gib index e döndük.
            //return View();

            return RedirectToAction("Index");
        }

    }
}
