using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog;
using Serilog.Events;

namespace MusShop.Presentation.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger = Log.ForContext<ExceptionHandlerMiddleware>();
    private const string MessageTemplate = "HTTP {Domain} {RequestMethod} {StatusCode} {RequestPath}";

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        HostString domain = httpContext.Request.Host;
        Claim? username = httpContext.User.Claims.FirstOrDefault(c => c.Type == "Username");

        Stream originalResponseBody = httpContext.Response.Body;

        try
        {
            await using MemoryStream responseBodyStream = new MemoryStream();

            httpContext.Response.Body = responseBodyStream;

            await _next(httpContext);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();

            int statusCode = httpContext.Response.StatusCode;

            switch (statusCode)
            {
                case 404 when
                    httpContext.Request.Path == "/" || httpContext.Request.Path.StartsWithSegments("/hangfire") ||
                    httpContext.Request.Path == "/favicon.ico":
                    break;
                case > 399:
                {
                    string headers = GetHeaders(httpContext);

                    _logger.Write(LogEventLevel.Error,
                        $"\n\n{MessageTemplate}\n\n{username}\n\n{responseBody}\n\n{headers}",
                        domain, httpContext.Request.Method, httpContext.Response.StatusCode,
                        httpContext.Request.Path);
                    break;
                }
            }

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalResponseBody);
        }
        catch (Exception ex)
        {
            if (httpContext.Response is not null)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            string headers = GetHeaders(httpContext);

            _logger.Write(LogEventLevel.Fatal,
                $"\n\n{MessageTemplate}\n\n{username}\n\n{ex.Message}\n\n{ex.StackTrace}\n\n{headers}",
                domain, httpContext.Request.Method, httpContext.Response?.StatusCode, httpContext.Request.Path);

            throw;
        }
        finally
        {
            if (httpContext.Response != null)
            {
                httpContext.Response.Body = originalResponseBody;
            }
        }
    }

    private static string GetHeaders(HttpContext httpContext)
    {
        StringBuilder sb = new StringBuilder();
        string separator = new string('=', 20);

        sb.AppendLine("Request Headers");
        foreach (KeyValuePair<string, StringValues> header in httpContext.Request.Headers)
        {
            sb.AppendLine(separator);
            sb.AppendLine($"{header.Key}: {header.Value}");
        }

        sb.AppendLine();
        sb.AppendLine("Response Headers");
        foreach (KeyValuePair<string, StringValues> header in httpContext.Response.Headers)
        {
            sb.AppendLine(separator);
            sb.AppendLine($"{header.Key}: {header.Value}");
        }

        return sb.ToString();
    }
}