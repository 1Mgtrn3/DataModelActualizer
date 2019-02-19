using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDetective
{
    class PopularityAnalyzer
    {
        public PopularityAnalyzer()
        {

        }

        public List<Link> GetOverusedLinks(int limit) {
            var result = new List<Link>();
            using (var storage = new StorageContext(StorageContext.constr))
            {
                result = storage.links.Where(l => l.popularity >= limit).ToList();
            }

            return result;
        }

        public List<Link> Get_N_MostPopular(int N) {

            var result = new List<Link>();
            using (var storage = new StorageContext(StorageContext.constr))
            {
                result = (from l in storage.links
                          orderby l.popularity descending
                          select l).Take(N).ToList();
            }

            return result;
        }
    }
}
