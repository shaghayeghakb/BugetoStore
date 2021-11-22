using bugeto_stor.domain.entities.commands;

namespace bugeto_stor.domain.entities.products
{
 public   class productfeatures:baseentity
    {
        public virtual product product { get; set; }
        public long productid { get; set; }
        public string displayname { get; set; }
        public string value { get; set; }
    }
}
