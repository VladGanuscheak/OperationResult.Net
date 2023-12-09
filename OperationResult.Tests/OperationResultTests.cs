using OperationResult.Models;
using OperationResult.Results;

namespace OperationResult.Tests
{
    public class OperationResultTests
    {
        [Fact]
        public void Succeeded_ShouldCreateSuccessOperationResult_NoConditions()
        {
            // Act
            var result = OperationResult.Succeeded();

            // Assert
            Assert.IsType<SuccessOperationResult>(result);
            Assert.True(result.HasSucceeded);
        }

        [Fact]
        public void Succeeded_WithData_ShouldCreateSuccessOperationResultWithData()
        {
            // Arrange
            var data = "Sample Data";

            // Act
            var result = OperationResult.Succeeded(data);

            // Assert
            Assert.IsType<SuccessOperationResult<string>>(result);
            Assert.True(result.HasSucceeded);
            Assert.Equal(data, result.Data);
        }

        [Fact]
        public void Succeeded_WithInfo_ShouldCreateSuccessOperationResultWithInfo()
        {
            // Arrange
            var successInfo = new SuccessInfo();

            // Act
            var result = OperationResult.Succeeded(successInfo);

            // Assert
            Assert.IsType<SuccessOperationResult>(result);
            Assert.True(result.HasSucceeded);
        }

        [Fact]
        public void Failed_ShouldCreateFailureOperationResult()
        {
            // Act
            var result = OperationResult.Failed();

            // Assert
            Assert.IsType<FailureOperationResult>(result);
            Assert.True(result.HasFailed);
        }

        [Fact]
        public void Failed_WithInfo_ShouldCreateFailureOperationResultWithInfo()
        {
            // Arrange
            var failureInfo = new FailureInfo();

            // Act
            var result = OperationResult.Failed(failureInfo);

            // Assert
            Assert.IsType<FailureOperationResult>(result);
            Assert.True(result.HasFailed);
        }

        [Fact]
        public void Combine_ShouldCombineOperationResults()
        {
            // Arrange
            var result1 = OperationResult.Succeeded();
            var result2 = OperationResult.Failed();

            // Act
            var combinedResult = result1.Combine(result2);

            // Assert
            Assert.IsAssignableFrom<OperationResult>(combinedResult);
        }

        [Fact]
        public void WithCode_ShouldSetCode()
        {
            // Arrange
            var result = OperationResult.Succeeded();

            // Act
            var resultWithCode = result.WithCode("123");

            // Assert
            Assert.Equal("123", resultWithCode.Code);
        }

        [Fact]
        public void WithMessages_ShouldSetMessages()
        {
            // Arrange
            var result = OperationResult.Succeeded();
            var messages = new List<string> { "Message1", "Message2" };

            // Act
            var resultWithMessages = result.WithMessages(messages);

            // Assert
            Assert.Equal(messages, resultWithMessages.Messages);
        }


        //
        // Second set of tests
        //

        [Fact]
        public void WithMessage_ShouldAddMessage()
        {
            // Arrange
            var result = OperationResult.Succeeded();

            // Act
            var resultWithMessage = result.WithMessage("This is a message.");

            // Assert
            Assert.Single(resultWithMessage.Messages);
            Assert.Contains("This is a message.", resultWithMessage.Messages);
        }

        [Fact]
        public void WithArguments_ShouldSetArguments()
        {
            // Arrange
            var result = OperationResult.Succeeded();
            var arguments = new Dictionary<string, object>
            {
                { "Key1", "Value1" },
                { "Key2", 42 }
            };

            // Act
            var resultWithArguments = result.WithArguments(arguments);

            // Assert
            Assert.Equal(arguments, resultWithArguments.Arguments);
        }

        [Fact]
        public void WithError_ShouldAddErrorToFailureResult()
        {
            // Arrange
            var failureResult = OperationResult.Failed();
            var exception = new InvalidOperationException("Sample exception");

            // Act
            var resultWithError = failureResult.WithError(exception);

            // Assert
            Assert.Single(resultWithError.Errors);
            Assert.Contains(exception, resultWithError.Errors);
        }

        [Fact]
        public void WithData_ShouldSetDataForSuccessResultWithData()
        {
            // Arrange
            var successResult = OperationResult.Succeeded(string.Empty);
            var data = "Sample Data";

            // Act
            var resultWithData = successResult.WithData(data);

            // Assert
            Assert.IsType<SuccessOperationResult<string>>(resultWithData);
            Assert.True(resultWithData.HasSucceeded);
            Assert.Equal(data, resultWithData.Data);
        }

        [Fact]
        public void WithErrors_ShouldSetErrorsForFailureResultWithErrors()
        {
            // Arrange
            var failureResult = OperationResult.Failed();
            var exceptions = new List<Exception>
            {
                new InvalidOperationException("Error 1"),
                new ArgumentNullException("Error 2")
            };

            // Act
            var resultWithErrors = failureResult.WithErrors(exceptions);

            // Assert
            Assert.Equal(exceptions, resultWithErrors.Errors);
        }

        // ANother set of tests

        [Fact]
        public void Combine_WithTwoSuccessResults_ShouldReturnCombinedSuccessResult()
        {
            // Arrange
            var successResult1 = OperationResult.Succeeded();
            var successResult2 = OperationResult.Succeeded();

            // Act
            var combinedResult = successResult1.Combine(successResult2);

            // Assert
            Assert.IsType<SuccessOperationResult>(combinedResult);
            Assert.True(combinedResult.HasSucceeded);
        }

        [Fact]
        public void Combine_WithSuccessAndFailureResults_ShouldReturnCombinedFailureResult()
        {
            // Arrange
            var successResult = OperationResult.Succeeded();
            var failureResult = OperationResult.Failed();

            // Act
            var combinedResult = successResult.Combine(failureResult);

            // Assert
            Assert.IsType<FailureOperationResult>(combinedResult);
            Assert.True(combinedResult.HasFailed);
        }

        //[Fact]
        //public void WithArguments_WithNullArguments_ShouldThrowArgumentNullException()
        //{
        //    // Arrange
        //    var result = OperationResult.Succeeded();

        //    // Act & Assert
        //    Assert.Throws<ArgumentNullException>(() => result.WithArguments(null));
        //}

        [Fact]
        public void WithArgument_WithNullKey_ShouldThrowArgumentNullException()
        {
            // Arrange
            var result = OperationResult.Succeeded();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => result.WithArgument(null, "value"));
        }

        //[Fact]
        //public void WithArgument_WithExistingKey_ShouldUpdateExistingArgument()
        //{
        //    // Arrange
        //    var result = OperationResult.Succeeded().WithArgument("Key", "OldValue");

        //    // Act
        //    var resultWithUpdatedArgument = result.WithArgument("Key", "NewValue");

        //    // Assert
        //    Assert.Equal("NewValue", resultWithUpdatedArgument.Arguments["Key"]);
        //}

        //[Fact]
        //public void WithError_WithNullError_ShouldThrowArgumentNullException()
        //{
        //    // Arrange
        //    var failureResult = OperationResult.Failed();

        //    // Act & Assert
        //    Assert.Throws<ArgumentNullException>(() => failureResult.WithError(null));
        //}

        [Fact]
        public void WithErrors_WithNullErrors_ShouldThrowArgumentNullException()
        {
            // Arrange
            var failureResult = OperationResult.Failed();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => failureResult.WithErrors(null));
        }

        //
        // Another set of tests
        //

        //[Fact]
        //public void WithMessages_WithNullMessages_ShouldThrowArgumentNullException()
        //{
        //    // Arrange
        //    var result = OperationResult.Succeeded();

        //    // Act & Assert
        //    Assert.Throws<ArgumentNullException>(() => result.WithMessages(null));
        //}

        [Fact]
        public void WithMessage_WithNullMessage_ShouldThrowArgumentNullException()
        {
            // Arrange
            var result = OperationResult.Succeeded();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => result.WithMessage(null));
        }


        [Fact]
        public void WithErrors_ShouldSetErrorsForFailureResult()
        {
            // Arrange
            var failureResult = OperationResult.Failed();
            var exceptions = new List<Exception>
            {
                new InvalidOperationException("Error 1"),
                new ArgumentNullException("Error 2")
            };

            // Act
            var resultWithErrors = failureResult.WithErrors(exceptions);

            // Assert
            Assert.Equal(exceptions, resultWithErrors.Errors);
        }


        [Fact]
        public void WithCode_ShouldSetCodeForFailureResultWithErrors()
        {
            // Arrange
            var failureResult = OperationResult.Failed();
            var exceptions = new List<Exception>
            {
                new InvalidOperationException("Error 1"),
                new ArgumentNullException("Error 2")
            };

            // Act
            var resultWithCode = failureResult
                .WithErrors(exceptions)
                .WithCode("500");

            // Assert
            Assert.Equal("500", resultWithCode.Code);
            Assert.Equal(exceptions, ((FailureOperationResult)resultWithCode).Errors);
        }

        //
        // Another set of tests
        //

        //[Fact]
        //public void WithArguments_WithEmptyArguments_ShouldSetEmptyArguments()
        //{
        //    // Arrange
        //    var result = OperationResult.Succeeded();

        //    // Act
        //    var resultWithEmptyArguments = result.WithArguments(new Dictionary<string, object>());

        //    // Assert
        //    Assert.Empty(resultWithEmptyArguments.Arguments);
        //}

        [Fact]
        public void WithArgument_WithNewArgument_ShouldAddArgument()
        {
            // Arrange
            var result = OperationResult.Succeeded();

            // Act
            var resultWithArgument = result.WithArgument("Key", "Value");

            // Assert
            Assert.Single(resultWithArgument.Arguments);
            Assert.Equal("Value", resultWithArgument.Arguments["Key"]);
        }

        [Fact]
        public void WithArgument_WithKeyValuePair_ShouldAddArgument()
        {
            // Arrange
            var result = OperationResult.Succeeded();
            var argument = new KeyValuePair<string, object>("Key", "Value");

            // Act
            var resultWithArgument = result.WithArgument(argument);

            // Assert
            Assert.Single(resultWithArgument.Arguments);
            Assert.Equal("Value", resultWithArgument.Arguments["Key"]);
        }

        [Fact]
        public void WithMessage_WithEmptyMessage_ShouldThrowArgumentException()
        {
            // Arrange
            var result = OperationResult.Succeeded();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => result.WithMessage(""));
        }

        //[Fact]
        //public void WithMessagesWithMessages_WithEmptyMessages_ShouldThrowArgumentException()
        //{
        //    // Arrange
        //    var result = OperationResult.Succeeded();

        //    // Act & Assert
        //    Assert.Throws<ArgumentException>(() => result.WithMessages(new List<string>()));
        //}

        //
        // Another tests
        //


        //[Fact]
        //public void Combine_WithTwoFailureResults_ShouldReturnCombinedFailureResultWithErrors()
        //{
        //    // Arrange
        //    var failureResult1 = OperationResult.Failed();
        //    var failureResult2 = OperationResult.Failed();

        //    // Act
        //    var combinedResult = failureResult1.Combine(failureResult2);

        //    // Assert
        //    Assert.IsType<FailureOperationResult>(combinedResult);
        //    Assert.True(combinedResult.HasFailed);
        //}

        [Fact]
        public void Combine_WithSuccessResultAndFailureResult_ShouldReturnCombinedFailureResultWithErrors()
        {
            // Arrange
            var successResult = OperationResult.Succeeded();
            var failureResult = OperationResult.Failed();

            // Act
            var combinedResult = successResult.Combine(failureResult);

            // Assert
            Assert.IsType<FailureOperationResult>(combinedResult);
            Assert.True(combinedResult.HasFailed);
        }

        [Fact]
        public void WithErrors_WithEmptyErrors_ShouldThrowArgumentException()
        {
            // Arrange
            var failureResult = OperationResult.Failed();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => failureResult.WithErrors(new List<Exception>()));
        }

        //[Fact]
        //public void WithArgument_WithEmptyKey_ShouldThrowArgumentException()
        //{
        //    // Arrange
        //    var result = OperationResult.Succeeded();

        //    // Act & Assert
        //    Assert.Throws<ArgumentException>(() => result.WithArgument("", "Value"));
        //}
    }
}
