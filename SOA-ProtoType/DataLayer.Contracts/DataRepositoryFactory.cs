using Core.Common.Core;
using Core.Contracts;

namespace DataLayer.Contracts
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        public T GetDataRepository<T>() where T : IDataRepository
        {
            // Resolve something(T) thru MEF
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}