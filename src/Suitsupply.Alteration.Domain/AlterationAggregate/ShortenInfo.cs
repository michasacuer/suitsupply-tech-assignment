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
        string separator = ", ";
        string currentFormat;
        
        if (LeftTrouserLegShortenBy != default)
        {
            currentFormat = LeftTrouserLegShortenBy > 0 ? InfoMessages.EXTEND_BY_FORMAT : InfoMessages.SHORTEN_BY_FORMAT;
            alterations.Add(string.Format(currentFormat, InfoMessages.LEFT_TROUSER, Math.Abs(LeftTrouserLegShortenBy)));
        }
        
        if (RightTrouserLegShortenBy != default)
        {
            currentFormat = RightTrouserLegShortenBy > 0 ? InfoMessages.EXTEND_BY_FORMAT : InfoMessages.SHORTEN_BY_FORMAT;
            alterations.Add(string.Format(currentFormat, InfoMessages.RIGHT_TROUSER, Math.Abs(RightTrouserLegShortenBy)));
        }
        
        if (LeftSleeveShortenBy != default)
        {
            currentFormat = LeftSleeveShortenBy > 0 ? InfoMessages.EXTEND_BY_FORMAT : InfoMessages.SHORTEN_BY_FORMAT;
            alterations.Add(string.Format(currentFormat, InfoMessages.LEFT_SLEEVE, Math.Abs(LeftSleeveShortenBy)));
        }
        
        if (RightSleeveShortenBy != default)
        {
            currentFormat = RightSleeveShortenBy > 0 ? InfoMessages.EXTEND_BY_FORMAT : InfoMessages.SHORTEN_BY_FORMAT;
            alterations.Add(string.Format(currentFormat, InfoMessages.RIGHT_SLEEVE, Math.Abs(RightSleeveShortenBy)));
        }

        return string.Join(separator, alterations);
    }

    private void CheckIfShortenDataIsValid(params int[] shortenBy)
    {
        if (IsAllAlternationsEmpty(shortenBy))
        {
            throw new SuitsupplyBusinessException(ErrorMessages.NO_ALTERATIONS);
        }

        if (IsAnyAlternationBiggerThan(MaxAlternationDifferenceInCentimeters, shortenBy))
        {
            throw new SuitsupplyBusinessException(string.Format(ErrorMessages.MAXIMUM_CHANGE_ONLY_ALLOWED_FORMAT, MaxAlternationDifferenceInCentimeters));
        }
    }

    private static bool IsAllAlternationsEmpty(int[] shortenBy)
        => shortenBy.All(x => x == default);
    
    private static bool IsAnyAlternationBiggerThan(int maxDifference, int[] shortenBy)
        => !shortenBy.All(x => Math.Abs(x) <= maxDifference);
}