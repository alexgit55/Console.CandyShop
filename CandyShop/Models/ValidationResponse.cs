namespace MarysCandyShop.Models;

internal class ValidationResponse
{
    internal bool IsValid { get; set; }
    internal string Message { get; set; }
}

internal class CocoaValidationResponse : ValidationResponse
{
    internal int CocoaPercentage { get; set; }
}

internal class PriceValidationResponse : ValidationResponse
{
    internal decimal Price { get; set; }
}
