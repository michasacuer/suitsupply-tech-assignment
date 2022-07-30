namespace Suitsupply.Alteration.Common.Interfaces;

public interface IBaseEntity<T>
    where T : struct
{
    T Id { get; }
}