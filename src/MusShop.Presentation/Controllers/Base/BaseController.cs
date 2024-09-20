using System.Net;
using Microsoft.AspNetCore.Mvc;
using MusShop.Domain.Model.ResultItems;

namespace MusShop.Presentation.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected IActionResult StatusCode<T>(HttpStatusCode statusCode, T error)
        where T: DomainError
    {
        return statusCode switch
        {
            HttpStatusCode.NotFound => NotFound(error),
            HttpStatusCode.Unauthorized => Unauthorized(error),
            _ => BadRequest(error)
        };
    }
}