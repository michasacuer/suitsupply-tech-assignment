namespace Suitsupply.Alteration.Common.Exceptions;

public class SuitsupplyBusinessException : Exception
{
    public SuitsupplyBusinessException()
    {
    }

    public SuitsupplyBusinessException(string message) : base(message)
    {
    }
}