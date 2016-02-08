using System;
using System.Collections.Generic;
using System.ServiceModel;
using App.Common;
using Business.Contracts.DataContracts;
using Business.Entities;
using Core.Common.Exceptions;

namespace Business.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IHiringService
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        IEnumerable<Hired> GetHiringHistory(string loginEmail);

        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(DeveloperCurrentlyHiredException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime dateDueBack);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(DeveloperCurrentlyHiredException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime hiredDate, DateTime dateDueBack);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AcceptDeveloperReturn(int developerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Booking GetBooking(int bookingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Booking MakeBooking(string loginEmail, int developerId, DateTime hiredDate, DateTime returnDate);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ExecuteHiringFromBooking(int bookingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CancelBooking(int bookingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        ClientBookingData[] GetCurrentBookings();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        ClientBookingData[] GetClientBookings(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Hired GetHired(int hiredId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        ClientHiringData[] GetCurrentHires();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Booking[] GetDeadBookings();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        bool IsDeveloperCurrentlyHired(int developerId);
    }
}