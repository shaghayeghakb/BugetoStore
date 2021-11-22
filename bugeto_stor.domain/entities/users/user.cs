using bugeto_stor.domain.entities.commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace bugeto_stor.domain.entities.users
{
    public partial class user:baseentity
    {
     
        public string fullname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isactive { get; set; }
        public ICollection<userinrole> userinroles { get; set; }
    }
}
 