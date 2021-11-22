using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using System.Collections.Generic;
using System.Linq;
namespace bugeto_stor.application.services.users.queries.getrols
{
    public class getrolesservice : Igetrolsservice
    {
        private readonly Idatabasecontext _context;
        public getrolesservice(Idatabasecontext context)
        {
            _context = context;
        }
         public resultdto<List<rolsdto>> execute()
        {
            var roles = _context.roles.ToList().Select(p => new rolsdto
            {
                id = p.id,
                name = p.name
            }).ToList();
            return new resultdto<List<rolsdto>>()
            {
                data = roles,
                issuccess = true,
                message = "",
            };
        }
    }

}
