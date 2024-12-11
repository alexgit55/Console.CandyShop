namespace MarysCandyShop.UnitTests
{
    public class ValidationTests
    {
        [Fact]
        public void WhenStringIsValidReturnTrue()
        {
            var stringInput = "Test Chocolate Bar";
            var result = Validation.IsStringValid(stringInput);
        }
    }
}