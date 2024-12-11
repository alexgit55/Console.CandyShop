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
    }
}