
using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using bugeto_stor.domain.entities.products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugeto_stor.application.services.products.commadns.addnewcategory
{
   public interface Iaddnewcategoryservice
    {
        resultdto Execute(long? parentid, string name);
    }
    public class addnewcategoryservice : Iaddnewcategoryservice
    {
        private readonly Idatabasecontext _context;
        public addnewcategoryservice(Idatabasecontext context)
        {
            _context = context;
        }
        public resultdto Execute(long? parentid, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new resultdto()
                {
                    issuccess = false,
                    message = "نام دسته بندی را وارد نمایید",
                };
            }
            category category = new category()
            {
                name = name,
                parentcategory = getparent(parentid)
            };
            _context.categories.Add(category);
            _context.SaveChanges();
            return new resultdto()
            {
                issuccess = true,
                message = "دسته بندی با موفقیت اضافه شد",
            };
        }

        private category getparent(long? parentid)
        {
            return _context.categories.Find(parentid);
        }
    }
}
