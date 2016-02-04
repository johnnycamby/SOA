using Core.Common.Core;
using Core.Contracts;

namespace Business.Engine
{
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        public T GetDataRepository<T>() where T : IBusinessEngine
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }
    }
}