using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace Codacy.Proof.SharedKernel.Endpoints;
public static class ProblemExtensions
{
    public static IResult ProblemDetails(string title = "VALIDATION_FAILURE",
        int statusCode = StatusCodes.Status400BadRequest,
        string detail = "",
        Dictionary<string, object?>? errors = null)
    {
        return Results.Problem(title: title, detail: detail, statusCode: statusCode, extensions: errors);
    }


    public static IResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Results.Problem("A problem has occurred. Please try again later.",
                statusCode: StatusCodes.Status500InternalServerError);
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    private static IResult ValidationProblem(List<Error> errors)
    {
        var errorList = new Dictionary<string, List<string>>();

        foreach (var error in errors)
        {
            if (errorList.TryGetValue(error.Code, out var errorDescriptions))
            {
                errorDescriptions.Add(error.Description);
            }
            else
            {
                errorList.Add(error.Code, [error.Description]);
            }
        }

        var errorArrayDict = errorList.ToDictionary(kvp =>
            kvp.Key, kvp => kvp.Value.ToArray());

        return Results.ValidationProblem(errorArrayDict);
    }

    private static IResult Problem(Error error)
    {
        var code = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(detail: error.Description, statusCode: code, title: error.Code);
    }

}
