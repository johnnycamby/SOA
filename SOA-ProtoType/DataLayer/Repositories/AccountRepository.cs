using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Business.Entities;
using Core.Common.Data;
using DataLayer.Contracts.Contracts;

namespace DataLayer.Repositories
{
    [Export(typeof(IAccountRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)] // for MEF not to use Singleton-Pattern (by default) to resolve Type(s)
    public class AccountRepository : DataRepositoryBase<Account>, IAccountRepository
    {
        protected override Account AddEntity(XplicitDbContext dbctx, Account entity)
        {
            return dbctx.AccountSet.Add(entity);
        }

        protected override Account UpdateEntity(XplicitDbContext dbctx, Account entity)
        {
            return (from account in dbctx.AccountSet where account.AccountId == entity.AccountId select account).FirstOrDefault();
        }

        protected override IEnumerable<Account> GetEntities(XplicitDbContext dbctx)
        {
            return from account in dbctx.AccountSet
                select account;
        }

        protected override Account GetEntity(XplicitDbContext dbctx, int id)
        {
            var query = (from account in dbctx.AccountSet
                where account.AccountId == id
                select account);
            var results = query.FirstOrDefault();
            return results;
        }


        public Account GetByLogin(string login)
        {
            using (var dbctx = new XplicitDbContext())
            {
                return (
                    from account in dbctx.AccountSet where account.LoginEmail == login select account
                    ).FirstOrDefault();
            }
        }
    }
}