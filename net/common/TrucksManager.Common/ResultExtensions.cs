using Ardalis.Result;

namespace TrucksManager.Common;

public static class ResultExtensions
{
    public static bool HasValidationErrors(this Result result)
    {
        return result.ValidationErrors.Any();
    }

    public static bool HasValidationErrors<TModel>(this Result<TModel> result)
    {
        return result.ValidationErrors.Any();
    }

    public static string GetResultErrorsFormatted(this Result result)
    {
        return result.Errors.Any()
            ? string.Join(Environment.NewLine, result.Errors)
            : string.Empty;
    }
    public static string GetResultErrorsFormatted<TModel>(this Result<TModel> result)
    {
        return result.Errors.Any()
            ? string.Join(Environment.NewLine, result.Errors)
            : string.Empty;
    }
}