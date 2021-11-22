using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace bugeto_stor.application.services.products.queries.getallcategories
{
    public interface Igetallcategoriesservice
    {
        resultdto<List<allcategoriesdto>> execute();
    }
    public class getallcategoriesservice : Igetallcategoriesservice
    {
        private readonly Idatabasecontext _context;

        public getallcategoriesservice(Idatabasecontext context)
        {
            _context = context;
        }
        public resultdto<List<allcategoriesdto>> execute()
        {
            var categories = _context
                .categories
                .Include(p => p.parentcategory)
                .Where(p => p.parentcategory != null)
                .ToList()
                .Select(p => new allcategoriesdto
                {
                    id = p.id,
                    name = $"{p.parentcategory.name} - {p.name}",
                }
                ).ToList();

            return new resultdto<List<allcategoriesdto>>
            {
                data = categories,
                issuccess = false,
                message = "",
            };
        }

        resultdto<List<getallcategories.allcategoriesdto>> Igetallcategoriesservice.execute()
        {
            throw new NotImplementedException();
        }

        public class allcategoriesdto
        {
            public long id { get; set; }
            public string name { get; set; }
        }
    } }