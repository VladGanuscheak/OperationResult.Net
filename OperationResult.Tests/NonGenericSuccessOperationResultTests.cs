using OperationResult.Results;

namespace OperationResult.Tests
{
    public class NonGenericSuccessOperationResultTests
    {
        #region Common

        private void CommonPrecheck(SuccessOperationResult successOperationResult) 
        {
            // Assert
            Assert.True(successOperationResult.HasSucceeded);
            Assert.False(successOperationResult.HasFailed);
            Assert.NotNull(successOperationResult.Messages);
            Assert.NotNull(successOperationResult.Arguments);
        }

        private void CommonPrecheck(OperationResult successOperationResult)
        {
            // Assert
            Assert.True(successOperationResult.HasSucceeded);
            Assert.False(successOperationResult.HasFailed);
            Assert.NotNull(successOperationResult.Messages);
            Assert.NotNull(successOperationResult.Arguments);
        }

        #endregion

        [Fact]
        public void SuccessOperationResult_NoGeneric_True()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult();

            CommonPrecheck(successOperationResult);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithOptions_True()
        {
            // Arrange
            var firstSuccessCode = "200";
            var secondSuccessCode = "201";
            var thirdSuccessCode = "202";
            var argument = new KeyValuePair<string, object>("key", 1);
            
            var fourthSuccessCode = "204";

            var firstSuccessOperationResult = new SuccessOperationResult(new Models.SuccessInfo(firstSuccessCode));
            var secondSuccessOperationResult = new SuccessOperationResult(new Models.SuccessInfo(secondSuccessCode, new List<string> { "succeeded!" }));
            var thirdSuccessOperationResult = new SuccessOperationResult(new Models.SuccessInfo(thirdSuccessCode, new Dictionary<string, object> { { "key", 1 } }));
            var fourthSuccessOperationResult = new SuccessOperationResult(new Models.SuccessInfo(fourthSuccessCode, new List<string> { "successful" }, new Dictionary<string, object> { { "key", 2 } }));

            CommonPrecheck(firstSuccessOperationResult);
            Assert.Equal(firstSuccessCode, firstSuccessOperationResult.Code);

            CommonPrecheck(secondSuccessOperationResult);
            Assert.Equal(secondSuccessCode, secondSuccessOperationResult.Code);
            Assert.Contains("succeeded!", secondSuccessOperationResult.Messages);
            
            CommonPrecheck(thirdSuccessOperationResult);
            Assert.Equal(thirdSuccessCode, thirdSuccessOperationResult.Code);
            Assert.Contains(argument, thirdSuccessOperationResult.Arguments);
            
            CommonPrecheck(fourthSuccessOperationResult);
            Assert.Equal(fourthSuccessCode, fourthSuccessOperationResult.Code);
            Assert.Contains("successful", fourthSuccessOperationResult.Messages);
            Assert.Contains(new KeyValuePair<string, object>("key", 2), fourthSuccessOperationResult.Arguments);
        }

        #region WithMessage

        [Fact]
        public void SuccessOperationResult_NoGenericWithMessage_True()
        {
            // Arrange
            var successMessage = "Succeeded!";
            var successOperationResult = new SuccessOperationResult()
                .WithMessage(successMessage);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Contains(successMessage, successOperationResult.Messages);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithNullMessage_ArgumentNullException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithMessage(null));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithEmptyMessage_ArgumentException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithMessage(string.Empty));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithWhitespacesOnlyMessage_ArgumentException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithMessage("     "));
        }

        #endregion

        #region WithMessages

        [Fact]
        public void SuccessOperationResult_NoGenericWithMessages_True()
        {
            // Arrange
            var firstSuccessMessage = "Succeeded!";
            var secondSuccessMessage = "Second success message!";
            var successOperationResult = new SuccessOperationResult()
                .WithMessages([firstSuccessMessage, secondSuccessMessage]);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Contains(firstSuccessMessage, successOperationResult.Messages);
            Assert.Contains(secondSuccessMessage, successOperationResult.Messages);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithANullMessage_ArgumentNullException()
        {
            // Arrange
            var firstSuccessMessage = "Succeeded!";
            string invalidSuccessMessage = null;
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithMessages([firstSuccessMessage, invalidSuccessMessage]));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithAnEmptyMessage_ArgumentException()
        {
            // Arrange
            var firstSuccessMessage = "Succeeded!";
            string invalidSuccessMessage = string.Empty;
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithMessages([firstSuccessMessage, invalidSuccessMessage]));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithAnWhitespacesOnlyMessage_ArgumentException()
        {
            // Arrange
            var firstSuccessMessage = "Succeeded!";
            string invalidSuccessMessage = "        ";
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithMessages([firstSuccessMessage, invalidSuccessMessage]));
        }

        #endregion

        #region WithCode method testing

        [Fact]
        public void SuccessOperationResult_NoGenericWithCode_True()
        {
            // Arrange
            var successCode = "200";
            var successOperationResult = new SuccessOperationResult()
                .WithCode(successCode);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Equal(successCode, successOperationResult.Code);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithNullCode_ArgumentNullException()
        {
            // Arrange
            string invalidSuccessCode = null!;
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithCode(invalidSuccessCode));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithEmptyCode_ArgumentException()
        {
            // Arrange
            string invalidSuccessCode = string.Empty;
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithCode(invalidSuccessCode));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithWhitespacesCode_ArgumentException()
        {
            // Arrange
            string invalidSuccessCode = "    ";
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithCode(invalidSuccessCode));
        }

        #endregion

        #region WithArgument

        [Fact]
        public void SuccessOperationResult_NoGenericWithArgument_True()
        {
            // Arrange
            var value = 20;
            KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>("key", value);
            var successOperationResult = new SuccessOperationResult()
                .WithArgument(keyValuePair);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Contains(keyValuePair, successOperationResult.Arguments);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithNullArgument_ArgumentNullException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithArgument(default));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithDuplicateArgument_ArgumentException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult();
            successOperationResult.WithArgument(new KeyValuePair<string, object>("key", "value"));

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithArgument(new KeyValuePair<string, object>("key", "this is a duplicate")));
        }

        #endregion

        #region WithArguments

        [Fact]
        public void SuccessOperationResult_NoGenericWithArguments_True()
        {
            // Arrange
            KeyValuePair<string, object> firstKeyValuePair = new KeyValuePair<string, object>("key", 20);
            KeyValuePair<string, object> secondKeyValuePair = new KeyValuePair<string, object>("key2", 25);
            var dictionary = new Dictionary<string, object>();

            // Act
            dictionary.Add("key", 20);
            dictionary.Add("key2", 25);
            var successOperationResult = new SuccessOperationResult()
                .WithArguments(dictionary);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Contains(firstKeyValuePair, successOperationResult.Arguments);
            Assert.Contains(secondKeyValuePair, successOperationResult.Arguments);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithEmptyArgumentCollection_ArgumentException()
        {
            // Act
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithArguments(new Dictionary<string, object>()));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithNullArgumentCollection_ArgumentException()
        {
            // Act
            var successOperationResult = new SuccessOperationResult();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithArguments(null));
        }

        #endregion
    }
}
