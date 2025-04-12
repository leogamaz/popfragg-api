using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using fromshot_api.Common.Exceptions;

namespace fromshot_api.Common.Helpers;

public static partial class Guard
{
    public static void AgainstTrue(bool condition, string message, string? code = null)
    {
        if (condition)
            throw new BusinessException(message, code ?? ErrorCodes.BusinessError);
    }

    public static void AgainstNull<T>(T? obj, string message, string? code = null) where T : class
    {
        if (obj is null)
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
    }

    public static void AgainstNullOrEmpty(string? value, string message, string? code = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
    }

    public static void AgainstInvalidEmail(string? email, string message = "Email inválido", string? code = null)
    {
        if (string.IsNullOrWhiteSpace(email) || !EmailRegex().IsMatch(email))
        {
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
        }
    }

    public static void AgainstOutOfRange(int value, int min, int max, string message, string? code = null)
    {
        if (value < min || value > max)
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
    }

    public static void AgainstNegative(decimal value, string message, string? code = null)
    {
        if (value < 0)
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
    }

    public static void AgainstNotMatch(string value, string pattern, string message, string? code = null)
    {
        if (!Regex.IsMatch(value, pattern))
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
    }
    public static async Task IfAsync<T>(
        T? value,
        Func<T, Task<bool>> predicate,
        Func<T, Exception> exceptionFactory)
    {
        if (value is null) return;

        if (await predicate(value))
            throw exceptionFactory(value);
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, "pt-BR")]
    private static partial Regex EmailRegex();
}
