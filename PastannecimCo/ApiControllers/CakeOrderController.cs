using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastannecimCo.Data;
using PastannecimCo.Models.DTO;
using PastannecimCo.Models.Entities;
using PastannecimCo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.ApiControllers
{
    [ApiController]
    [Route("api/order")]
    public class CakeOrderController : ControllerBase
    {
        //private readonly ApplicationDbContext _context; // refaktoring sonrası bunu yorum yapıp aşağıdakini ekledik.
        //public CakeOrderController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        private readonly ICakeOrderService _cakeOrderService;

        public CakeOrderController(ICakeOrderService cakeOrderService)
        {
            _cakeOrderService = cakeOrderService ?? throw new ArgumentNullException(nameof(cakeOrderService));  // DI esnasında null ise bunu baştan bilelim.
        }
        //[HttpPost]
        //public JsonResult Post ([FromBody] OrderDetails orderDetails ) // twilio studio dan json döneceğizz..
        //{
        //    return null;
        //}

        // GET
        [HttpGet]
        public IActionResult Get()
        {
            return null;
        }


        // POST
        [HttpPost]
        public async Task<IActionResult> Post(OrderDetails orderDetails)
        {
            var cakeOrderResponse = await _cakeOrderService.AddNewOrderAsync(orderDetails);
            return new JsonResult(cakeOrderResponse.Content.Id.ToString());

            // başlangıçta şağıdaki bölümü kullanıp çalıştırdık. Ancak service kullanınca bunlara artık gerek kalmadı. Yukarıdaki kodu bu aşamada yeni ekledik. Böylece API ye gönderdiğimiz kodu en aza indirdik. (burası end point ve 2-3 satır olmalı)

            //var user = await _context.Users
            //    .FirstOrDefaultAsync(e => e.Number == orderDetails.Number);

            //if (user == null)
            //{
            //    user = new User
            //    {
            //        Name = orderDetails.Name,
            //        Number = orderDetails.Number,
            //        Email = orderDetails.Email
            //    };

            //    var entity = await _context.Users.AddAsync(user);

            //    await _context.SaveChangesAsync();
            //    user = entity.Entity;
            //}

            //var cakeOrder = new CakeOrder
            //{
            //    Flavour = orderDetails.Cake.Flavour,
            //    Frosting = orderDetails.Cake.Frosting,
            //    Topping = orderDetails.Cake.Topping,
            //    Size = orderDetails.Cake.Size,
            //    Price = orderDetails.Cake.Price,
            //    OrderDate = DateTime.UtcNow,
            //    UserId = user.Id
            //};

            //var cakeEntity = await _context
            //    .CakeOrders.AddAsync(cakeOrder);

            //await _context.SaveChangesAsync();


            //return new JsonResult(cakeEntity.Entity.Id.ToString());
        }
    }
}
