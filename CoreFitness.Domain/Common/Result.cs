using CoreFitness.Domain.Exceptions;

namespace CoreFitness.Domain.Common
{
    //TODO: Finalize Result
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        protected Result(bool isSuccess, string? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, null);
        public static Result Failure(string error) => new(false, error);

        public static async Task<Result> TryAsync(Func<Task> action)
        {
            try
            {
                await action();
                return Success();
            }
            catch (DomainException ex)
            {
                return Failure(ex.Message);
            }
        }
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T? value, string? error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, value, null);
        public static new Result<T> Failure(string error) => new(false, default, error);

        public static async Task<Result<T>> TryAsync(Func<Task<T>> action)
        {
            try
            {
                var value = await action();
                return Success(value);
            }
            catch (DomainException ex)
            {
                return Failure(ex.Message);
            }
        }
    }
}
