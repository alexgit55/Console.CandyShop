namespace MarysCandyShop.UnitTests
{
    public class ValidationTests
    {
        [Fact]
        public void WhenStringIsValidReturnTrue()
        {
            var stringInput = "Test Chocolate Bar";
            var result = Validation.IsStringValid(stringInput);

            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("This is a string with more than 20 characters")]
        public void WhenStringIsNotValidReturnFalse(string input)
        {
            var result = Validation.IsStringValid(input);
            Assert.False(result);
        }

        [Theory]
        [InlineData("20")]
        [InlineData("20.5")]
        public void WhenPriceIsValidReturnTrue(string input)
        {
            var result = Validation.IsPriceValid(input);
            Assert.True(result.IsValid);
        }

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

        [Theory]
        [InlineData("10")]
        [InlineData("50")]
        public void WhenCocoaIsValidReturnTrue(string input)
        {
            var result = Validation.IsCocoaPercentageValid(input);
            Assert.True(result.IsValid);
        }

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