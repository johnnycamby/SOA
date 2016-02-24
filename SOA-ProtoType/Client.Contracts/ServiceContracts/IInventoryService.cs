using System;
using System.Collections.Generic;
using System.Linq;
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
        Developer GetDeveloper(Guid developerId);

        [OperationContract]
        Developer[] GetAllDevelopers();

        [OperationContract]
        //Developer[] GetAllDevelopers(string ownerId);
        IQueryable<Developer> GetAllDevelopers(string ownerId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)] // Because it l'l be affecting the db or data state. 
        Developer UpdateDeveloper(Developer developer);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDeveloper(Guid developerId);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteDeveloper(string ownerId, Guid developerId);

        [OperationContract]
        Developer[] GetAvailableDevelopers(DateTime startDate, DateTime endDate);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Developer AddDeveloper(Developer developer , string ownerId);

        // ============ Async Operations =========================

        [OperationContract]
        Task<Developer> UpdateDeveloperAsync(Developer developer);

        [OperationContract]
        Task<Developer[]> GetAvailableDevelopersAsync(DateTime startDate, DateTime endDate);

        [OperationContract]
        Task DeleteDeveloperAsync(Guid developerId);

        [OperationContract]
        Task<Developer[]> GetAllDevelopersAsync();

        [OperationContract]
        Task<Developer> GetDeveloperAsync(Guid developerId);

        [OperationContract]
        Task DeleteDeveloperAsync(string ownerId, Guid developerId);

        [OperationContract]
        Task<Developer> AddDeveloperAsync(Developer developer, string ownerId);

        [OperationContract]
        //Developer[] GetAllDevelopers(string ownerId);
       Task<IQueryable<Developer>> GetAllDevelopersAsync(string ownerId);

    }
}