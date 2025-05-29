using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;

namespace Application.Errors
{
    public static class UserError
    {
        public static Error InternalServerError => new(ErrorTypeConstant.InternalServerError, "Somthing went wrong");

        public static Error UserNotFound => new(ErrorTypeConstant.NotFoundError, "User not found");

        public static Error UserAlreadyHasRole => new(ErrorTypeConstant.ValidationError, "User already has role");

        public static Error UserHasNotRole => new(ErrorTypeConstant.ValidationError, "User has not role");

        public static Error FailedToAssignRole => new(ErrorTypeConstant.InternalServerError, "Failed to assign role");

        public static Error FailedToRevokeRole => new(ErrorTypeConstant.InternalServerError, "Failed to revoke role");

        public static Error InvalidUserUpdateRequestError(IEnumerable<string> errors) => new(ErrorTypeConstant.ValidationError, string.Join(", ", errors));

        public static Error InvalidUserLoginRequestError(IEnumerable<string> errors) => new(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    }
}
