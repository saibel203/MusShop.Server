namespace MusShop.Domain.Model.ResultItems;

public class DomainResult<TResult>
{
    private readonly TResult? _result;

    private DomainResult(TResult value)
    {
        Value = value;
        IsSuccess = true;
        Error = DomainError.None;
    }

    private DomainResult(DomainError error)
    {
        if (error == DomainError.None)
        {
            throw new ArgumentException("invalid error", nameof(error));
        }

        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public DomainError Error { get; }

    public TResult Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException("there is no value for failure");
            }

            return _result!;
        }

        private init => _result = value;
    }

    public static DomainResult<TResult> Success(TResult value)
    {
        return new DomainResult<TResult>(value);
    }

    public static DomainResult<TResult> Failure(DomainError error)
    {
        return new DomainResult<TResult>(error);
    }
}