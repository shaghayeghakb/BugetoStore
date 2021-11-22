using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using bugeto_stor.application.services.users.command.rgegisteruser;
using bugeto_stor.application.services.users.command.userlogins;
using bugeto_stor.common.dto;
using bugeto_stor.domain.entities.users;
using endpoint.site.Controllers.models.viewmodels.authenticationviewmodel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace endpoint.site.Controllers
{
    public class authenticationcontroller:Controller
    {
        private readonly Irgegisteruserservice _rgegisteruserservice;
        private readonly Iuserloginservice _userloginservice;
        public authenticationcontroller(Irgegisteruserservice rgegisteruserservice, Iuserloginservice userloginservice)
        {
            _rgegisteruserservice = rgegisteruserservice;
            _userloginservice = userloginservice;
        }
        [HttpGet]
        public IActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult signup(signupviewmodel request)
        {
            if (string.IsNullOrWhiteSpace(request.fullname) ||
                string.IsNullOrWhiteSpace(request.email) ||
                string.IsNullOrWhiteSpace(request.password) ||
                string.IsNullOrWhiteSpace(request.repassword))
            {
                return Json(new resultdto { issuccess = false, message = "لطفا تمامی موارد رو ارسال نمایید" });
            }
            if (user.identity.IsAuthenticated == true)
            {
                return Json(new resultdto { issuccess = false, message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید" });
            }
            if (request.password != request.repassword)
            {
                return Json(new resultdto { issuccess = false, message = "رمز عبور و تکرار آن برابر نیست" });
            }
            if (request.password.Length < 8)
            {
                return Json(new resultdto { issuccess = false, message = "رمز عبور باید حداقل 8 کاراکتر باشد" });
            }

            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(request.email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Json(new resultdto { issuccess = true, message = "ایمیل خودرا به درستی وارد نمایید" });
            }


            var signeupResult = _rgegisteruserservice.execute(new requestrgegisteruserdto
            {
                email = request.email,
                fullname = request.fullname,
                password = request.password,
                repassword = request.repassword,
                roles = new List<rolesinrgegisteruserdto>()
                                {
                                     new rolesinrgegisteruserdto {id = 3},
                                }
            });

            if (signeupResult.issuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signeupResult.Data.UserId.ToString()),
                                new Claim(ClaimTypes.Email, request.email),
                                new Claim(ClaimTypes.Name, request.fullname),
                                new Claim(ClaimTypes.Role, "customer"),
             };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);
            }
            return Json(signeupResult);
        }
        public IActionResult signin(string returnurl = "/")
        {
            ViewBag.url = returnurl;
            return View();
        }

        [HttpPost]
        public IActionResult signin(string email, string password, string url = "/")
        {
            var signupresult = _userloginservice.execute(email, password);
            if (signupresult.issuccess == true)
            {
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,signupresult.data.userid.ToString()),
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.Name, signupresult.data.name),
                        new Claim(ClaimTypes.Role, signupresult.data.roles ),
                    };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signupresult);
        }


        public IActionResult signout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
