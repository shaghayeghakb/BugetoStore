using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace bugeto_stor.application.services.products.queries.getallcategories
{
    public interface Igetcategoriesservice
    {
        resultdto<List<categoriesdto>> execute(long? parentid);
    }
    public class getcategoriesservice : Igetcategoriesservice
    {
        private readonly Idatabasecontext _context;

        public getcategoriesservice(Idatabasecontext context)
        {
            _context = context;
        }

        public resultdto<List<categoriesdto>> execute(long? parentid)
        {
            var categories = _context.categories
                .Include(p => p.parentcategory)
                .Include(p => p.subcategories)
                .Where(p => p.parentcategoryid == parentid)
                .ToList()
                .Select(p => new categoriesdto
                  {
                    id = p.id,
                    name = p.name,
                    parent = p.parentcategory != null ? new
                            parentcategorydto
                    {
                        id = p.parentcategory.id,
                        name = p.parentcategory.name,
                    }
                         : null,
                    haschiid = p.SubCategories.Count() > 0 ? true : false,
                }).ToList();

            return new resultdto<List<categoriesdto>>()
            {
                data = categories,
                issuccess = true,
                message = "لیست با موفقیت برگشت داده شد"
            };
        }
    }
    }

