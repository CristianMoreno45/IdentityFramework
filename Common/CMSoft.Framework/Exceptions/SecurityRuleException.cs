using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSoft.Framework.Exceptions
{
    /// <summary>
    /// Exception when trading rules were broken
    /// </summary>
    public class SecurityRuleException : Exception
    {
        public SecurityRuleException()
        {
        }

        public SecurityRuleException(string message) : base(message)
        {

        }

        public SecurityRuleException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
