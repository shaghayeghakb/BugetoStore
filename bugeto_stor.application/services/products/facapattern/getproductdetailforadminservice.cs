using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.application.interfaces.facadpatterns;

namespace bugeto_stor.application.services.products.facapattern
{
    internal class getproductdetailforadminservice : Igetproductdetailforadminservice
    {
        private Idatabasecontext context;

        public getproductdetailforadminservice(Idatabasecontext context)
        {
            this.context = context;
        }
    }
}