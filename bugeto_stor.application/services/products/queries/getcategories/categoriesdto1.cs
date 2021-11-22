namespace bugeto_stor.application.services.products.queries.getallcategories
{
    public class categoriesdto
        {
            public long id { get; set; }
            public string name { get; set; }
            public bool haschiid { get; set; }
            public parentcategorydto parent { get; set; }

        }
    }

