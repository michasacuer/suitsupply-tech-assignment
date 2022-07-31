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
        
        _leftTrouserLegShortenBy = leftTrouserLegShortenBy;
        _rightTrouserLegShortenBy = rightTrouserLegShortenBy;
        _leftSleeveShortenBy = leftSleeveShortenBy;
        _rightSleeveShortenBy = rightSleeveShortenBy;
    }

    private readonly int _leftTrouserLegShortenBy;

    private readonly int _rightTrouserLegShortenBy;

    private readonly int _leftSleeveShortenBy;

    private readonly int _rightSleeveShortenBy;
    
    public string MessageForTailors()
    {
        List<string> alterations = new();
        string messageFormatShorten = "{0} shorten by {1} centimeters";
        string messageFormatExtend = "{0} extend by {1} centimeters";
        string separator = ", ";
        string currentFormat;
        
        if (_leftTrouserLegShortenBy != default)
        {
            currentFormat = _leftTrouserLegShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Left trouser leg", Math.Abs(_leftTrouserLegShortenBy)));
        }
        
        if (_rightTrouserLegShortenBy != default)
        {
            currentFormat = _rightTrouserLegShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Right trouser leg", Math.Abs(_rightTrouserLegShortenBy)));
        }
        
        if (_leftSleeveShortenBy != default)
        {
            currentFormat = _leftSleeveShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Left sleeve", Math.Abs(_leftSleeveShortenBy)));
        }
        
        if (_rightSleeveShortenBy != default)
        {
            currentFormat = _rightSleeveShortenBy > 0 ? messageFormatExtend : messageFormatShorten;
            alterations.Add(string.Format(currentFormat, "Right sleeve", Math.Abs(_rightSleeveShortenBy)));
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