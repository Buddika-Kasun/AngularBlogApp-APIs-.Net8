using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Results
{
    public sealed record Error(string code, string message)
    {
        internal static Error None => new(ErrorTypeConstant.None, string.Empty);
    }
}
