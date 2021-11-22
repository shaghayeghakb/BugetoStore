using bugeto_stor.application.interfaces.contexts;

namespace bugeto_stor.application.interfaces.facadpatterns
{
    public class igetallcategoryservice
    {
        private Idatabasecontext context;

        public igetallcategoryservice(Idatabasecontext context)
        {
            this.context = context;
        }
    }
}