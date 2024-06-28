using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;
        private readonly ProductService _productsService;
        public FirstController(ILogger<FirstController> logger, ProductService productsService)
        {   
            _logger = logger;
            _productsService = productsService;
        }

        public string Index(){
            _logger.LogInformation("index action");
            return "Tôi là index của first";
        }
        [TempData]
        public string StatusMessage {set;get;}
        public IActionResult ViewProduct(int? id){
            var product = _productsService.Where(p => p.Id == id).FirstOrDefault();
            if(product == null){
                // TempData["StatusMessage"] = "san pham khong co";
                StatusMessage = "san pham khong co";
                return Redirect(Url.Action("Index", "Home"));
            }
            // return View(product);
            this.ViewData["product"] = product;
            ViewData["Title"] = product.Name;
            return View("ViewProduct2");
        }
    }
}