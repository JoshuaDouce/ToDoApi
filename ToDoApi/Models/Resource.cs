using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public abstract class Resource
    {
        //will alway be at the top on reosurces
        [JsonProperty(Order = -2)]
        public string Href { get; set; }
    }
}
