using System.ComponentModel.Composition;
using System.Security.Permissions;
using System.ServiceModel;
using App.Common;
using Business.Contracts.ServiceContracts;
using Business.Entities;
using Core.Common.Exceptions;
using Core.Contracts;
using DataLayer.Contracts.Contracts;

namespace Business.Managers.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, ReleaseServiceInstanceOnTransactionComplete = false)] // If you set to TransactionScopeRequired 'true' on an operation/method, then you 've to set to ReleaseServiceInstanceOnTransactionComplete  'false'
    public class AccountManager : ManagerBase, IAccountService
    {

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;

        // default for WCF usage
        public AccountManager()
        {}

        // non-default for UnitTests
        public AccountManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
            var authAccount = accountRepository.GetByLogin(loginName);

            if (authAccount == null)
            {
                var ex = new NotFoundException($"Cannot find account with such a {loginName}");
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return authAccount;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public Account GetClientAccountInfo(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();

                var account = accountRepository.GetByLogin(loginEmail);

                if (account == null)
                {
                    var ex = new NotFoundException($"Cannot find account with such a {loginEmail}");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(account);

                return account;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public void UpdateClientAccountInfo(Account account)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();

                ValidateAuthorization(account);
                accountRepository.Update(account);
            });
        }
    }
}