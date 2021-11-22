using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace bugeto_stor.domain.entities.commands
{
   
        public abstract class baseentity<TKey>
        {
            public TKey id { get; set; }
            public DateTime inserttime { get; set; } = DateTime.Now;
            public DateTime? updatetime { get; set; }
         
            public bool isremoved { get; set; } = false;
            public DateTime? removedtime { get; set; }
        }
        public abstract class baseentity : baseentity<long>
        {
        }
    }

