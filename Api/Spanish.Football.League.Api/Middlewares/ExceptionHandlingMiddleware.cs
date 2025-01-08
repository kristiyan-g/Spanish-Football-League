using System.Text.Json;

namespace Spanish.Football.League.Api.Middlewares
{
    /// <summary>
    /// Middleware that handles exceptions thrown during the request processing pipeline.
    /// Catches specific exceptions and logs general exceptions.
    /// Sets appropriate HTTP status codes and responses for different types of errors.
    /// </summary>
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private const string UnexpectedErrorOccured = "An unexpected error occurred. Please, try again.";

        /// <summary>
        /// Invokes the middleware, handling exceptions during the request pipeline execution.
        /// </summary>
        /// <param name="context">The HTTP context representing the current request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                await SetResponseStatusCode(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await SetResponseStatusCode(context, StatusCodes.Status500InternalServerError, UnexpectedErrorOccured);
            }
        }

        /// <summary>
        /// Sets the response status code and the error message in the HTTP response.
        /// Formats the error response as a JSON object.
        /// </summary>
        /// <param name="context">The HTTP context representing the current request.</param>
        /// <param name="statusCode">The HTTP status code to set for the response.</param>
        /// <param name="exceptionMessage">The error message to return in the response body.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static async Task SetResponseStatusCode(HttpContext context, int statusCode, string exceptionMessage)
        {
            context.Response.StatusCode = statusCode;
            var errorResponse = new { message = exceptionMessage };
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
