using bugeto_stor.application.interfaces.contexts;
//using bugeto_stor.application.services.users.command.userlogin;
using bugeto_stor.common;
using bugeto_stor.common.dto;
using bugeto_stor.domain.entities.users;
//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static bugeto_stor.domain.entities.users.user;

namespace bugeto_stor.application.services.users.command.rgegisteruser
{

   public interface Irgegisteruserservice
    {
        resultdto<resultrgegisteruserdto> execute(resultrgegisteruserdto request);
        object execute(requestrgegisteruserdto requestrgegisteruserdto);
    }
    public class rgegisteruserservice : Irgegisteruserservice
    {
        private readonly Idatabasecontext _context;

        public rgegisteruserservice(Idatabasecontext context)
        {
            _context = context;
        }

        public string email { get; private set; }
        public string fullname { get; private set; }

        public resultdto<resultrgegisteruserdto> execute(requestrgegisteruserdto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.email))
                {
                    return new resultdto<resultrgegisteruserdto>()
                    {
                        data = new resultrgegisteruserdto()
                        {
                            userid = 0,
                        },
                        issuccess = false,
                        message = "پست الکترونیک را وارد نمایید"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.fullname))
                {
                    return new resultdto<resultrgegisteruserdto>()
                    {
                        data = new resultrgegisteruserdto()
                        {
                            userid = 0,
                        },
                        issuccess = false,
                        message = "نام را وارد نمایید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.password))  
                {
                    return new ResultDto<resultrgegisteruserdto>()
                    {
                        Data = new resultrgegisteruserdto()
                        {
                            userid = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید"
                    };
                }
                if (request.password != request.repassword)
                {
                    return new ResultDto<resultrgegisteruserdto>()
                    {
                        data = new resultrgegisteruserdto()
                        {
                            userid = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور و تکرار آن برابر نیست"
                    };
                }
                string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

                var match = Regex.Match(request.email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDto<resultrgegisteruserdto>()
                    {
                       data = new resultrgegisteruserdto()
                        {
                            userid = 0,
                        },
                        issuccess = false,
                        message = "ایمیل خودرا به درستی وارد نمایید"
                    };
                }
                var passwordHasher = new passwordhasher();
                var hashedPassword = passwordHasher.HashPassword(request.password);
                user user = new user()
                {
                    email = request.email,
                    fullname = request.fullname,
                    password = hashedPassword,
                    isactive = true,
                };
                //Execute(request.password),
                List<userinrole> userInRoles = new List<userinrole>();

                foreach (var item in request.roles)
                {
                    var roles = _context.roles.Find(item.id);
                    userInRoles.Add(new userinrole
                    {
                        role = roles,
                        roleid = roles.id,
                        user = user,
                        userid = user.id,
                    });
                }
                user.userinroles = userInRoles;

                _context.users.Add(user);

                _context.SaveChanges();

                return new ResultDto<resultrgegisteruserdto>()
                {
                    Data = new resultrgegisteruserdto()
                    {
                        userid = user.id,

                    },
                    IsSuccess = true,
                    Message = "ثبت نام کاربر انجام شد",
                };
            }
            catch (Exception)
            {
                return new ResultDto<resultrgegisteruserdto>()
                {
                    Data = new resultrgegisteruserdto()
                    {
                        userid = 0,
                    },
                    IsSuccess = false,
                    Message = "ثبت نام انجام نشد !"
                };
            }
        }

        public resultdto<resultrgegisteruserdto> execute(resultrgegisteruserdto request)
        {
            throw new NotImplementedException();
        }

        object Irgegisteruserservice.execute(requestrgegisteruserdto requestrgegisteruserdto)
        {
            throw new NotImplementedException();
        }
    }
}
       
    public class requestrgegisteruserdto
    {
  
    public string fullname { get; set; }
        public string email { get; set; }
        public List<rolesinrgegisteruserdto> roles { get; set; }
    public string repassword { get; set; }
    public string password { get; set; }
    public string userid { get;  set; }
}
    public class rolesinrgegisteruserdto
    {
        public long id { get; set; }
      
    }


    public class resultrgegisteruserdto
    {
        public long userid { get; set; }

    //public static implicit operator resultrgegisteruserdto(ResultUserloginDto v)
    //{
    //    throw new NotImplementedException();
    //}
}