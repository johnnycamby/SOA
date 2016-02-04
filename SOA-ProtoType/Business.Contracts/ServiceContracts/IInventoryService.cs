using System;
using System.ServiceModel;
using Business.Entities;
using Core.Common.Exceptions;

namespace Business.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IInventoryService
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
        Developer[] GetAvailableDevelopers(DateTime startDate, DateTime enDate);

    }
}