using System.Collections.Generic;

namespace OperationResult.Models
{
    /// <summary>
    ///     Contains the information which is necessary for the SuccessOperationResult.
    /// </summary>
    public class SuccessInfo : ResultInfo
    {
        public SuccessInfo(string code) 
            : base(code)
        {
        }

        public SuccessInfo(string code, List<string> messages) 
            : base(code, messages)
        {
        }

        public SuccessInfo(string code, Dictionary<string, object> arguments) 
            : base(code, arguments)
        {
        }

        public SuccessInfo(string code, List<string> messages, Dictionary<string, object> arguments) 
            : base(code, messages, arguments)
        {
        }
    }
}
