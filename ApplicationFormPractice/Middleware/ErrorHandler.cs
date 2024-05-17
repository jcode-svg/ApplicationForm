using ApplicationFormPractice.SharedKernel.GenericModels;
using Newtonsoft.Json;
using System.Net;

namespace ApplicationFormPractice.API.Middleware;

public class ErrorHandler
{
    private readonly RequestDelegate _next;

    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            Console.WriteLine($"Error handler caught exception => {error.StackTrace}");
            switch (error)
            {
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var responseBody = ResponseWrapper<string>.Error("Something went wrong, your request could not be processed.");

            await response.WriteAsync(JsonConvert.SerializeObject(responseBody));
        }
    }
}
