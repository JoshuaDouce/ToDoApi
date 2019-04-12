using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoApi.Models;

namespace ToDoApi.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public JsonExceptionFilter(IHostingEnvironment environment)
        {
            _env = environment;
        }
        public void OnException(ExceptionContext context)
        {
            var error = new ApiError();

            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "An internal server error occured";
                error.Detail = context.Exception.Message;
            }


            //serializes the object to JSON
            context.Result = new ObjectResult(error) {
                StatusCode = 500
            };
        }
    }
}
