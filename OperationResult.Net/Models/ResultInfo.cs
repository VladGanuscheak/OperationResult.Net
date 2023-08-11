using System.Collections.Generic;

namespace OperationResult.Models
{
    /// <summary>
    ///     Contains common info which is necessary for both Succeeded and Failed operation results.
    /// </summary>
    public abstract class ResultInfo
    {
        public string Code { get; set; }

        public List<string> Messages { get; }
            = new List<string>();

        public Dictionary<string, object> Arguments { get; }
            = new Dictionary<string, object>();
    }
}
