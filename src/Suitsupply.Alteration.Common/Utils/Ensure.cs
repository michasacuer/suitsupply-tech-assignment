namespace Suitsupply.Alteration.Common.Utils;

public static class Ensure
{
    public static void StringNotNullOrEmpty(string s, string nameof)
    {
        if (string.IsNullOrEmpty(s))
        {
            throw new ArgumentNullException(nameof);
        }
    }
    
    public static void NotNull(object o, string nameof)
    {
        if (o is null)
        {
            throw new ArgumentNullException(nameof);
        }
    }

    public static void DateTimeNotEmpty(DateTime date, string nameof)
    {
        if (date == DateTime.MinValue)
        {
            throw new ArgumentNullException(nameof);
        }
    }
}