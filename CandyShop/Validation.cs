
using MarysCandyShop.Models;

namespace MarysCandyShop;

public static class Validation
{
    public static PriceValidationResponse IsPriceValid(string? productPrice)
    {
        var response = new PriceValidationResponse
        {
            IsValid = true
        };

        decimal price;
        if (!decimal.TryParse(productPrice, out price))
        {
            response.IsValid = false;
            response.Message = "Price must be a number";
            return response;
        }

        if (price < 0 || price > 9999)
        {
            response.IsValid = false;
            response.Message = "Price must be between 0 and 9999";
            return response;
        }

        response.Price = price;
        return response;
    }

    public static CocoaValidationResponse IsCocoaPercentageValid(string? cocoaPercentage)
    {
        var response = new CocoaValidationResponse
        {
            IsValid = true
        };
        int percentage;
        if (!int.TryParse(cocoaPercentage, out percentage))
        {
            response.IsValid = false;
            response.Message = "Cocoa percentage must be a number";
            return response;
        }
        if (percentage < 0 || percentage > 99)
        {
            response.IsValid = false;
            response.Message = "Cocoa percentage must be between 0 and 100";
            return response;
        }
        response.CocoaPercentage = percentage;
        return response;
    }

    public static bool IsStringValid(string input)
    {
        return !string.IsNullOrWhiteSpace(input) && input.Length <= 20;
    }


}
