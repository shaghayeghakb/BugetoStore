using System.Collections.Generic;

namespace bugeto_stor.application.services.products.commadns.addnewproduct
{
    public class requestaddNewproductdto
    {
        public List<Microsoft.AspNetCore.Http.IFormFile> images;

        public List<addnewproduct_features> features { get; set; }
        public object Form { get; set; }
    }
}