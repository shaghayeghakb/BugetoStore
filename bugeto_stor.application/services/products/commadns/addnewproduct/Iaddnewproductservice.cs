using bugeto_stor.application.interfaces.contexts;
using bugeto_stor.common.dto;
using bugeto_stor.domain.entities.products;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bugeto_stor.application.services.products.commadns.addnewproduct
{
    public interface Iaddnewproductservice
    {
        resultdto execute(requestaddNewproductdto request);
    }
    public class addnewproductservice : Iaddnewproductservice
    {
        private readonly Idatabasecontext _context;
        private readonly Ihostingenvironment _environment;
        private readonly string filename;
        private readonly int Length;


        public addnewproductservice(Idatabasecontext context)
        {
        }

        public addnewproductservice(Idatabasecontext context, Ihostingenvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }
        public resultdto execute(requestaddnewproductdto request)
        {
            try
            {

                var category = _context.categories.Find(request.categoryid);

                product product = new product()
                {
                    brand = request.brand,
                    description = request.description,
                    name = request.name,
                    price = request.price,
                    inventory = request.inventory,
                    category = category,
                    displayed = request.displayed,
                };
                _context.products.Add(product);

                List<productimages> productimages = new List<productimages>();
                foreach (var item in request.images)
                {
                    var uploadedresult = UploadFile(item);
                    productimages.Add(new productimages
                    {
                        product = product,
                        src = uploadedresult.filenameaddress,
                    });
                }

                _context.productimages.AddRange(productimages);


                List<productfeatures> productfeatures = new List<productfeatures>();
                foreach (var item in request.features)
                {
                    productfeatures.Add(new productfeatures
                    {
                        displayname = item.displayname,
                        value = item.value,
                        product = product,
                    });
                }
                _context.productfeatures.AddRange(productfeatures);


                _context.SaveChanges();

                return new resultdto
                {
                    issuccess = true,
                    message = "محصول با موفقیت به محصولات فروشگاه اضافه شد",
                };
            }
            catch (Exception ex)
            {

                return new resultdto
                {
                    issuccess = false,
                    message = "خطا رخ داد ",
                };
            }

        }

        public resultdto execute(requestaddNewproductdto request)
        {
            throw new NotImplementedException();
        }

        resultdto Iaddnewproductservice.execute(requestaddNewproductdto request)
        {
            throw new NotImplementedException();
        }

        public uploaddto UploadFile(Iformfile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsrootfolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsrootfolder))
                {
                    Directory.CreateDirectory(uploadsrootfolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new uploaddto()
                    {
                        status = false,
                        filenameaddress = "",
                    };
                }

                string filename = DateTime.Now.Ticks.ToString() + file.filename;
                var filePath = Path.Combine(uploadsrootfolder, filename);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(filestream);
                }

                return new uploaddto()
                {
                    filenameaddress = folder + filename,
                    status = true,
                };
            }
            return null;
        }
    }
    //private class Iformfile
    //{
    //    public int Length { get; internal set; }

    //    internal void CopyTo(FileStream filestream)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    public class uploaddto
    {
        public long id { get; set; }
        public bool status { get; set; }
        public string filenameaddress { get; set; }
    }
    public class requestaddnewproductdto
    {
        public string name { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int inventory { get; set; }
        public long categoryid { get; set; }
        public bool displayed { get; set; }

        public List<Iformfile> images { get; set; }
        public List<addnewproduct_features> features { get; set; }
        public object Form { get; set; }
    }

    public class addNewproduct_features
    {
        public string displayname { get; set; }
        public string value { get; set; }
    }
}