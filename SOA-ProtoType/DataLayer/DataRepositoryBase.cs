using System.Collections.Generic;
using Core.Common.Data;
using Core.Contracts;

namespace DataLayer
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, XplicitDbContext>
        where T : class , IIdentifiableEntity, new()
    {}
}