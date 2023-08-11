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
        public string Code { get; set; }

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
    }
}
