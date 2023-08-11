using OperationResult.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OperationResult.Results
{
    public class FailureOperationResult : OperationResult
    {
        /// <summary>
        ///     Constructor for the Failed Operation Result.
        /// </summary>
        public FailureOperationResult()
        {
        }

        /// <summary>
        ///     Constructor for the Failed Operation Result.
        /// </summary>
        /// <param name="failureInfo">Required. The information about the Failed Operation Result.</param>
        public FailureOperationResult(
            [Required] FailureInfo failureInfo)
                : base(failureInfo)
        {
            if (failureInfo.Errors?.Any() ?? false)
            {
                WithErrors(failureInfo.Errors);
            }
        }

        /// <summary>
        ///     The list of errors (exceptions) which have been occured.
        /// </summary>
        public List<Exception> Errors { get; }
            = new List<Exception>();

        /// <summary>
        ///     Adds the specified errors (exceptions) to the corresponding Operation Result's collection.
        ///     Ensures that the collection is not empty.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when the provided collection of exceptions is empty.</exception>
        /// <param name="exceptions">Required. The list of exceptions.</param>
        /// <returns>FailureOperationResult</returns>
        public FailureOperationResult WithErrors([Required] List<Exception> exceptions)
        {
            if (!exceptions.Any())
            {
                throw new ArgumentException();
            }

            Errors.AddRange(exceptions);

            return this;
        }

        /// <summary>
        ///     Adds the specified errors (exceptions) to the corresponding Operation Result's collection.
        ///     Ensures that the collection is not empty.
        /// </summary>
        /// <param name="exception">Required. The specified exception.</param>
        /// <returns>FailureOperationResult</returns>
        public FailureOperationResult WithError([Required] Exception exception)
            => WithErrors(new List<Exception> { exception });
    }

    public class FailureOperationResult<TData> : OperationResult<TData>
    {
        /// <summary>
        ///     Constructor for the Failed Operation Result.
        /// </summary>
        public FailureOperationResult()
        {
        }

        /// <summary>
        ///     Constructor for the Failed Operation Result.
        /// </summary>
        /// <param name="failureInfo">Required. The information about the Failed Operation Result.</param>
        public FailureOperationResult(
            [Required] FailureInfo failureInfo)
                : base()
        {
            if (failureInfo.Errors?.Any() ?? false)
            {
                WithErrors(failureInfo.Errors);
            }
        }

        /// <summary>
        ///     The list of errors (exceptions) which have been occured.
        /// </summary>
        public List<Exception> Errors { get; }
            = new List<Exception>();

        /// <summary>
        ///     Adds the specified errors (exceptions) to the corresponding Operation Result's collection.
        ///     Ensures that the collection is not empty.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when the provided collection of exceptions is empty.</exception>
        /// <param name="exceptions">Required. The list of exceptions.</param>
        /// <returns>FailureOperationResult<TData></returns>
        public FailureOperationResult<TData> WithErrors([Required] List<Exception> exceptions)
        {
            if (!exceptions.Any())
            {
                throw new ArgumentException();
            }

            Errors.AddRange(exceptions);

            return this;
        }

        /// <summary>
        ///     Adds the specified errors (exceptions) to the corresponding Operation Result's collection.
        ///     Ensures that the collection is not empty.
        /// </summary>
        /// <param name="exception">Required. The specified exception.</param>
        /// <returns>FailureOperationResult<TData></returns>
        public FailureOperationResult<TData> WithError([Required] Exception exception)
            => WithErrors(new List<Exception> { exception });

        #region Fluent Operation's Result
        /// <summary>
        ///     Sets the code to the operation result.
        ///     The method ensures that the "code" is not null, empty or white space.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <param name="code">Required. Operation Result's code.</param>
        /// <returns>FailureOperationResult<TData></returns>
        public new FailureOperationResult<TData> WithCode([Required] string code)
        {
            ((OperationResult)this).WithCode(code);

            return this;
        }

        /// <summary>
        ///     Adds the specified messages to the corresponding Operation Result's collection.
        ///     Ensures that each item in the collection is not null, empty or white space.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when one of the messages is null.</exception>
        /// <exception cref="ArgumentException">Throws ArgumentException when one of the messages is empty or contains only whitespaces.</exception>
        /// <param name="messages">Required. The list of messages.</param>
        /// <returns>FailureOperationResult<TData></returns>
        public new FailureOperationResult<TData> WithMessages([Required] List<string> messages)
        {
            ((OperationResult)this).WithMessages(messages);

            return this;
        }

        /// <summary>
        ///     Adds the specified message to the corresponding Operation Result's collection.
        ///     Ensures that the message is not null, empty or white space.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when the message is null.</exception>
        /// <exception cref="ArgumentException">Throws ArgumentException when the message is empty or contains only whitespaces.</exception>
        /// <param name="message">Required. The message.</param>
        /// <returns>FailureOperationResult<TData></returns>
        public new FailureOperationResult<TData> WithMessage([Required] string message)
            => WithMessages(new List<string> { message });

        /// <summary>
        ///     Adds the specified arguments (metadata) to the corresponding Operation Result's collection.
        ///     Ensures that the collection is not empty.
        ///     Ensures that the existing metadata do not contain one of the keys from the input patameter.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when one of the arguments is already contained inside the Operation Result's metadata or
        /// the passed collection do not contain any element.</exception>
        /// <param name="arguments">Required. The collection of arguments. 
        /// The items of the corresponding collections will be added to the existing Operatio Result's metadata.</param>
        /// <returns>FailureOperationResult<TData></returns>
        public new FailureOperationResult<TData> WithArguments([Required] Dictionary<string, object> arguments)
        {
            ((OperationResult)this).WithArguments(arguments);

            return this;
        }

        /// <summary>
        ///     Adds the specified argument (metadata) to the corresponding Operation Result's collection.
        ///     Ensures that the existing metadata do not contain the key from the input patameter.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when the key is already contained inside the Operation Result's metadata.</exception>
        /// <param name="key">Required. The key of the argument (metadata).</param>
        /// <param name="value">Optional. The value of the argument (metadata).</param>
        /// <returns>FailureOperationResult<TData></returns>
        public new FailureOperationResult<TData> WithArgument([Required] string key, object value)
        {
            ((OperationResult)this).WithArgument(key, value);

            return this;
        }

        /// <summary>
        ///     Adds the specified argument (metadata) to the corresponding Operation Result's collection.
        ///     Ensures that the existing metadata do not contain the key from the input patameter.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when the key is already contained inside the Operation Result's metadata.</exception>
        /// <param name="argument">Required. The argument (metadata).</param>
        /// <returns>FailureOperationResult<TData></returns>
        public new FailureOperationResult<TData> WithArgument([Required] KeyValuePair<string, object> argument)
            => WithArgument(argument.Key, argument.Value);

        #endregion
    }
}
