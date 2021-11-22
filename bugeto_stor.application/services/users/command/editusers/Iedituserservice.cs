using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugeto_stor.application.services.users.command.editusers
{
    public interface Iedituserservice
    {
        resultdto execute(requestrgegisteruserdto request);
        object execute(requestedituserdto requestedituserdto);
    }
    public class edituserservice: Iedituserservice
    {
        private readonly Idatabasecontext _context;

       public edituserservice (Idatabasecontext context)
        {
            _context = context;
        }

        public resultdto execute(requestrgegisteruserdto request)
        {
            var user = _context.users.Find(request.userid);
            if(user== null)
            {
                return new resultdto
                {
                    issuccess = false,
                    message = "کاربری یافت نشد"
                };
            }
            user.fullname = request.fullname;
            _context.SaveChanges();
            return new resultdto()
            {
                issuccess = true,
                message = "ویرایش کاربر انجام شد"
            };
        }

        public object execute(requestedituserdto requestedituserdto)
        {
            throw new NotImplementedException();
        }
    }
    public class requestedituserdto
    {
        public long userid { get; set; }
        public string fullname { get; set; }
    }
}
