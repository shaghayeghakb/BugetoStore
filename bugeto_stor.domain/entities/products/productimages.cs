using bugeto_stor.domain.entities.commands;

namespace bugeto_stor.domain.entities.products
{
  public  class productimages:baseentity
    {
        public virtual product product { get; set; }
        public long productid { get; set; }
        public string src { get; set; }
    }
}
