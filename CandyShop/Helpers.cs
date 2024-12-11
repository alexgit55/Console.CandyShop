namespace MarysCandyShop
{
    internal static class Helpers
    {
        internal static int GetDaysSinceOpening()
        {
            var openingDate = new DateTime(2023, 1, 1);
            return (DateTime.Now - openingDate).Days;
        }
    }
}
