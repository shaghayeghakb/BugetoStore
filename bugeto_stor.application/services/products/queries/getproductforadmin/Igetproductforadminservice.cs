using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common;
using bugeto_stor.common.dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugeto_stor.application.services.products.queries.getproductforadmin
{
   public interface Igetproductforadminservice
    {
        resultdto<productforadmindto> execute(int page = 1, int pagesize = 20);
    }

    public class getproductforadminservice : Igetproductforadminservice
    {
        private readonly Idatabasecontext _context;
        public getproductforadminservice(Idatabasecontext context)
        {
            _context = context;
        }
        public resultdto<productforadmindto> execute(int page = 1, int pagesize = 20)
        {
            int rowcount = 0;
            var products = _context.products
              .Include(p => p.category)
               .ToPaged(page, pagesize, out rowcount)
               .Select(p => new productsformadminlist_dto
               {
                   id = p.id,
                   brand = p.brand,
                   category = p.category.name,
                   description = p.description,
                   displayed = p.displayed,
                   inventory = p.inventory,
                   name = p.name,
                   price = p.price,
               }).ToList();

            return new resultdto<productforadmindto>()
            {
                data = new productforadmindto()
                {
                    products = products,
                    currentpage = page,
                    pagesize = pagesize,
                    rowcount = rowcount
                },
                issuccess = true,
                message = "",
            };
        }
    }
    public class productforadmindto
    {
        public int rowcount { get; set; }
        public int currentpage { get; set; }
        public int pagesize { get; set; }
        public List<productsformadminlist_dto> products { get; set; }
    }
    public class productsformadminlist_dto
    {
        public long id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int inventory { get; set; }
        public bool displayed { get; set; }
    }
}