using System;
using System.Collections.Generic;

namespace OperationResult.Models
{
    /// <summary>
    ///     Contains the information which is necessary for the FailureOperationResult.
    /// </summary>
    public class FailureInfo : ResultInfo
    {
        public FailureInfo(string code)
            : base(code)
        {
        }

        public FailureInfo(
            string code,
            List<string> messages)
            : base(code, messages)
        {
        }

        public FailureInfo(
            string code,
            Dictionary<string, object> arguments)
            : base(code, arguments)
        {
        }

        public FailureInfo(
            string code,
            List<string> messages,
            Dictionary<string, object> arguments)
            : base(code, messages, arguments)
        {
        }

        public FailureInfo(string code, 
            List<Exception> errors) 
            : base(code)
        {
            Errors = errors;
        }

        public FailureInfo(string code, 
            List<string> messages,
            List<Exception> errors) 
            : base(code, messages)
        {
            Errors = errors;
        }

        public FailureInfo(string code, 
            Dictionary<string, object> arguments,
            List<Exception> errors) 
            : base(code, arguments)
        {
            Errors = errors;
        }

        public FailureInfo(string code, 
            List<string> messages, 
            Dictionary<string, object> arguments,
            List<Exception> errors) 
            : base(code, messages, arguments)
        {
            Errors = errors;
        }

        /// <summary>
        ///     The list of errors (exceptions) which have been occured.
        /// </summary>
        public List<Exception> Errors { get; }
            = new List<Exception>();
    }
}
