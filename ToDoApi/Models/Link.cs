using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class Link
    {
        public const string GetMethod = "GET";

        [JsonProperty(Order = -4)]
        public string Href { get; set; }
        [JsonProperty(Order = -3, 
            NullValueHandling = NullValueHandling.Ignore)]
        public string[] Relations { get; set; }
        [JsonProperty(Order = -2, 
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; }
        //Stores route name before being rewritten
        [JsonIgnore]
        public string RouteName { get; set; }
        //Stores route values before being rewritten
        [JsonIgnore]
        public object Values { get; set; }

        public static Link To(string routeName, object routeValues = null) {
            return new Link() {
                RouteName = routeName,
                Values = routeValues,
                Method = GetMethod,
                Relations = null
            };
        }
    }
}
