using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapIntroduction.ViewModels
{
    public class QueryOptions
    {
        public QueryOptions()
        {
            PageSize = 3;
            CurrentPage = 1;
            SortField = "Id";
            SortOrder = SortOrder.ASC;
        }

        [JsonProperty(PropertyName ="sortField")]
        public string SortField { get; set; }

        [JsonProperty(PropertyName ="sortOrder")]
        public SortOrder SortOrder{ get; set; }

        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "pageSize")]
        public int PageSize { get; set; }

        public string Sort {
            get {
                return string.Format("{0} {1}", SortField, SortOrder.ToString());
            }
        }
    }
}