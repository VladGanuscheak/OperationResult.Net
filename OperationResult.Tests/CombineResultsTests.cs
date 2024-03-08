using OperationResult.Results;

namespace OperationResult.Tests
{
    public class CombineResultsTests
    {
        [Fact]
        public void CombineResults_ValidAndValid_ValidResult()
        {
            // Arrange
            var firstSuccessResult = new SuccessOperationResult();
            var secondSuccessResult = new SuccessOperationResult();

            // Act
            var finalResult = firstSuccessResult.Combine(secondSuccessResult);

            // Assert
            Assert.IsType<SuccessOperationResult>(finalResult);
            Assert.True(finalResult.HasSucceeded);
            Assert.False(finalResult.HasFailed);
        }

        [Fact]
        public void CombineResults_ValidGenericAndValid_ValidResult()
        {
            // Arrange
            var firstData = 10;
            var firstSuccessResult = new SuccessOperationResult<int>(firstData);
            var secondSuccessResult = new SuccessOperationResult();

            // Act
            var finalResult = firstSuccessResult.Combine(secondSuccessResult);

            // Assert
            Assert.IsType<SuccessOperationResult<int>>(finalResult);
            Assert.True(finalResult.HasSucceeded);
            Assert.False(finalResult.HasFailed);
            Assert.Equal(firstData, ((SuccessOperationResult<int>)finalResult).Data);
            Assert.True(((SuccessOperationResult<int>)finalResult).HasData);
        }

        [Fact]
        public void CombineResults_ValidAndValidGeneric_ValidResult()
        {
            // Arrange
            var secondData = 10;
            var firstSuccessResult = new SuccessOperationResult();
            var secondSuccessResult = new SuccessOperationResult<int>(secondData);

            // Act
            var finalResult = firstSuccessResult.Combine(secondSuccessResult);

            // Assert
            Assert.IsType<SuccessOperationResult>(finalResult);
            Assert.True(finalResult.HasSucceeded);
            Assert.False(finalResult.HasFailed);
        }

        [Fact]
        public void CombineResults_ValidGenericAndValidGeneric_ValidResult()
        {
            // Arrange
            var firstData = 10;
            var firstSuccessResult = new SuccessOperationResult<long>(firstData);
            var secondData = 20;
            var secondSuccessResult = new SuccessOperationResult<int>(secondData);

            // Act
            var finalResult = firstSuccessResult.Combine(secondSuccessResult);

            // Assert
            Assert.IsType<SuccessOperationResult<long>>(finalResult);
            Assert.True(finalResult.HasSucceeded);
            Assert.False(finalResult.HasFailed);
            Assert.Equal(firstData, ((SuccessOperationResult<long>)finalResult).Data);
            Assert.True(((SuccessOperationResult<long>)finalResult).HasData);
        }

        [Fact]
        public void CombineResults_ValidAndInvalid_InvalidResult()
        {
            // Arrange
            var firstSuccessResult = new SuccessOperationResult();
            var secondSuccessResult = new FailureOperationResult();

            // Act
            var finalResult = firstSuccessResult.Combine(secondSuccessResult);

            // Assert
            Assert.IsType<FailureOperationResult>(finalResult);
            Assert.False(finalResult.HasSucceeded);
            Assert.True(finalResult.HasFailed);
        }

        [Fact]
        public void CombineResults_ValidAndInvalidGeneric_InvalidResult()
        {
            // Arrange
            var firstSuccessResult = new SuccessOperationResult();
            var secondSuccessResult = new FailureOperationResult<long>();

            // Act
            var finalResult = firstSuccessResult.Combine(secondSuccessResult);

            // Assert
            Assert.IsType<FailureOperationResult<long>>(finalResult);
            Assert.False(finalResult.HasSucceeded);
            Assert.True(finalResult.HasFailed);
        }

        [Fact]
        public void CombineResults_InvalidAndValid_InvalidResult()
        {
            // Arrange
            var firstErrorMessage = "first failed";
            var firstCode = "400";
            var firstException = new ArgumentNullException();
            var firstArgument = new KeyValuePair<string, object>("test", new { value = 5 });

            var secondErrorMessage = "secondFailed";
            var secondCode = "403";
            var secondException = new ArgumentException();
            var secondArgument = new KeyValuePair<string, object>("test 2", new { value = 5 });


            var firstSuccessResult = new FailureOperationResult()
                .WithError(firstException)
                .WithMessage(firstErrorMessage)
                .WithCode(firstCode)
                .WithArgument(firstArgument);

            var secondSuccessResult = new FailureOperationResult()
                .WithError(secondException)
                .WithMessage(secondErrorMessage)
                .WithCode(secondCode)
                .WithArgument(secondArgument);
            
            // Act
            var finalResult = firstSuccessResult.Combine(secondSuccessResult);

            // Assert
            Assert.IsType<FailureOperationResult>(finalResult);
            Assert.False(finalResult.HasSucceeded);
            Assert.True(finalResult.HasFailed);

            Assert.Equal(firstCode, finalResult.Code);
            Assert.Contains(firstErrorMessage, finalResult.Messages);
            Assert.Contains(secondErrorMessage, finalResult.Messages);

            Assert.Contains(firstException, ((FailureOperationResult)finalResult).Errors);
            Assert.Contains(secondException, ((FailureOperationResult)finalResult).Errors);

            Assert.Contains(firstArgument, finalResult.Arguments);
            Assert.Contains(secondArgument, finalResult.Arguments);
        }

        [Fact]
        public void CombineResults_InvalidAndValidGeneric_InvalidResult()
        {
            // Arrange
            var firstErrorMessage = "first failed";
            var firstCode = "400";
            var firstException = new ArgumentNullException();
            var firstArgument = new KeyValuePair<string, object>("test", new { value = 5 });

            var secondErrorMessage = "secondFailed";
            var secondCode = "403";
            var secondException = new ArgumentException();
            var secondArgument = new KeyValuePair<string, object>("test 2", new { value = 5 });


            var firstFailureResult = new FailureOperationResult()
                .WithError(firstException)
                .WithMessage(firstErrorMessage)
                .WithCode(firstCode)
                .WithArgument(firstArgument);

            var secondFailureResult = new FailureOperationResult<string>()
                .WithError(secondException)
                .WithMessage(secondErrorMessage)
                .WithCode(secondCode)
                .WithArgument(secondArgument);

            // Act
            var finalResult = firstFailureResult.Combine(secondFailureResult);

            // Assert
            Assert.IsType<FailureOperationResult>(finalResult);
            Assert.False(finalResult.HasSucceeded);
            Assert.True(finalResult.HasFailed);

            Assert.Equal(firstCode, finalResult.Code);
            Assert.Contains(firstErrorMessage, finalResult.Messages);
            Assert.Contains(secondErrorMessage, finalResult.Messages);

            Assert.Contains(firstException, ((FailureOperationResult)finalResult).Errors);
            Assert.Contains(secondException, ((FailureOperationResult)finalResult).Errors);

            Assert.Contains(firstArgument, finalResult.Arguments);
            Assert.Contains(secondArgument, finalResult.Arguments);
        }
    }
}
