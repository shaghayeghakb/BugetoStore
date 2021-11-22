
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bugeto_stor.application.interfaces.facadpatterns;
using bugeto_stor.application.services.products.commadns.addnewproduct;
using bugeto_stor.common.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace endpoint.site.Areas.admin.controllers
{
    [Area("admin")]
    public class productcontroller:controller
    {
        private readonly Iproductfacad _productfacad;
        private readonly object ViewBag;
        private List<Iformfile> Images;

        public productcontroller(Iproductfacad productfacad)
        {
            _productfacad = productfacad;
        }
        public IActionResult Index(int page = 1, int pagesize = 20)
        {
            return View(_productfacad.getproductforadminservice.execute(page, pagesize).data);
        }

        public IActionResult detail(long id)
        {
            return View(_productfacad.getproductdetailforadminservice.execute(id).data);
        }
        private IActionResult View(bugeto_stor.application.services.products.queries.getproductforadmin.productforadmindto data)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public IActionResult addnewproduct()
        {
            ViewBag.categories = new SelectList(_productfacad.getallcategoriesservice.execute().data, "id", "name");
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult addnewproduct(requestaddNewproductdto request, List<addnewproduct_features> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < request.Form.Files.Count; i++)
            {
                var file = request.Form.Files[i];
                images.Add(file);
            }
            request.im
                ages = images;
            request.features = features;
            return Json(_productfacad.addnewproductservice.execute(request));
        }

        private IActionResult Json(resultdto resultdto)
        {
            throw new NotImplementedException();
        }
    }
}
