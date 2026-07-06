namespace ICMON.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string? Error { get; }
    public List<string> Errors { get; } = new();

    private Result(bool isSuccess, T? data, string? error = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    public static Result<T> Success(T data) => new(true, data);

    public static Result<T> Failure(string error) => new(false, default, error);

    public static Result<T> Failure(IEnumerable<string> errors)
    {
        var errorList = errors.ToList();
        return new Result<T>(false, default, string.Join("; ", errorList));
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<string, TResult> onFailure)
        => IsSuccess ? onSuccess(Data!) : onFailure(Error!);
}
