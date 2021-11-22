using System;
using System.IO;

namespace bugeto_stor.application.services.products.commadns.addnewproduct
{
    public class Iformfile
    {
        public int Length { get; internal set; }
        public string filename { get; internal set; }

        internal void CopyTo(FileStream filestream)
        {
            throw new NotImplementedException();
        }
    }
}