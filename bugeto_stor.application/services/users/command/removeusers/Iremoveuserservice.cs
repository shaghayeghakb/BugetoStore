using bugeto_stor.common.dto;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace bugeto_stor.application.services.users.command.removeusers
{
    public interface Iremoveuserservice
    {
        resultdto execute(long userid);
    }
}
