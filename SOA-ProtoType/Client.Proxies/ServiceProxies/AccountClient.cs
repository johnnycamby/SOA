using System.ComponentModel.Composition;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.Contracts.ServiceContracts;
using Clients.Entities;
using Core.Common.ServiceModel;

namespace Client.Proxies.ServiceProxies
{
    [Export(typeof(IAccountService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountClient : UserClientBase<IAccountService> ,IAccountService
    {
        public Account GetClientAccountInfo(string loginEmail)
        {
            return Channel.GetClientAccountInfo(loginEmail);
        }

        public void UpdateClientAccountInfo(Account account)
        {
            Channel.UpdateClientAccountInfo(account);
        }

        public Task UpdateClientAccountInfoAsync(Account account)
        {
            return Channel.UpdateClientAccountInfoAsync(account);
        }

        public Task<Account> GetClientAccountInfoAsync(string loginEmail)
        {
            return Channel.GetClientAccountInfoAsync(loginEmail);
        }
    }
}