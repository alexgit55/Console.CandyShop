namespace MarysCandyShop.UnitTests
{
    public class ValidationTests
    {

        // Test the IsStringValid method, which should return true if the string is not null or empty and has a length of 20 characters or less.
        [Fact]
        public void WhenStringIsValidReturnTrue()
        {
            var stringInput = "Test Chocolate Bar";
            var result = Validation.IsStringValid(stringInput);

            Assert.True(result);
        }

        // Test the IsStringValid method, which should return false if the string is null, empty, or has a length of more than 20 characters.
        [Theory]
        [InlineData("")]
        [InlineData("This is a string with more than 20 characters")]
        public void WhenStringIsNotValidReturnFalse(string input)
        {
            var result = Validation.IsStringValid(input);
            Assert.False(result);
        }

        // Test the IsPriceValid method, which should return true if the price is a valid number between 0 and 9999.
        [Theory]
        [InlineData("20")]
        [InlineData("20.5")]
        public void WhenPriceIsValidReturnTrue(string input)
        {
            var result = Validation.IsPriceValid(input);
            Assert.True(result.IsValid);
        }


        // Test the IsPriceValid method, which should return false if the price is not a valid number or is outside the range of 0 to 9999.
        [Theory]
        [InlineData("")]
        [InlineData("Not a number")]
        [InlineData("-5")]
        [InlineData("10000")]
        public void WhenPriceIsNotValidReturnFalse(string input)
        {
            var result = Validation.IsPriceValid(input);
            Assert.False(result.IsValid);
        }


        // Test the IsCocoaPercentageValid method, which should return true if the cocoa percentage is a valid number between 0 and 99.
        [Theory]
        [InlineData("10")]
        [InlineData("50")]
        public void WhenCocoaIsValidReturnTrue(string input)
        {
            var result = Validation.IsCocoaPercentageValid(input);
            Assert.True(result.IsValid);
        }


        // Test the IsCocoaPercentageValid method, which should return false if the cocoa percentage is not a valid number or is outside the range of 0 to 99.
        [Theory]
        [InlineData("")]
        [InlineData("Not a number")]
        [InlineData("-5")]
        [InlineData("100")]
        public void WhenCocoaIsNotValidReturnFalse(string input)
        {
            var result = Validation.IsCocoaPercentageValid(input);
            Assert.False(result.IsValid);
        }

    }
}