using FluentValidation;
using Suitsupply.Alteration.Domain.AlterationAggregate;

namespace Suitsupply.Alteration.Api.Commands.SendCustomerRequest;

public class SendCustomerRequestCommandValidator : AbstractValidator<SendCustomerRequestCommandDto>
{
    public SendCustomerRequestCommandValidator()
    {
        RuleFor(x => x.CustomerName).NotEmpty();
        RuleFor(x => x.CustomerEmail).NotEmpty().EmailAddress();
        
        RuleFor(x => x.LeftSleeveShortenBy)
            .LessThanOrEqualTo(+ShortenInfo.MaxAlternationDifferenceInCentimeters)
            .GreaterThanOrEqualTo(-ShortenInfo.MaxAlternationDifferenceInCentimeters);
        
        RuleFor(x => x.RightSleeveShortenBy)
            .LessThanOrEqualTo(+ShortenInfo.MaxAlternationDifferenceInCentimeters)
            .GreaterThanOrEqualTo(-ShortenInfo.MaxAlternationDifferenceInCentimeters);
        
        RuleFor(x => x.LeftTrouserLegShortenBy)
            .LessThanOrEqualTo(+ShortenInfo.MaxAlternationDifferenceInCentimeters)
            .GreaterThanOrEqualTo(-ShortenInfo.MaxAlternationDifferenceInCentimeters);
        
        RuleFor(x => x.RightTrouserLegShortenBy)
            .LessThanOrEqualTo(+ShortenInfo.MaxAlternationDifferenceInCentimeters)
            .GreaterThanOrEqualTo(-ShortenInfo.MaxAlternationDifferenceInCentimeters);
    }
}