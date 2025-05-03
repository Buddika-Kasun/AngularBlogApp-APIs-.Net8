using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Results
{
    public class ErrorTypeConstant
    {
        public const string None = "None";
        public const string ValidationError = "ValidationError";
        public const string UnAuthorizedError = "UnAuthorizedError";
        public const string NotFoundError = "NotFoundError";
        public const string InternalServerError = "InternalServerError";
        public const string ForbiddenError = "ForbiddenError";
    }
}
