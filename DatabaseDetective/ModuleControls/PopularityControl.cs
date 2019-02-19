using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DatabaseDetective
{
    class PopularityControl
    {
        private PopularityAnalyzer analyzer { get; set; }
        public PopularityControl()
        {
            analyzer = new PopularityAnalyzer();
        }

        public string toJson(object o, bool Indented)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            if (Indented)
            {
                settings.Formatting = Formatting.Indented;
            }


            return JsonConvert.SerializeObject(o, settings);


        }

        public List<Link> GetOverusedLinks(int limit)
        {
            return analyzer.GetOverusedLinks(limit);

        }

        public List<Link> Get_N_MostPopular(int N) {

            return analyzer.Get_N_MostPopular(N);
        }

        public string GetOverusedLinksJson(int limit, bool Indented)
        {
            return toJson(GetOverusedLinks(limit), Indented);

        }

        public string Get_N_MostPopularJson(int N, bool Indented) {
            return toJson(Get_N_MostPopular(N), Indented);
        }
    }
}
