using System.Runtime.InteropServices.ComTypes;

namespace Core.Contracts
{
    public interface IDataRepositoryFactory
    {
         T GetDataRepository<T>() where T : IDataRepository;
    }
}