using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bugeto_stor.common
{
    public static class pageination
    {
        public static IEnumerable<TSource>topaged<TSource>(this IEnumerable<TSource> sources, int page,int pagesize,out int rowscount)
        {
            rowscount = sources.Count();
            return sources.Skip((page - 1) * pagesize).Take(pagesize);
        }
    }

}
