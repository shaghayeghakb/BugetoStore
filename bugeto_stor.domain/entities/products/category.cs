using System;
using System.Collections.Generic;
using System.Text;
using bugeto_stor.domain.entities.commands;
using System.Linq;
using System.Threading.Tasks;

namespace bugeto_stor.domain.entities.products
{
    public class category: baseentity
    {
        public readonly object SubCategories;

        public string name { get; set; }
        public virtual category parentcategory{ get; set; }
        public long? parentcategoryid { get; set; }
        public object subcategories { get; set; }

        // public virtual category icollection<category> subcategories { get; set; }

    }
}
