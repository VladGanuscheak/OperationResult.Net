using OperationResult.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationResult.Tests
{
    public class NonGenericFailureOperationResultTests
    {
        #region Common

        private void CommonPrecheck(FailureOperationResult failureOperationResult)
        {
            // Assert
            Assert.False(failureOperationResult.HasSucceeded);
            Assert.True(failureOperationResult.HasFailed);
            Assert.NotNull(failureOperationResult.Messages);
            Assert.NotNull(failureOperationResult.Arguments);
        }

        private void CommonPrecheck(OperationResult failureOperationResult)
        {
            // Assert
            Assert.False(failureOperationResult.HasSucceeded);
            Assert.True(failureOperationResult.HasFailed);
            Assert.NotNull(failureOperationResult.Messages);
            Assert.NotNull(failureOperationResult.Arguments);
        }

        #endregion

        [Fact]
        public void FailureOperationResult_NoGeneric_True()
        {
            // Arrange
            var failureOperationResult = new FailureOperationResult();

            CommonPrecheck(failureOperationResult);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithOptions_True()
        {
            // Arrange
            var firstFailureCode = "400";
            var secondFailureCode = "401";
            var thirdFailureCode = "402";
            var argument = new KeyValuePair<string, object>("key", 1);

            var fourthFailureCode = "404";

            var firstFailureOperationResult = new FailureOperationResult(new Models.FailureInfo(firstFailureCode));
            var secondFailureOperationResult = new FailureOperationResult(new Models.FailureInfo(secondFailureCode, new List<string> { "failed!" }));
            var thirdFailureOperationResult = new FailureOperationResult(new Models.FailureInfo(thirdFailureCode, new Dictionary<string, object> { { "key", 1 } }));
            var fourthFailureOperationResult = new FailureOperationResult(new Models.FailureInfo(fourthFailureCode, new List<string> { "failed" }, new Dictionary<string, object> { { "key", 2 } }));

            CommonPrecheck(firstFailureOperationResult);
            Assert.Equal(firstFailureCode, firstFailureOperationResult.Code);

            CommonPrecheck(secondFailureOperationResult);
            Assert.Equal(secondFailureCode, secondFailureOperationResult.Code);
            Assert.Contains("failed!", secondFailureOperationResult.Messages);

            CommonPrecheck(thirdFailureOperationResult);
            Assert.Equal(thirdFailureCode, thirdFailureOperationResult.Code);
            Assert.Contains(argument, thirdFailureOperationResult.Arguments);

            CommonPrecheck(fourthFailureOperationResult);
            Assert.Equal(fourthFailureCode, fourthFailureOperationResult.Code);
            Assert.Contains("failed", fourthFailureOperationResult.Messages);
            Assert.Contains(new KeyValuePair<string, object>("key", 2), fourthFailureOperationResult.Arguments);
        }

        #region WithMessage

        [Fact]
        public void FailureOperationResult_NoGenericWithMessage_True()
        {
            // Arrange
            var failureMessage = "Failed!";
            var failureOperationResult = new FailureOperationResult()
                .WithMessage(failureMessage);

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Contains(failureMessage, failureOperationResult.Messages);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithNullMessage_ArgumentNullException()
        {
            // Arrange
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithMessage(null));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithEmptyMessage_ArgumentException()
        {
            // Arrange
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithMessage(string.Empty));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithWhitespacesOnlyMessage_ArgumentException()
        {
            // Arrange
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithMessage("     "));
        }

        #endregion

        #region WithMessages

        [Fact]
        public void FailureOperationResult_NoGenericWithMessages_True()
        {
            // Arrange
            var firstFailureMessage = "Failed!";
            var secondFailureMessage = "Second failure message!";
            var failureOperationResult = new FailureOperationResult()
                .WithMessages([firstFailureMessage, secondFailureMessage]);

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Contains(firstFailureMessage, failureOperationResult.Messages);
            Assert.Contains(secondFailureMessage, failureOperationResult.Messages);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithANullMessage_ArgumentNullException()
        {
            // Arrange
            var firstFailureMessage = "Failed!";
            string invalidFailureMessage = null;
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithMessages([firstFailureMessage, invalidFailureMessage]));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithAnEmptyMessage_ArgumentException()
        {
            // Arrange
            var firstFailureMessage = "Failed!";
            string invalidFailureMessage = string.Empty;
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithMessages([firstFailureMessage, invalidFailureMessage]));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithAnWhitespacesOnlyMessage_ArgumentException()
        {
            // Arrange
            var firstFailureMessage = "Failed!";
            string invalidFailureMessage = "        ";
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithMessages([firstFailureMessage, invalidFailureMessage]));
        }

        #endregion

        #region WithCode method testing

        [Fact]
        public void FailureOperationResult_NoGenericWithCode_True()
        {
            // Arrange
            var failureCode = "400";
            var failureOperationResult = new FailureOperationResult()
                .WithCode(failureCode);

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Equal(failureCode, failureOperationResult.Code);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithNullCode_ArgumentNullException()
        {
            // Arrange
            string invalidFailureCode = null!;
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithCode(invalidFailureCode));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithEmptyCode_ArgumentException()
        {
            // Arrange
            string invalidFailureCode = string.Empty;
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithCode(invalidFailureCode));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithWhitespacesCode_ArgumentException()
        {
            // Arrange
            string invalidFailureCode = "    ";
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithCode(invalidFailureCode));
        }

        #endregion

        #region WithArgument

        [Fact]
        public void FailureOperationResult_NoGenericWithArgument_True()
        {
            // Arrange
            var value = 20;
            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>("key", value);
            var failureOperationResult = new FailureOperationResult()
                .WithArgument(keyValuePair);

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Contains(keyValuePair, failureOperationResult.Arguments);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithNullArgument_ArgumentNullException()
        {
            // Arrange
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithArgument(default));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithDuplicateArgument_ArgumentException()
        {
            // Arrange
            var failureOperationResult = new FailureOperationResult();
            failureOperationResult.WithArgument(new KeyValuePair<string, object>("key", "value"));

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithArgument(new KeyValuePair<string, object>("key", "this is a duplicate")));
        }

        #endregion

        #region WithArguments

        [Fact]
        public void FailureOperationResult_NoGenericWithArguments_True()
        {
            // Arrange
            KeyValuePair<string, object> firstKeyValuePair = new KeyValuePair<string, object>("key", 20);
            KeyValuePair<string, object> secondKeyValuePair = new KeyValuePair<string, object>("key2", 25);
            var dictionary = new Dictionary<string, object>();

            // Act
            dictionary.Add("key", 20);
            dictionary.Add("key2", 25);
            var failureOperationResult = new FailureOperationResult()
                .WithArguments(dictionary);

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Contains(firstKeyValuePair, failureOperationResult.Arguments);
            Assert.Contains(secondKeyValuePair, failureOperationResult.Arguments);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithEmptyArgumentCollection_ArgumentException()
        {
            // Act
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithArguments(new Dictionary<string, object>()));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithNullArgumentCollection_ArgumentException()
        {
            // Act
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithArguments(null));
        }

        #endregion

        #region WithError

        [Fact]
        public void FailureOperationResult_NoGenericWithError_True()
        {
            // Arrange
            var exception = new ArgumentException();

            // Act
            var failureOperationResult = new FailureOperationResult();
            failureOperationResult.WithError(exception);

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Contains(exception, failureOperationResult.Errors);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithNullError_ThrowsArgumentNullException()
        {
            // Act
            var failureOperationResult = new FailureOperationResult();
            
            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithError(null));
        }

        #endregion

        #region WithErrors

        [Fact]
        public void FailureOperationResult_NoGenericWithErrors_True()
        {
            // Arrange
            var firstException = new ArgumentException();
            var secondException = new ArgumentNullException();

            // Act
            var failureOperationResult = new FailureOperationResult();
            failureOperationResult.WithErrors([firstException, secondException]);

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Contains(firstException, failureOperationResult.Errors);
            Assert.Contains(secondException, failureOperationResult.Errors);
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithNullErrors_ThrowsArgumentNullException()
        {
            // Act
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithErrors(null));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithEmptyErrors_ThrowsArgumentNullException()
        {
            // Act
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentException>(() => failureOperationResult.WithErrors(Enumerable.Empty<Exception>().ToList()));
        }

        [Fact]
        public void FailureOperationResult_NoGenericWithAnInvalidError_ThrowsArgumentNullException()
        {
            // Act
            var failureOperationResult = new FailureOperationResult();

            // Assert
            CommonPrecheck(failureOperationResult);
            Assert.Throws<ArgumentNullException>(() => failureOperationResult.WithErrors(new List<Exception> { default! }));
        }

        #endregion
    }
}
