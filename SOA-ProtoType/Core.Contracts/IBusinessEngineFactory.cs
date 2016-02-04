namespace Core.Contracts
{
    public interface IBusinessEngineFactory
    {
        T GetDataRepository<T>() where T : IBusinessEngine;
    }
}