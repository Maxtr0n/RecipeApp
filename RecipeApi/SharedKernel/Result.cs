using System.Diagnostics.CodeAnalysis;

namespace SharedKernel;

public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;

    public ApplicationError? Error { get; set; }

    protected Result(bool isSuccess, ApplicationError? error)
    {
        if (isSuccess && error != null)
        {
            throw new InvalidOperationException();
        }
        if (!isSuccess && error == null)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    public static Result Failure(ApplicationError error) => new(false, error);

    public static Result<T> Failure<T>(ApplicationError error) => Result<T>.Failure(error);
}

[SuppressMessage(
    "Design",
    "CA1000:Do not declare static members on generic types",
    Justification = "Result<T> factory methods are intentional and improve usability.")]
public class Result<T> : Result
{
    public T? Value { get; set; }

    protected Result(bool isSuccess, T? value, ApplicationError? error)
        : base(isSuccess, error)
    {
        Value = value;
    }
    public static Result<T> Success(T value) => new(true, value, default);
    public static new Result<T> Failure(ApplicationError error) => new(false, default, error);
}