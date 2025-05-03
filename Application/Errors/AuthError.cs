using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Results;

namespace Application.Errors
{
    public static class AuthError
    {
        public static Error InvalidRegisterRequest => new(ErrorTypeConstant.ValidationError, "Invalid register request");
        public static Error UserAlreadyExist => new(ErrorTypeConstant.ValidationError, "User already exist");
        public static Error InvalidLoginRequest => new(ErrorTypeConstant.ValidationError, "Invalid login request");
        public static Error UserNotFound => new(ErrorTypeConstant.NotFoundError, "User not found");
        public static Error InvalidPassword => new(ErrorTypeConstant.ValidationError, "Invalid password");
        public static Error CreateInvalidRequestError(IEnumerable<string> errors) => new(ErrorTypeConstant.ValidationError, string.Join(", ", errors));
    }
}
