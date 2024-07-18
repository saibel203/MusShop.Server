namespace MusShop.Domain.Model.ResultItems.ErrorsDocumentation;

public static class TestErrors
{
    public static readonly DomainError DivideByZero = 
        new DomainError("TestErrors.DivideByZero", "You cannot divide by zero!");
}