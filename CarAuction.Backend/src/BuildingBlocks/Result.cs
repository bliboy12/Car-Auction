public class Result<T>
{
    public bool Succeeded { get; }
    public T? Value { get; }
    public IReadOnlyList<string> Errors { get; }

    public Result(bool succeeded, T? value, IReadOnlyList<string> errors)
    {
        Succeeded = succeeded;
        Value = value;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new(true, value, Array.Empty<string>());
    public static Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors.ToList());
}