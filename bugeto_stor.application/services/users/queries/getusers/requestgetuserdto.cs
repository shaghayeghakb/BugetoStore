using System.Collections.Generic;

namespace bugeto_stor.application.services.users.queries.getusers
{
    public class requestgetuserdto
    {
        internal List<getusersdto> users;
        internal int rows;

        public string searchkey { get; set; }
        public int page { get; set; }
}
    }
