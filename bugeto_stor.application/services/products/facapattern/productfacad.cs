using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.application.interfaces.facadpatterns;
using bugeto_stor.application.services.products.commadns.addnewcategory;
using bugeto_stor.application.services.products.commadns.addnewproduct;
using bugeto_stor.application.services.products.queries.getallcategories;
using bugeto_stor.application.services.products.queries.getproductforadmin;
//using bugeto_stor.application.services.products.queries.getcategoriey;
using bugeto_stor.application.services.products.queries.getproductforadmin;
//using bugeto_stor.application.services.products.queries.
//using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace bugeto_stor.application.services.products.facapattern
{
    public class productfacad : Iproductfacad
    {
        private readonly Idatabasecontext _context;
        private readonly Ihostingenvironment _environment;
        private readonly Iaddnewcategoryservice _addnewcategoryservice;

        public productfacad(Idatabasecontext context, Ihostingenvironment hostingenvironment)
        {
            _context = context;
            _environment = hostingenvironment;

        }
        private addnewcategoryservice _addnewcategory;
        private addnewcategoryservice addnewcategoryservice
        {
            get
            {
                return _addnewcategory = _addnewcategory ?? new addnewcategoryservice(_context);
            }
        }
        private Igetcategoriesservice _getcategoriesservice;

        public Igetcategoriesservice getcategoriesservice
        {
            get
            {
                return _getcategoriesservice = _getcategoriesservice ?? new getcategoriesservice(_context);
            }
        }
        private addnewproductservice _addnewproductservice;
        public addnewproductservice addnewproductservice
        {
            get
            {
                return _addnewproductservice = _addnewproductservice ?? new addnewproductservice(_context);
            }
        }
        private igetallcategoryservice _getallcategoryservice;
        public igetallcategoryservice getallcategoryservice
        {
            get
            {
                return _getallcategoryservice = _getallcategoryservice ?? new igetallcategoryservice(_context);
            }
        }
        private Igetproductforadminservice _getproductforadminservice;
        public Igetproductforadminservice getproductforadminservice
        {
            get
            {
                return _getproductforadminservice = _getproductforadminservice ?? new getproductforadminservice(_context);
            }
        }
        private Igetproductdetailforadminservice _getgetproductdetailforadminservice;
        public Igetproductdetailforadminservice getproductdetailforadminservice
        {
            get
            {
                return _getgetproductdetailforadminservice = _getgetproductdetailforadminservice ?? new getproductdetailforadminservice(_context);
            }
        }
   //     addnewcategoryservice Iproductfacad.addnewcategoryservice => throw new NotImplementedException();

            //Igetcategoriesservice Iproductfacad.getcategoriesservice => throw new NotImplementedException();

        public object getallcategoriesservice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Igetcategoriesservice Iproductfacad.getcategoriesservice => throw new NotImplementedException();

        Igetproductforadminservice Iproductfacad.getproductforadminservice => throw new NotImplementedException();

        Igetproductdetailforadminservice Iproductfacad.getproductdetailforadminservice => throw new NotImplementedException();

        addnewcategoryservice Iproductfacad.addnewcategoryservice => throw new NotImplementedException();
    }
}