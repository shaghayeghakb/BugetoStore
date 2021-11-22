using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.domain.entities.products;
using bugeto_stor.common.roles;
using bugeto_stor.domain.entities.users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static bugeto_stor.domain.entities.users.user;
//using static bugeto_stor.domain.entities.users.user;

namespace bugeto_stor.persistence.contexts
{
   public class databasecontext:DbContext, Idatabasecontext
    {
        public databasecontext(DbContextOptions options):base (options)
        {
        }
        public DbSet<user> users { get; set; }
        public DbSet<role> roles { get; set; }
        public DbSet<userinrole> userinroles { get; set; }
        public DbSet<category> categories { get; set; }

        public object productimages => throw new NotImplementedException();

        public object productfeatures { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object products { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed data
            seeddata(modelBuilder);

            // اعمال ایندکس بر روی فیلد ایمیل
            // اعمال عدم تکراری بودن ایمیل
            modelBuilder.Entity<user>().HasIndex(u => u.email).IsUnique();
            //--عدم نمایش اطلاغات حذف شده
            Applyqueryfilter(modelBuilder);
        }

        //افزودن مقادیر پیشفرض به جدول Roles
        private void Applyqueryfilter (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<user>().HasQueryFilter(p => !p.isremoved);
            modelBuilder.Entity<role>().HasQueryFilter(p => !p.isremoved);
            modelBuilder.Entity<userinrole>().HasQueryFilter(p => !p.isremoved);
            modelBuilder.Entity<category>().HasQueryFilter(p => !p.isremoved);

        }
        private void seeddata(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<role>().HasData(new role { id = 1, name = nameof(userroles.admin) });
            modelBuilder.Entity<role>().HasData(new role { id = 2, name = nameof(userroles.operators) });
            modelBuilder.Entity<role>().HasData(new role { id = 3, name = nameof(userroles.customer) });

           

            modelBuilder.Entity<user>().HasQueryFilter(p => !p.isremoved);

        }
   }
}