using System;
using System.Collections.Generic;

namespace OperationResult.Models
{
    /// <summary>
    ///     Contains the information which is necessary for the FailureOperationResult.
    /// </summary>
    public class FailureInfo : ResultInfo
    {
        /// <summary>
        ///     The list of errors (exceptions) which have been occured.
        /// </summary>
        public List<Exception> Errors { get; }
            = new List<Exception>();
    }
}
