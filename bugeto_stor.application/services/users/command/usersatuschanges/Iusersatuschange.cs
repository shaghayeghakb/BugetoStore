using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugeto_stor.application.services.users.command.UserSatusChanges
{
    public interface Iusersatuschangeservice
    {
        resultdto execute(long userid);
    }
    public class usersatuschangeservice: Iusersatuschangeservice
    {
        private readonly Idatabasecontext _context;

        public usersatuschangeservice(Idatabasecontext context)
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
            user.isactive = !user.isactive;
            _context.SaveChanges();
            string userstate = user.isactive == true ? "فعال" : "غیر فعال";
            return new resultdto()
            {
                issuccess = true,
                message = $"کاربر با موفقیت {userstate} شد!",
            };
        }
    }
}
