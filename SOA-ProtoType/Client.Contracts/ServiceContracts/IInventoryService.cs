using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Clients.Entities;
using Core.Common.Exceptions;
using Core.Contracts;

namespace Client.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IInventoryService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))] // Notify WCF about rising of FaultException<T> E.g 'NotFoundException' for better serialization upto Client-level
        Developer GetDeveloper(int developerId);

        [OperationContract]
        Developer[] GetAllDevelopers();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)] // Because it l'l be affecting the db or data state. 
        Developer UpdateDeveloper(Developer developer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDeveloper(int developerId);

        [OperationContract]
        Developer[] GetAvailableDevelopers(DateTime startDate, DateTime endDate);

        // ============ Async Operations =========================

        [OperationContract]
        Task<Developer> UpdateDeveloperAsync(Developer developer);

        [OperationContract]
        Task<Developer[]> GetAvailableDevelopersAsync(DateTime startDate, DateTime endDate);

        [OperationContract]
       Task DeleteDeveloperAsync(int developerId);

        [OperationContract]
        Task<Developer[]> GetAllDevelopersAsync();

        [OperationContract]
       Task<Developer> GetDeveloperAsync(int developerId);



    }
}