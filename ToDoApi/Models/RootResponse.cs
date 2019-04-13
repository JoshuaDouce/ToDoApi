using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class RootResponse : Resource
    {
        public Link ToDoItems { get; set; }
    }
}
