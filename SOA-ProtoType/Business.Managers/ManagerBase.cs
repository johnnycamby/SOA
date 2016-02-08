using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Common;
using Business.Entities;
using Core.Common.Core;
using Core.Contracts;

namespace Business.Managers
{
    public class ManagerBase
    {
        protected string LoginName;
        protected Account AuthorizationAccount;


        public ManagerBase()
        {
            // deal with in coming message-headers
            var context = OperationContext.Current;

            if (context != null)
            {
                LoginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");

                // '\' means LoginName came from a desktop application login
                if (LoginName.IndexOf(@"\", StringComparison.Ordinal) > 1)
                    LoginName = string.Empty;
            }

            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);

            if (!string.IsNullOrWhiteSpace(LoginName))
                AuthorizationAccount = LoadAuthorizationValidationAccount(LoginName);

        }
        
        protected virtual Account LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected void ValidateAuthorization(IAccountOwnedEntity entity)
        {
            if (Thread.CurrentPrincipal.IsInRole(Security.AppAdmin))
            {
                if (LoginName != string.Empty && entity.OwnerAccountId != AuthorizationAccount.AccountId)
                {
                    var ex = new AuthorizationValidationException("Attempting to access a secure record!");
                    throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
                }
            }

        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute)
        {
            try
            {
                return codeToExecute.Invoke();
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Better way to handle exception(s) the way SOAP would expect it.
                throw new FaultException(ex.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codeToExecute)
        {
            try
            {
                codeToExecute.Invoke();
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

    }
}
