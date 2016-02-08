using System.ServiceModel;
using System.Threading;

namespace Core.Common.ServiceModel
{
    public abstract class UserClientBase<T> : ClientBase<T> where T : class 
    {
        protected UserClientBase()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            var header = new MessageHeader<string>(userName);
            var context = new OperationContextScope(InnerChannel);

            OperationContext.Current.OutgoingMessageHeaders.Add(header.GetUntypedHeader("string", "System"));

        }
    }
}