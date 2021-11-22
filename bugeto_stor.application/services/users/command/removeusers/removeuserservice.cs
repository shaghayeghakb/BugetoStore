using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace bugeto_stor.application.services.users.command.removeusers
{
    public class removeuserservice : Iremoveuserservice
    {
        private readonly Idatabasecontext _context;
        public removeuserservice(Idatabasecontext context)
        {
            _context = context;
        }
        public resultdto execute(long userid)
        {
            var user = _context.users.Find(userid);
            if (user == null)
            {
                return new resultdto
                {
                    issuccess = false,
                    message = "کاربری یافت نشد"
                };
            }
            user.removedtime = DateTime.Now;
            user.isremoved = true;
            _context.SaveChanges();
            return new resultdto()
            {
                issuccess = false,
                message = "کاربری یافت نشد"
            };
        }
    }
}