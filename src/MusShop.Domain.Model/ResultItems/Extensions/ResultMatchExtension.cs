namespace MusShop.Domain.Model.ResultItems.Extensions;

public static class ResultMatchExtension
{
    public static T Match<TResult, T>(
        this DomainResult<TResult> domainResult,
        Func<T> onSuccess,
        Func<DomainError, T> onError)
    {
        return domainResult.IsSuccess ? onSuccess() : onError(domainResult.Error);
    }
}