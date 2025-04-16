using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using popfragg.Common.Exceptions;

namespace popfragg.Common.Helpers;

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

    public static void AgainstInvalidPassword(string? email, string message = "Padrão de senha incorreto", string? code = null)
    {
        if (string.IsNullOrWhiteSpace(email) || !PasswordRegex().IsMatch(email))
        {
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
        }
    }
    public static void AgainstInvalidNickname(string? email, string message = "O Nickname contém caracteres não permitidos", string? code = null)
    {
        if (string.IsNullOrWhiteSpace(email) || !NicknameRegex().IsMatch(email))
        {
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
        }
    }
    public static void AgainstInvalidName(string? email, string message = "O nome ou sobrenome não é permitido", string? code = null)
    {
        if (string.IsNullOrWhiteSpace(email) || !NameRegex().IsMatch(email))
        {
            throw new ValidationException(message, code ?? ErrorCodes.ValidationError);
        }
    }
    public static void AgainstConflicts<T>(T conflictDto, Dictionary<string, (string message, string errorCode)> mapping)
    {
        if (conflictDto is null)
            throw new ArgumentNullException(nameof(conflictDto));

        var type = typeof(T);
        foreach (var entry in mapping)
        {
            var prop = type.GetProperty(entry.Key);
            if (prop == null)
                continue;

            var value = prop.GetValue(conflictDto) as string;

            if (!string.IsNullOrWhiteSpace(value))
                throw new BusinessException(entry.Value.message, entry.Value.errorCode);
        }
    }

    [GeneratedRegex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex EmailRegex();

    [GeneratedRegex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*#?&^_\\-])[A-Za-z\\d@$!%*#?&^_\\-]{6,}$", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex PasswordRegex();

    [GeneratedRegex("^[A-Za-zÀ-ÖØ-öø-ÿ0-9_-]{4,20}$", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex NicknameRegex();

    [GeneratedRegex("^[A-Za-zÀ-ÖØ-öø-ÿ' -]{2,}$", RegexOptions.IgnoreCase, "pt-BR")]
    public static partial Regex NameRegex();

}
