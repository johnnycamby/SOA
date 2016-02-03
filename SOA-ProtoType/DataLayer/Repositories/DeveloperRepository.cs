using System.Collections.Generic;
using System.ComponentModel.Composition;
using Business.Entities;
using DataLayer.Contracts.Contracts;
using System.Linq;

namespace DataLayer.Repositories
{
    [Export(typeof(IDeveloperRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DeveloperRepository : DataRepositoryBase<Developer>, IDeveloperRepository
    {
        protected override Developer AddEntity(XplicitDbContext dbctx, Developer entity)
        {
            return dbctx.DeveloperSet.Add(entity);
        }

        protected override Developer UpdateEntity(XplicitDbContext dbctx, Developer entity)
        {
            return
                (from developer in dbctx.DeveloperSet where developer.DeveloperId == entity.DeveloperId select developer)
                    .FirstOrDefault();

        }

        protected override IEnumerable<Developer> GetEntities(XplicitDbContext dbctx)
        {
            return from developer in dbctx.DeveloperSet
                select developer;
        }

        protected override Developer GetEntity(XplicitDbContext dbctx, int id)
        {
            var query = (from developer in dbctx.DeveloperSet
                where developer.DeveloperId == id
                select developer);
            var result = query.FirstOrDefault();

            return result;
        }
    }
}