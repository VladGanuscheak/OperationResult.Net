using OperationResult.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OperationResult.Results
{
    public class SuccessOperationResult : OperationResult
    {
        /// <summary>
        ///     The constructor of succeeded Operation result.
        /// </summary>
        public SuccessOperationResult()
        {
        }

        /// <summary>
        ///     The constructor of succeeded Operation result.
        /// </summary>
        /// <param name="successInfo">Required. The information of the succeeded operation result.</param>
        public SuccessOperationResult(
            [Required] SuccessInfo successInfo)
                : base(successInfo)
        {
        }
    }

    public class SuccessOperationResult<TData> : OperationResult<TData>
    {
        public SuccessOperationResult()
        {
        }

        /// <summary>
        ///     The constructor of succeeded Operation result which containts the Data parameter.
        /// </summary>
        /// <param name="data">The provided Data.</param>
        public SuccessOperationResult(TData data)
        {
            if (data != null)
            {
                WithData(data);
            }
        }

        /// <summary>
        ///     The constructor of succeeded Operation result which containts the Data parameter.
        /// </summary>
        /// <param name="data">The provided Data.</param>
        /// <param name="successInfo">Required. The information of the succeeded operation result.</param>
        public SuccessOperationResult(
            TData data,
            [Required] SuccessInfo successInfo)
                : base(successInfo)
        {
            if (data != null)
            {
                WithData(data);
            }
        }

        private TData _data;

        /// <summary>
        ///     The provided Data.
        /// </summary>
        public TData Data => _data;

        /// <summary>
        ///     Checks if the provided data isn't null.
        /// </summary>
        public bool HasData => Data != null;

        #region Fluent Operation's Result
        /// <summary>
        ///     Sets the code to the operation result.
        ///     The method ensures that the "code" is not null, empty or white space.
        ///     *Overwrites the base functionality by reusing it.
        /// </summary>
        /// <param name="code">Required. Operation Result's code.</param>
        /// <returns>SuccessOperationResult<TData></returns>
        public new SuccessOperationResult<TData> WithCode([Required] string code)
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
        /// <returns>SuccessOperationResult<TData></returns>
        public new SuccessOperationResult<TData> WithMessages([Required] List<string> messages)
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
        /// <returns>SuccessOperationResult<TData></returns>
        public new SuccessOperationResult<TData> WithMessage([Required] string message)
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
        /// <returns>SuccessOperationResult<TData></returns>
        public new SuccessOperationResult<TData> WithArguments([Required] Dictionary<string, object> arguments)
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
        /// <returns>SuccessOperationResult<TData></returns>
        public new SuccessOperationResult<TData> WithArgument([Required] string key, object value)
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
        /// <returns>SuccessOperationResult<TData></returns>
        public new SuccessOperationResult<TData> WithArgument([Required] KeyValuePair<string, object> argument)
            => WithArgument(argument.Key, argument.Value);

        #endregion

        /// <summary>
        ///     Sets the Operation Result's data.
        /// </summary>
        /// <param name="data">The provided Data.</param>
        /// <returns>SuccessOperationResult<TData></returns>
        public SuccessOperationResult<TData> WithData(TData data)
        {
            _data = data;

            return this;
        }
    }
}
