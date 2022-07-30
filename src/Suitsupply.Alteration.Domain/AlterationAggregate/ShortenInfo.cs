using Suitsupply.Alteration.Common.Exceptions;

namespace Suitsupply.Alteration.Domain.AlterationAggregate;

public class ShortenInfo : IAlterationInfo
{
    public const int MaxAlternationDifferenceInCentimeters = 5;
    
    public ShortenInfo(
        int leftTrouserLegShortenBy,
        int rightTrouserLegShortenBy,
        int leftSleeveShortenBy,
        int rightSleeveShortenBy)
    {
        CheckIfShortenDataIsValid(leftTrouserLegShortenBy, rightTrouserLegShortenBy, leftSleeveShortenBy, rightSleeveShortenBy);
        
        LeftTrouserLegShortenBy = leftTrouserLegShortenBy;
        RightTrouserLegShortenBy = rightTrouserLegShortenBy;
        LeftSleeveShortenBy = leftSleeveShortenBy;
        RightSleeveShortenBy = rightSleeveShortenBy;
    }

    public int LeftTrouserLegShortenBy { get; }

    public int RightTrouserLegShortenBy { get; }

    public int LeftSleeveShortenBy { get; }

    public int RightSleeveShortenBy { get; }
    
    public string MessageForTailors()
    {
        List<string> alterations = new();
        string messageFormat = "{0} shorten by {1} centimeters";
        string separator = ", ";

        if (LeftTrouserLegShortenBy != default)
        {
            alterations.Add(string.Format(messageFormat, "Left trouser leg", LeftTrouserLegShortenBy));
        }
        
        if (RightTrouserLegShortenBy != default)
        {
            alterations.Add(string.Format(messageFormat, "Right trouser leg", RightTrouserLegShortenBy));
        }
        
        if (LeftSleeveShortenBy != default)
        {
            alterations.Add(string.Format(messageFormat, "Left sleeve", LeftSleeveShortenBy));
        }
        
        if (RightSleeveShortenBy != default)
        {
            alterations.Add(string.Format(messageFormat, "Right sleeve", RightSleeveShortenBy));
        }

        return string.Join(separator, alterations);
    }

    private void CheckIfShortenDataIsValid(params int[] shortenBy)
    {
        if (IsAllAlternationsEmpty(shortenBy))
        {
            throw new SuitsupplyBusinessException("No alternations provided.");
        }

        if (IsAnyAlternationBiggerThan(MaxAlternationDifferenceInCentimeters, shortenBy))
        {
            throw new SuitsupplyBusinessException($"Maximum change of +/- {MaxAlternationDifferenceInCentimeters} centimeters is allowed.");
        }
    }

    private static bool IsAllAlternationsEmpty(int[] shortenBy)
        => shortenBy.All(x => x == default);
    
    private static bool IsAnyAlternationBiggerThan(int maxDifference, int[] shortenBy)
        => !shortenBy.All(x => Math.Abs(x) <= maxDifference);
}