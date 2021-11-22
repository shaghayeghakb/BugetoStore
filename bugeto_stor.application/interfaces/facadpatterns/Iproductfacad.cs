
using bugeto_stor.application.services.products.commadns.addnewcategory;
using bugeto_stor.application.services.products.commadns.addnewproduct;
using bugeto_stor.application.services.products.queries.getallcategories;
//using bugeto_stor.application.services.products.queries.getcategories;
using bugeto_stor.application.services.products.queries.getproductforadmin;
using bugeto_stor.application.services.products.queries.getallcategories;
//using bugeto_stor.application.services.products.queries.getproductdetailforadminservice;
using bugeto_stor.application.services.products.queries.getproductforadmin;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugeto_stor.application.interfaces.facadpatterns
{
  public  interface Iproductfacad
    {  addnewcategoryservice  addnewcategoryservice { get; }
       Igetcategoriesservice  getcategoriesservice { get; }
        addnewproductservice addnewproductservice { get; }
        igetallcategoryservice getallcategoryservice { get; }

        //لیست محصولات
        Igetproductforadminservice getproductforadminservice { get; }
        Igetproductdetailforadminservice getproductdetailforadminservice { get; }
        object getallcategoriesservice { get; set; }
    }
}
