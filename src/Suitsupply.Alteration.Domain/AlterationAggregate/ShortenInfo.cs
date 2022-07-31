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
        string messageFormatShorten = "{0} shorten by {1} centimeters";
        string messageFormatExtend = "{0} extend by {1} centimeters";
        string separator = ", ";
        string currentFormat;
        
        if (LeftTrouserLegShortenBy != default)
        {
            currentFormat = LeftTrouserLegShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Left trouser leg", Math.Abs(LeftTrouserLegShortenBy)));
        }
        
        if (RightTrouserLegShortenBy != default)
        {
            currentFormat = RightTrouserLegShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Right trouser leg", Math.Abs(RightTrouserLegShortenBy)));
        }
        
        if (LeftSleeveShortenBy != default)
        {
            currentFormat = LeftSleeveShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Left sleeve", Math.Abs(LeftSleeveShortenBy)));
        }
        
        if (RightSleeveShortenBy != default)
        {
            currentFormat = RightSleeveShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Right sleeve", Math.Abs(RightSleeveShortenBy)));
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