using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Models
{
    public class ToDoItemReponse
    {
        public long Id { get; set; }
        public ToDoItem Item { get; set; }
    }
}
