using OperationResult.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OperationResult.Tests
{
    public class GenericSuccessOperationResultTests
    {
        #region Common

        private static void CommonPrecheck<T>(SuccessOperationResult<T> successOperationResult)
        {
            // Assert
            Assert.True(successOperationResult.HasSucceeded);
            Assert.False(successOperationResult.HasFailed);
            Assert.NotNull(successOperationResult.Messages);
            Assert.NotNull(successOperationResult.Arguments);
        }

        private static void CommonPrecheck<T>(OperationResult<T> successOperationResult)
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
            var successOperationResult = new SuccessOperationResult<int>();

            CommonPrecheck(successOperationResult);
            Assert.Equal(0, successOperationResult.Data);
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

            var firstSuccessOperationResult = new SuccessOperationResult<int>(1 ,new Models.SuccessInfo(firstSuccessCode));
            var secondSuccessOperationResult = new SuccessOperationResult<int>(2, new Models.SuccessInfo(secondSuccessCode, new List<string> { "succeeded!" }));
            var thirdSuccessOperationResult = new SuccessOperationResult<int>(3, new Models.SuccessInfo(thirdSuccessCode, new Dictionary<string, object> { { "key", 1 } }));
            var fourthSuccessOperationResult = new SuccessOperationResult<int>(4, new Models.SuccessInfo(fourthSuccessCode, ["successful"], new Dictionary<string, object> { { "key", 2 } }));

            CommonPrecheck(firstSuccessOperationResult);
            Assert.Equal(firstSuccessCode, firstSuccessOperationResult.Code);
            Assert.True(firstSuccessOperationResult.HasData);
            Assert.Equal(1, firstSuccessOperationResult.Data);

            CommonPrecheck(secondSuccessOperationResult);
            Assert.Equal(secondSuccessCode, secondSuccessOperationResult.Code);
            Assert.Contains("succeeded!", secondSuccessOperationResult.Messages);
            Assert.True(secondSuccessOperationResult.HasData);
            Assert.Equal(2, secondSuccessOperationResult.Data);

            CommonPrecheck(thirdSuccessOperationResult);
            Assert.Equal(thirdSuccessCode, thirdSuccessOperationResult.Code);
            Assert.Contains(argument, thirdSuccessOperationResult.Arguments);
            Assert.True(thirdSuccessOperationResult.HasData);
            Assert.Equal(3, thirdSuccessOperationResult.Data);

            CommonPrecheck(fourthSuccessOperationResult);
            Assert.Equal(fourthSuccessCode, fourthSuccessOperationResult.Code);
            Assert.Contains("successful", fourthSuccessOperationResult.Messages);
            Assert.Contains(new KeyValuePair<string, object>("key", 2), fourthSuccessOperationResult.Arguments);
            Assert.True(fourthSuccessOperationResult.HasData);
            Assert.Equal(4, fourthSuccessOperationResult.Data);
        }

        #region WithMessage

        [Fact]
        public void SuccessOperationResult_NoGenericWithMessage_True()
        {
            // Arrange
            var successMessage = "Succeeded!";
            var successOperationResult = new SuccessOperationResult<string>()
                .WithMessage(successMessage);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Contains(successMessage, successOperationResult.Messages);
            Assert.False(successOperationResult.HasData);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithNullMessage_ArgumentNullException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult<int>();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithMessage(null));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithEmptyMessage_ArgumentException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult<int>();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithMessage(string.Empty));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithWhitespacesOnlyMessage_ArgumentException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult<int>()
                .WithData(21);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Equal(21, successOperationResult.Data);
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
            var successOperationResult = new SuccessOperationResult<int>()
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
            string invalidSuccessMessage = null!;
            var successOperationResult = new SuccessOperationResult<int>();

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
            var successOperationResult = new SuccessOperationResult<int>();

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
            var successOperationResult = new SuccessOperationResult<int>();

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
            var successOperationResult = new SuccessOperationResult<int>()
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
            var successOperationResult = new SuccessOperationResult<int>();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithCode(invalidSuccessCode));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithEmptyCode_ArgumentException()
        {
            // Arrange
            string invalidSuccessCode = string.Empty;
            var successOperationResult = new SuccessOperationResult<int>();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithCode(invalidSuccessCode));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithWhitespacesCode_ArgumentException()
        {
            // Arrange
            string invalidSuccessCode = "    ";
            var successOperationResult = new SuccessOperationResult<int>();

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
            KeyValuePair<string, object> keyValuePair = new("key", value);
            var successOperationResult = new SuccessOperationResult<int>()
                .WithArgument(keyValuePair);

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Contains(keyValuePair, successOperationResult.Arguments);
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithNullArgument_ArgumentNullException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult<int>();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithArgument(default));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithDuplicateArgument_ArgumentException()
        {
            // Arrange
            var successOperationResult = new SuccessOperationResult<int>();
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
            KeyValuePair<string, object> firstKeyValuePair = new("key", 20);
            KeyValuePair<string, object> secondKeyValuePair = new("key2", 25);
            var dictionary = new Dictionary<string, object>
            {
                { "key", 20 },
                { "key2", 25 }
            };

            // Act
            var successOperationResult = new SuccessOperationResult<int>()
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
            var successOperationResult = new SuccessOperationResult<int>();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentException>(() => successOperationResult.WithArguments([]));
        }

        [Fact]
        public void SuccessOperationResult_NoGenericWithNullArgumentCollection_ArgumentException()
        {
            // Act
            var successOperationResult = new SuccessOperationResult<int>();

            // Assert
            CommonPrecheck(successOperationResult);
            Assert.Throws<ArgumentNullException>(() => successOperationResult.WithArguments(null));
        }

        #endregion
    }
}
