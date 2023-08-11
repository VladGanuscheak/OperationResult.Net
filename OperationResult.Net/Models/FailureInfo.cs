using System;
using System.Collections.Generic;

namespace OperationResult.Models
{
    /// <summary>
    ///     Contains the information which is necessary for the FailureOperationResult.
    /// </summary>
    public class FailureInfo : ResultInfo
    {
        public List<Exception> Errors { get; }
            = new List<Exception>();
    }
}
