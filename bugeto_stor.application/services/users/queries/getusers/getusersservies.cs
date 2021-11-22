using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bugeto_stor.application.services.users.queries.getusers
{
    public class getusersservies : Igetusersservies
    {
        private readonly Idatabasecontext _context;
        private int rowsCount;

        public getusersservies(Idatabasecontext context)
        {
            _context = context;
        }
        public requestgetuserdto Execute(requestgetuserdto request)
        {
            var users = _context.users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.searchkey))
            {
                users = users.Where(p => p.fullname.Contains(request.searchkey) && p.email.Contains(request.searchkey));
            }
            int rowscount = 0;
             var userslist= users.topaged(request.page, 20, out rowscount).Select(p => new getusersdto
            {
                email = p.email,
                fullname = p.fullname,
                id = p.id,
                 isactive=p.isactive
             }).ToList();
            return new requestgetuserdto
            {
                rows = rowscount,
                users= userslist,
            };
    }
    }
    }
