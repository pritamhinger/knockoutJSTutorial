using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapIntroduction.ViewModels
{
    public class ResultList<P>
    {
        [JsonProperty(PropertyName ="queryOptions")]
        public QueryOptions QueryOptions { get; set; }

        [JsonProperty(PropertyName ="results")]
        public List<P> Results { get; set; }
    }
}