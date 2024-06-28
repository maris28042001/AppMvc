using Microsoft.AspNetCore.Mvc;

namespace AppMvc.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

    }
}
