using System.Collections.Generic;

namespace bugeto_stor.domain.entities.users
{
    public partial class user
    {
        public class role
        {
            public bool isremoved;

            public long id { get; set; }
            public string name { get; set; }
            public ICollection<userinrole> userinroles { get; set; }
        }
    }
}
