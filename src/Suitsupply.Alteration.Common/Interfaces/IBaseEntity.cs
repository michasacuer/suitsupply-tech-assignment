namespace Suitsupply.Alteration.Common.Interfaces;

public interface IBaseEntity<out T>
    where T : struct
{
    T Id { get; }
}