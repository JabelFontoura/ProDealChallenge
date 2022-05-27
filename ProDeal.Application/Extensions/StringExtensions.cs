namespace ProDeal.Application.Extensions
{
    public static class StringExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            int i;

            if (int.TryParse(s.Trim(), out i))
            {
                return i;
            }

            return null;
        }
    }
}
