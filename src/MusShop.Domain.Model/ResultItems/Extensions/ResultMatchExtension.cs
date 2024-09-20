namespace MusShop.Domain.Model.ResultItems.Extensions;

public static class ResultMatchExtension
{
    public static T Match<TResult, T>(
        this DomainResult<TResult> domainResult,
        Func<T> onSuccess,
        Func<DomainError, T>? onError,
        Func<IEnumerable<DomainError>, T>? onErrors)
    {
        return domainResult.IsSuccess
            ? onSuccess()
            : !string.IsNullOrWhiteSpace(domainResult.Error.Description) ||
                                      !string.IsNullOrWhiteSpace(domainResult.Error.Code)
                ? onError!(domainResult.Error)
                : onErrors!(domainResult.Errors);
    }
}