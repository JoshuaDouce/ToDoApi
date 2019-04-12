﻿using Microsoft.AspNetCore.Mvc;

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
            var response = new
            {
                href = Url.Link(nameof(GetRoot), null),
                toDoItems = new {
                    href = Url.Link(nameof(ToDoItemController.GetToDoItems), null)
                }
            };

            return Ok(response);
        }
    }
}
