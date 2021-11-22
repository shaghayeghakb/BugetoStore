using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace endpoint.site.Controllers.models.viewmodels.authenticationviewmodel
{
    public class signupviewmodel
    {
        public string fullname { get; set; } = "";
        public string email { get; set; }
        public string password { get; set; }
        public string repassword { get; set; }
    }
}