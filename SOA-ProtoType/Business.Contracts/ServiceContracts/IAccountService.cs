using System.ServiceModel;
using App.Common;
using Business.Entities;
using Core.Common.Exceptions;

namespace Business.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Account GetClientAccountInfo(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void UpdateClientAccountInfo(Account account);

    }
}