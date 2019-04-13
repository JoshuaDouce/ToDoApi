using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    //Controller base is a stripped down controller it excludes features you only need in a web app
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot() {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                ToDoItems = Link.To(nameof(ToDoItemController.GetToDoItems))
            };

            return Ok(response);
        }
    }
}
