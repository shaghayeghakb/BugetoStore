using bugeto_stor.domain.entities.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugeto_stor.domain.entities.products
{
   public class product:baseentity
    {
        public string name { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int inventory { get; set; }
        public bool displayed { get; set; }

        public virtual category category { get; set; }
        public long categoryid { get; set; }
        public virtual ICollection<productimages> productimages{ get; set; }
        public virtual ICollection<productfeatures> productfeatures { get; set; }
    }
}
