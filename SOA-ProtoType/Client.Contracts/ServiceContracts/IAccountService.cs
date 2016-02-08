using System.ServiceModel;
using System.Threading.Tasks;
using App.Common;
using Clients.Entities;
using Core.Common.Exceptions;
using Core.Contracts;

namespace Client.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IAccountService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Account GetClientAccountInfo(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateClientAccountInfo(Account account);

        // ============= Async Operations ===================================

        [OperationContract]
        Task UpdateClientAccountInfoAsync(Account account);

        [OperationContract]
        Task<Account> GetClientAccountInfoAsync(string loginEmail);


    }
}