using System.Collections.Generic;
using System.Text;
using static bugeto_stor.application.services.users.queries.getusers.getusersservies;

namespace bugeto_stor.application.services.users.queries.getusers
{
    public interface Igetusersservies
    {
        requestgetuserdto Execute(requestgetuserdto request);
    }
}