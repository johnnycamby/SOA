using Business.Entities;
using Core.Contracts;

namespace DataLayer.Contracts.Contracts
{
    public interface IAccountRepository : IDataRepository<Account>
    {
        Account GetByLogin(string login);
    }
}