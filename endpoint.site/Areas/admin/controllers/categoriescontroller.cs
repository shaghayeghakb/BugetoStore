using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using bugeto_stor.application.interfaces.facadpatterns;
using Microsoft.AspNetCore.Mvc;
using bugeto_stor.application.services.products.facapattern;
using static bugeto_stor.application.services.products.facapattern.productfacad;

namespace endpoint.site.Areas.Admin.controllers
{

    [Area("Admin")]
    public class categoriescontroller:Controller
    {
        private readonly Iproductfacad _productfacad;
        public categoriescontroller(Iproductfacad productfacad)
        {
            _productfacad = productfacad;
        }

        public IActionResult Index(long? parentid)
        {
            return View(_productfacad.getcategoriesservice.Execute(parentid).data);
        }

        private IActionResult View(object data)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult addnewcategory(long? parentid)
        {
            ViewBag.parentid = parentid;
            return View();
        }
        [HttpPost]
        public IActionResult addnewcategory(long? parentid, string name)
        {
            var result = _productfacad.addnewcategoryservice.Execute(parentid, name);
            return Json(result);
        }
    }
}