using OperationResult.Extensions;
using OperationResult.Models;
using OperationResult.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OperationResult
{
    public abstract class OperationResult
    {
        #region succeeded Operation Result
        /// <summary>
        ///     Get succeeded Operation Result.
        /// </summary>
        /// <returns>SuccessOperationResult</returns>
        public static SuccessOperationResult Succeeded()
        {
            var operationResult = new SuccessOperationResult();

            return operationResult;
        }

        /// <summary>
        ///     Get succeeded Operation Result.
        /// </summary>
        /// <param name="successInfo">Required. The model which contains information of Result Code, provided messages 
        /// and additional arguments.</param>
        /// <returns>SuccessOperationResult</returns>
        public static SuccessOperationResult Succeeded([Required] SuccessInfo successInfo)
        {
            var operationResult = new SuccessOperationResult(successInfo);

            return operationResult;
        }

        /// <summary>
        ///     Get succeeded Operation Result.
        /// </summary>
        /// <typeparam name="TData">The type of the data</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>SuccessOperationResult<TData></returns>
        public static SuccessOperationResult<TData> Succeeded<TData>(TData data)
        {
            var operationResult = new SuccessOperationResult<TData>(data);

            return operationResult;
        }

        /// <summary>
        ///     Get succeeded Operation Result.
        /// </summary>
        /// <typeparam name="TData">The type of the data</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="successInfo">Required. The model which contains information of Result Code, provided messages 
        /// and additional arguments.</param>
        /// <returns>SuccessOperationResult<TData></returns>
        public static SuccessOperationResult<TData> Succeeded<TData>(TData data, 
            [Required] SuccessInfo successInfo)
        {
            var operationResult = new SuccessOperationResult<TData>(data, successInfo);

            return operationResult;
        }

        #endregion

        #region Failed Operation Result
        /// <summary>
        ///     Get failed Operation Result.
        /// </summary>
        /// <returns>Failed Operation Result with default values.</returns>
        public static FailureOperationResult Failed()
        {
            var operationResult = new FailureOperationResult();

            return operationResult;
        }

        /// <summary>
        ///     Get failed Operation Result.
        /// </summary>
        /// <param name="failureInfo">Required. The model which contains information of Result Code, provided messages, 
        /// additional arguments and errors.</param>
        /// <returns></returns>
        public static FailureOperationResult Failed([Required] FailureInfo failureInfo)
        {
            var operationResult = new FailureOperationResult(failureInfo);

            return operationResult;
        }

        #endregion

        #region Constrcutors
        protected OperationResult()
        {
        }

        protected OperationResult([Required] ResultInfo resultInfo)
        {
            if (!string.IsNullOrEmpty(resultInfo.Code))
            {
                WithCode(resultInfo.Code);
            }

            if (resultInfo.Messages?.Any() ?? false)
            {
                WithMessages(resultInfo.Messages);
            }

            if (resultInfo.Arguments?.Count > 0)
            {
                WithArguments(resultInfo.Arguments);
            }
        }
        #endregion

        #region Core
        private string _code;

        /// <summary>
        ///     Operation Result's code
        /// </summary>
        public string Code => _code;

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

        /// <summary>
        ///     Checks if the operation result has succeeded. 
        ///     It will be either of type SuccessOperationResult, either of type SuccessOperationResult<TData>.
        /// </summary>
        public bool HasSucceeded => GetType()
            .IsIn(typeof(SuccessOperationResult), typeof(SuccessOperationResult<>));

        /// <summary>
        ///     Checks if the operation result has Failed. 
        ///     It will be either of type FailureOperationResult, either of type FailureOperationResult<TData>.
        /// </summary>
        public bool HasFailed => GetType()
            .IsIn(typeof(FailureOperationResult), typeof(FailureOperationResult<>));

        /// <summary>
        ///     Combines two results into one.
        ///     1. If both results have succeeded, the first one is returned. 
        ///     2. If the base result has failed, it is returned. 
        ///     3. If the base result has succeeded and the second one not, the second result is returned.
        ///     4. If both results have failed, the final result will contain error messages and arguments (metadata) from both sources 
        ///     (The "Code" will be inherited from the base result).  
        /// </summary>
        /// <param name="operationResult">Required. The second operationresult (the one which will be combined with the base one).</param>
        /// <returns>OperationResult</returns>
        public OperationResult Combine([Required] OperationResult operationResult)
        {
            if (HasSucceeded)
            {
                if (operationResult.HasSucceeded)
                {
                    return this;
                }
                else
                {
                    return operationResult;
                }
            }
            else
            {
                if (operationResult.HasSucceeded)
                {
                    return this;
                }
                else
                {
                    var firstFailure = this as FailureOperationResult;
                    var secondFailure = operationResult as FailureOperationResult;

                    var failureResult = new FailureOperationResult();

                    failureResult.WithErrors(firstFailure.Errors)
                        .WithErrors(secondFailure.Errors)
                        .WithCode(firstFailure.Code)
                        .WithArguments(firstFailure.Arguments)
                        .WithMessages(firstFailure.Messages)
                        .WithArguments(secondFailure.Arguments)
                        .WithMessages(secondFailure.Messages);

                    return failureResult;
                }
            }
        }
        #endregion

        #region Fluent Operation's Result
        /// <summary>
        ///     Sets the code to the operation result.
        ///     The method ensures that the "code" is not null, empty or white space.
        /// </summary>
        /// <param name="code">Required. Operation Result's code.</param>
        /// <returns>OperationResult</returns>
        public OperationResult WithCode([Required] string code)
        {
            EnsureNotEmptyArgument(code);

            _code = code;

            return this;
        }

        /// <summary>
        ///     Adds the specified messages to the corresponding Operation Result's collection.
        ///     Ensures that each item in the collection is not null, empty or white space.
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when one of the messages is null.</exception>
        /// <exception cref="ArgumentException">Throws ArgumentException when one of the messages is empty or contains only whitespaces.</exception>
        /// <param name="messages">Required. The list of messages.</param>
        /// <returns>OperationResult</returns>
        public OperationResult WithMessages([Required] List<string> messages)
        {
            foreach (var message in messages)
            {
                EnsureNotEmptyArgument(message);
            }

            Messages.AddRange(messages);

            return this;
        }

        /// <summary>
        ///     Adds the specified message to the corresponding Operation Result's collection.
        ///     Ensures that the message is not null, empty or white space.
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException when the message is null.</exception>
        /// <exception cref="ArgumentException">Throws ArgumentException when the message is empty or contains only whitespaces.</exception>
        /// <param name="message">Required. The message.</param>
        /// <returns>OperationResult</returns>
        public OperationResult WithMessage([Required] string message)
            => WithMessages(new List<string> { message });

        /// <summary>
        ///     Adds the specified arguments (metadata) to the corresponding Operation Result's collection.
        ///     Ensures that the collection is not empty.
        ///     Ensures that the existing metadata do not contain one of the keys from the input patameter.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when one of the arguments is already contained inside the Operation Result's metadata or
        /// the passed collection do not contain any element.</exception>
        /// <param name="arguments">Required. The collection of arguments. 
        /// The items of the corresponding collections will be added to the existing Operatio Result's metadata.</param>
        /// <returns>OperationResult</returns>
        public OperationResult WithArguments([Required] Dictionary<string, object> arguments)
        {
            if (arguments.Count == 0)
            {
                throw new ArgumentException();
            }

            foreach(var argument in arguments)
            {
                if (Arguments.ContainsKey(argument.Key))
                {
                    throw new ArgumentException();
                }

                Arguments.Add(argument.Key, argument.Value);
            }

            return this;
        }

        /// <summary>
        ///     Adds the specified argument (metadata) to the corresponding Operation Result's collection.
        ///     Ensures that the existing metadata do not contain the key from the input patameter.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when the key is already contained inside the Operation Result's metadata.</exception>
        /// <param name="key">Required. The key of the argument (metadata).</param>
        /// <param name="value">Optional. The value of the argument (metadata).</param>
        /// <returns>OperationResult</returns>
        public OperationResult WithArgument([Required] string key, object value)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add(key, value);
            return WithArguments(dictionary);
        }

        /// <summary>
        ///     Adds the specified argument (metadata) to the corresponding Operation Result's collection.
        ///     Ensures that the existing metadata do not contain the key from the input patameter.
        /// </summary>
        /// <exception cref="ArgumentException">Throws ArgumentException when the key is already contained inside the Operation Result's metadata.</exception>
        /// <param name="argument">Required. The argument (metadata).</param>
        /// <returns>OperationResult</returns>
        public OperationResult WithArgument([Required] KeyValuePair<string, object> argument)
            => WithArgument(argument.Key, argument.Value);
        #endregion

        #region Private methods
        private void EnsureNotEmptyArgument(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException();
            }
        }
        #endregion
    }

    public abstract class OperationResult<TData> : OperationResult
    {
        #region Constructors
        protected OperationResult()
        {
        }

        protected OperationResult(ResultInfo resultInfo)
            : base(resultInfo)
        {
        }
        #endregion

        #region Core

        /// <summary>
        ///     Combines two results into one.
        ///     1. If both results have succeeded, the first one is returned. 
        ///     2. If the base result has failed, it is returned. 
        ///     3. If the base result has succeeded and the second one not, the final result will contain code, errors and arguments of the second result in form of FailureOperationResult<TData>.
        ///     4. If both results have failed, the final result will contain error messages and arguments (metadata) from both sources in form of FailureOperationResult<TData>
        ///     (The "Code" will be inherited from the base result).  
        /// </summary>
        /// <param name="operationResult">Required. The second operationresult (the one which will be combined with the base one).</param>
        /// <returns>OperationResult<TData></returns>
        public new OperationResult<TData> Combine(OperationResult operationResult)
        {
            if (HasSucceeded)
            {
                if (operationResult.HasSucceeded)
                {
                    return this;
                }
                else
                {
                    var failure = operationResult as FailureOperationResult;

                    var failureResult = new FailureOperationResult<TData>()
                        .WithCode(failure.Code);

                    if (failure.Errors.Any())
                    {
                        failureResult.WithErrors(failure.Errors);
                    }

                    if (failure.Arguments.Any())
                    {
                        failureResult.WithArguments(failure.Arguments);
                    }
                        
                    if (failure.Messages.Any())
                    {
                        failureResult.WithMessages(failure.Messages);
                    }

                    return failureResult;
                }
            }
            else
            {
                if (operationResult.HasSucceeded)
                {
                    return this;
                }
                else
                {
                    var firstFailure = this as FailureOperationResult;
                    var secondFailure = operationResult as FailureOperationResult;

                    var failureResult = new FailureOperationResult<TData>()
                        .WithCode(firstFailure.Code);

                    if (firstFailure.Errors.Any())
                    {
                        failureResult.WithErrors(firstFailure.Errors);
                    }

                    if (secondFailure.Errors.Any())
                    {
                        failureResult.WithErrors(secondFailure.Errors);
                    }

                    if (firstFailure.Arguments.Any())
                    {
                        failureResult.WithArguments(firstFailure.Arguments);
                    }

                    if (secondFailure.Arguments.Any())
                    {
                        failureResult.WithArguments(secondFailure.Arguments);
                    }

                    return failureResult;
                }
            }
        }

        #endregion

        #region Success Operation Result
        /// <summary>
        ///     Get succeeded Operation Result.
        /// </summary>
        /// <returns>SuccessOperationResult<TData></returns>
        public static SuccessOperationResult<TData> Succeeded(TData data)
        {
            var operationResult = new SuccessOperationResult<TData>(data);

            return operationResult;
        }

        /// <summary>
        ///     Get succeeded Operation Result.
        /// </summary>
        /// <param name="successInfo">Required. The model which contains information of Result Code, provided messages 
        /// and additional arguments.</param>
        /// <returns>SuccessOperationResult<TData></returns>
        public static SuccessOperationResult<TData> Succeeded(TData data, [Required] SuccessInfo successInfo)
        {
            var operationResult = new SuccessOperationResult<TData>(data, successInfo);

            return operationResult;
        }

        #endregion

        #region Failed Operation Result
        /// <summary>
        ///     Get failed Operation Result.
        /// </summary>
        /// <returns>FailureOperationResult</returns>
        public new static FailureOperationResult<TData> Failed()
        {
            var operationResult = new FailureOperationResult<TData>();

            return operationResult;
        }

        /// <summary>
        ///     Get failed Operation Result.
        /// </summary>
        /// <param name="failureInfo">Required. The model which contains information of Result Code, provided messages, 
        /// additional arguments and errors.</param>
        /// <returns>FailureOperationResult</returns>
        public new static FailureOperationResult<TData> Failed([Required] FailureInfo failureInfo)
        {
            var operationResult = new FailureOperationResult<TData>(failureInfo);

            return operationResult;
        }

        #endregion

        #region Fluent Operation's Result
        /// <summary>
        ///     Sets the code to the operation result.
        ///     The method ensures that the "code" is not null, empty or white space.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <param name="code">Required. Operation Result's code.</param>
        /// <returns>OperationResult<TData></returns>
        public new OperationResult<TData> WithCode([Required] string code)
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
        /// <returns>OperationResult<TData></returns>
        public new OperationResult<TData> WithMessages([Required] List<string> messages)
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
        /// <returns>OperationResult</returns>
        public new OperationResult<TData> WithMessage([Required] string message)
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
        /// <returns>OperationResult<TData></returns>
        public new OperationResult<TData> WithArguments([Required] Dictionary<string, object> arguments)
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
        /// <returns>OperationResult<TData></returns>
        public new OperationResult<TData> WithArgument([Required] string key, object value)
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
        /// <returns>OperationResult<TData></returns>
        public new OperationResult<TData> WithArgument([Required] KeyValuePair<string, object> argument)
            => WithArgument(argument.Key, argument.Value);

        #endregion
    }
}
