using bugeto_stor.domain.entities.products;
using bugeto_stor.domain.entities.users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;  
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static bugeto_stor.domain.entities.users.user;

namespace bugeto_stor.application.interfaces.contexts
{
  public  interface Idatabasecontext 
    {
         DbSet<user> users { get; set; }
         DbSet<role> roles { get; set; }
         DbSet<userinrole> userinroles { get; set; }
        DbSet<category> categories { get; set; }
        object productimages { get; }
        object productfeatures { get; set; }
        object products { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangenOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken=new CancellationToken());
    }
}
