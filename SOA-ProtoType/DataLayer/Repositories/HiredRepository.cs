using System.Collections.Generic;
using System.ComponentModel.Composition;
using Business.Entities;
using DataLayer.Contracts.Contracts;
using DataLayer.Contracts.DTOs;
using System.Linq;
using Core.Common.Extensions;

namespace DataLayer.Repositories
{
    [Export(typeof(IHiredRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HiredRepository : DataRepositoryBase<Hired>, IHiredRepository
    {
        protected override Hired AddEntity(XplicitDbContext dbctx, Hired entity)
        {
            return dbctx.HiredSet.Add(entity);
        }

        protected override Hired UpdateEntity(XplicitDbContext dbctx, Hired entity)
        {
            return (from hired in dbctx.HiredSet
                where hired.HiredId == entity.HiredId
                select hired).FirstOrDefault();
        }

        protected override IEnumerable<Hired> GetEntities(XplicitDbContext dbctx)
        {
            return from hired in dbctx.HiredSet
                select hired;
        }

        protected override Hired GetEntity(XplicitDbContext dbctx, int id)
        {
            var query = (from hired in dbctx.HiredSet
                where hired.HiredId == id
                select hired);
            var result = query.FirstOrDefault();

            return result;
        }

        public IEnumerable<Hired> GetHireHistoryByDeveloper(int developerId)
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = (from hired in dbctx.HiredSet
                    where hired.DeveloperId == developerId
                    select hired);

                return query.ToFullyLoaded();
            }
        }

        public Hired GetCurrentHireByDeveloper(int developerId)
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = from hired in dbctx.HiredSet
                    where hired.DeveloperId == developerId && hired.EndDate == null
                    select hired;

                return query.FirstOrDefault();
            }
        }

        public IEnumerable<Hired> GetCurrentlyHiredDevelopers()
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = from hired in dbctx.HiredSet
                    where hired.EndDate == null
                    select hired;
                return query.ToFullyLoaded();
            }
            
        }

        public IEnumerable<Hired> GetHireHistoryByAccount(int accountId)
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = from hired in dbctx.HiredSet
                    where hired.AccountId == accountId
                    select hired;
                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ClientHireInfo> GetCurrentClientHireInfo()
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = from hired in dbctx.HiredSet
                    where hired.EndDate == null
                    join clientAccount in dbctx.AccountSet on hired.AccountId equals clientAccount.AccountId
                    join developer in dbctx.DeveloperSet on hired.DeveloperId equals developer.DeveloperId

                    select new ClientHireInfo()
                    {
                        Client = clientAccount,
                        Developer = developer,
                        Hired = hired
                    };

                return query.ToFullyLoaded();


            }
        }
    }
}