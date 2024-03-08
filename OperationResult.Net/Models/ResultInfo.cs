using System.Collections.Generic;

namespace OperationResult.Models
{
    /// <summary>
    ///     Contains common info which is necessary for both Succeeded and Failed operation results.
    /// </summary>
    public abstract class ResultInfo
    {
        /// <summary>
        ///     Operation Result's code
        /// </summary>
        public string Code { get; }

        /// <summary>
        ///     The provided messages.
        /// </summary>
        public List<string> Messages { get; }
            = new List<string>();

        /// <summary>
        ///     Additional Arguments of the operation result. 
        ///     They may be considered as metadata: 
        ///     the key of type "string" and the value which is an object.
        /// </summary>
        public Dictionary<string, object> Arguments { get; }
            = new Dictionary<string, object>();

        public ResultInfo(string code)
        {
            Code = code;
        }

        public ResultInfo(
            string code,
            List<string> messages)
        {
            Code = code;
            Messages = messages;
        }

        public ResultInfo(
            string code,
            Dictionary<string, object> arguments)
        {
            Code = code;
            Arguments = arguments;
        }

        public ResultInfo(
            string code,
            List<string> messages,
            Dictionary<string, object> arguments)
        {
            Code = code;
            Messages = messages;
            Arguments = arguments;
        }
    }
}
