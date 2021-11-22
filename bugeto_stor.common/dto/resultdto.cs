using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace bugeto_stor.common.dto
{
  public  class resultdto
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
    }

    public class resultdto<T>
    {
        public bool issuccess { get; set; }
       // public bool IsSuccess { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
