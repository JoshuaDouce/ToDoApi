using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers
{
    //Controller base is a stripped down controller it excludes features you only need in a web app
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot() {
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null)
            };

            return Ok(response);
        }
    }
}
