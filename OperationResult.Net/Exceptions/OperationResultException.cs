using OperationResult.Results;
using System;

namespace OperationResult.Exceptions
{
    /// <summary>
    ///  OperationResult Exception. It aggregates all errors inside the FailureOperationResult as the InnerException and
    ///  joins all the "Messages" as the content of the Exception.
    /// </summary>
    public class OperationResultException : Exception
    {
        public OperationResultException(FailureOperationResult failureOperationResult)
            : base(string.Join($";{Environment.NewLine}", failureOperationResult.Messages), new AggregateException(failureOperationResult.Errors))
        {
        }
    }
}
