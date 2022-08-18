using CrudAsp.netcore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAsp.netcore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository productRepository;
        public ProductController(IConfiguration configuration)
        {
            productRepository = new ProductRepository(configuration);
        }
        public IActionResult Index()
        {
            var data = productRepository.FindAll();
            return View(data);
        }
    }
}
