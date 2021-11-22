using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bugeto_stor.application.services.users.command.editusers;
using bugeto_stor.application.services.users.command.removeusers;
using bugeto_stor.application.services.users.command.rgegisteruser;
using bugeto_stor.application.services.users.command.UserSatusChanges;
using bugeto_stor.application.services.users.queries.getrols;
using bugeto_stor.application.services.users.queries.getusers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace endpoint.site.Areas.Admin.controllers
{
    [Area("Admin")]
    public class userscontroller : Controller
    {
        private readonly Igetusersservies _getusersservice;
        private readonly Igetrolsservice _getRolesService;
        private readonly Irgegisteruserservice _registeruserservice;
        private readonly Iremoveuserservice _removeuserservice;
        private readonly Iusersatuschangeservice _usersatuschangeservice;
        private readonly Iedituserservice _edituserservice;
        public userscontroller(Igetusersservies getusersservice
         , Igetrolsservice getrolesservice
            , Irgegisteruserservice registeruserservice
            , Iremoveuserservice removeuserservice
            , Iusersatuschangeservice usersatuschangeservice
            , Iedituserservice edituserservice)
        {
            _getusersservice = getusersservice;
            _getRolesService = getrolesservice;
            _registeruserservice = registeruserservice;
            _removeuserservice = removeuserservice;
            _usersatuschangeservice = usersatuschangeservice;
            _edituserservice = edituserservice;
        }
        public IActionResult Index(string serchkey, int page = 1)
        {
            return View(_getusersservice.Execute(new requestgetuserdto
            {
                page = page,
                searchkey = serchkey,
            }));
        }
        [HttpGet]
        public IActionResult create()
        {
            ViewBag.roles = new SelectList(_getRolesService.execute().data, "id", "name");
            return View();
        }
        [HttpPost]
        public IActionResult create(string email, string fullname, long roleid, string password, string repassword)
        {
            var result = _registeruserservice.execute(new requestrgegisteruserdto
            {
                email = email,
                fullname = fullname,
                roles = new List<rolesinrgegisteruserdto>()
                           {
                                new rolesinrgegisteruserdto
                                {
                                     id= roleid
                                }
                           },
                password = password,
                repassword = repassword,
            });
            return Json(result);
        }
        [HttpPost]
        public IActionResult delete(long userid)
        {
            return Json(_removeuserservice.execute(userid));
        }

        [HttpPost]
        public IActionResult usersatuschange(long userid)
        {
            return Json(_usersatuschangeservice.execute(userid));
        }
        [HttpPost]
        public IActionResult edit(long userid, string fullname)
        {
            return Json(_edituserservice.execute(new requestedituserdto
            {
                fullname = fullname,
                userid = userid,
            }));
        }
    }
}