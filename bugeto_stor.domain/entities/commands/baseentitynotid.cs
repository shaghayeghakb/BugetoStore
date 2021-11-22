using System;

namespace bugeto_stor.domain.entities.commands
{
 
        
    public abstract class baseentitynotid
    {
        public DateTime  inserttime { get; set; } = DateTime.Now;
        public DateTime? updatetime { get; set; }
        public bool isremoved { get; set; } = false;
        public DateTime? removetime { get; set; }
    }
}
