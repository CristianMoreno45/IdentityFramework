using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSoft.Framework.Exceptions
{
    public class IncompleteOperationException : Exception
    {
        public IEnumerable<string>? Errors { get; set; }
        public IncompleteOperationException()
        {
            Errors = new List<string>();
        }
        public IncompleteOperationException(string message) : base(message)
        {

        }

        public IncompleteOperationException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}

