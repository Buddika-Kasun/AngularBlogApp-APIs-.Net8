using Application.Common.Results;

namespace API.Extensions
{
    public static class ResultExtension
    {
        public static IResult ToHttpResponse(this Result result)
        {
            if(result.IsSuccess)
            {
                return Results.Ok(result);
            }
            else
            {
                return MapErrorResponse(result.Error, result);
            }
        }

        public static IResult ToHttpResponse<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result);
            }
            else
            {
                return MapErrorResponse(result.Error, result);
            }
        }

        public static IResult MapErrorResponse(Error error, object result)
        {
            return error.code switch
            {
                ErrorTypeConstant.ValidationError => Results.BadRequest(result),
                ErrorTypeConstant.NotFoundError => Results.NotFound(result),
                ErrorTypeConstant.ForbiddenError => Results.Forbid(),
                ErrorTypeConstant.UnAuthorizedError => Results.Unauthorized(),
                _ => Results.Problem(detail: error.message, statusCode: 500),
            };
        }
    }
}
