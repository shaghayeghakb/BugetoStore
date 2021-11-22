using bugeto_stor.common.dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text;
using System.Threading.Tasks;
namespace bugeto_stor.application.services.users.queries.getrols
{

    public interface Igetrolsservice         
    {
        resultdto<List<rolsdto>> execute(); 
    
    }

}
