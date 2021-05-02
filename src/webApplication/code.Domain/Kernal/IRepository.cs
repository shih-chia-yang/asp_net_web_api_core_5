namespace code.Domain.Kernal
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}