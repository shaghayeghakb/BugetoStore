using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common;
using bugeto_stor.common.dto;
using bugeto_stor.domain.entities.users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bugeto_stor.application.services.users.command.userlogins
{
    public interface Iuserloginservice
    {
        resultdto<resultuserlogindto> execute(string username, string password);
    }
    public class userloginservice : Iuserloginservice
    {
        private readonly Idatabasecontext _context;
        public userloginservice(Idatabasecontext context)
        {
            _context = context;
        }
        public resultdto<resultuserlogindto> execute(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return new resultdto<resultuserlogindto>()
                {
                    data = new resultuserlogindto()
                    {

                    },
                    issuccess = false,
                    message = "نام کاربری و رمز عبور را وارد نمایید",
                };
            }
            var user = _context.users
                .Include(p => p.userinroles)
                .ThenInclude(p => p.role)
               .Where(p => p.email.Equals(username)
               && p.isactive == true)
               .FirstOrDefault();
            if (user == null)
            {
                return new resultdto<resultuserlogindto>()
                {
                    data = new resultuserlogindto()
                    {

                    },
                    issuccess = false,
                    message = "کاربری با این ایمیل در سایت فروشگاه باگتو ثبت نام نکرده است",
                };
            }
            var passwordhasher = new passwordhasher();
            bool resultVerifyPassword = passwordhasher.VerifyPassword(user.password, password);
            if (resultVerifyPassword == false)
            {
                return new resultdto<resultuserlogindto>()
                {
                    data = new resultuserlogindto()
                    {
                    },
                    issuccess = false,
                    message = "رمز وارد شده اشتباه است!",
                };
            }
            var roles = "";
            foreach (var item in user.userinroles)
            {
                roles += $"{item.role.name}";
            }
            return new resultdto<resultuserlogindto>()
            {
                data = new resultuserlogindto()
                {
                    roles = roles,
                    userid = user.id,
                    name = user.fullname
                },
                issuccess = true,
                message = "ورود به سایت با موفقیت انجام شد",
            };
        }
    }

       
        public class resultuserlogindto
        {
            public long userid { get; set; }
            public string roles { get; set; }
            public string name { get; set; }
        }
    }
