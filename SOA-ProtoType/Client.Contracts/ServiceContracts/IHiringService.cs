using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using App.Common;
using Client.Contracts.DataContracts;
using Clients.Entities;
using Core.Common.Exceptions;
using Core.Contracts;

namespace Client.Contracts.ServiceContracts
{
    [ServiceContract]
    public interface IHiringService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        IEnumerable<Hired> GetHiringHistory(string loginEmail);

        [OperationContract]
        Task<IEnumerable<Hired>> GetHiringHistoryAsync(string loginEmail);

        [OperationContract(Name = "HireDeveloperToClientImmediately")]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(DeveloperCurrentlyHiredException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime dateDueBack);

        [OperationContract(Name = "HireDeveloperToClientImmediately")]
        Task<Hired> HireDeveloperToClientAsync(string loginEmail, int developerId, DateTime dateDueBack);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(DeveloperCurrentlyHiredException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime hiredDate, DateTime dateDueBack);

        [OperationContract]
        Task<Hired> HireDeveloperToClientAsync(string loginEmail, int developerId, DateTime hiredDate, DateTime dateDueBack);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void AcceptDeveloperReturn(int developerId);

        [OperationContract]
        Task AcceptDeveloperReturnAsync(int developerId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Booking GetBooking(int bookingId);

        [OperationContract]
        Task<Booking> GetBookingAsync(int bookingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        Booking MakeBooking(string loginEmail, int developerId, DateTime hiredDate, DateTime returnDate);

        [OperationContract]
        Task<Booking> MakeBookingAsync(string loginEmail, int developerId, DateTime hiredDate, DateTime returnDate);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void ExecuteHiringFromBooking(int bookingId);

        [OperationContract]
        Task ExecuteHiringFromBookingAsync(int bookingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CancelBooking(int bookingId);

        [OperationContract]
        Task CancelBookingAsync(int bookingId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        ClientBookingData[] GetCurrentBookings();

        [OperationContract]
        Task<ClientBookingData[]> GetCurrentBookingsAsync();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        ClientBookingData[] GetClientBookings(string loginEmail);

        [OperationContract]
        Task<ClientBookingData[]> GetClientBookingsAsync(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Hired GetHired(int hiredId);

        [OperationContract]
        Task<Hired> GetHiredAsync(int hiredId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        ClientHiringData[] GetCurrentHires();

        [OperationContract]
        Task<ClientHiringData[]> GetCurrentHiresAsync();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Booking[] GetDeadBookings();

        [OperationContract]
        Task<Booking[]> GetDeadBookingsAsync();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        bool IsDeveloperCurrentlyHired(int developerId);

        [OperationContract]
        Task<bool> IsDeveloperCurrentlyHiredAsync(int developerId);
    }
}