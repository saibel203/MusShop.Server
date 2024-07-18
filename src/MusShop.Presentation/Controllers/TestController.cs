using Microsoft.AspNetCore.Mvc;
using MusShop.Domain.Model.ResultItems;
using MusShop.Domain.Model.ResultItems.ErrorsDocumentation;
using MusShop.Domain.Model.ResultItems.Extensions;
using MusShop.Presentation.Controllers.Base;

namespace MusShop.Presentation.Controllers;

public class TestController : BaseController
{
    [HttpGet("Test")]
    public IActionResult Test(int secondNumber)
    {
        var x = 1;

        DomainResult<decimal> test = TestMethod(x, secondNumber);

        return test.Match<decimal, IActionResult>(
            onSuccess: () => Ok(test.Value),
            onError: BadRequest);
    }

    [HttpGet("Test2")]
    public IActionResult Test2()
    {
        return Ok("Hello world!");
    }

    private DomainResult<decimal> TestMethod(int firstNumber, int secondNumber)
    {
        if (secondNumber == 0)
        {
            return DomainResult<decimal>.Failure(TestErrors.DivideByZero);
        }

        return DomainResult<decimal>.Success(firstNumber / secondNumber);
    }
}