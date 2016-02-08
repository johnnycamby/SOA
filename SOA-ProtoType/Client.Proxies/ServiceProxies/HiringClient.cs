using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.ServiceModel;
using System.Threading.Tasks;
using Client.Contracts.DataContracts;
using Client.Contracts.ServiceContracts;
using Clients.Entities;
using Core.Common.ServiceModel;

namespace Client.Proxies.ServiceProxies
{
    [Export(typeof(IHiringService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HiringClient : UserClientBase<IHiringService> , IHiringService
    {
        public IEnumerable<Hired> GetHiringHistory(string loginEmail)
        {
            return Channel.GetHiringHistory(loginEmail);
        }

        public Task<IEnumerable<Hired>> GetHiringHistoryAsync(string loginEmail)
        {
            return Channel.GetHiringHistoryAsync(loginEmail);
        }

        public Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime dateDueBack)
        {
            return Channel.HireDeveloperToClient(loginEmail, developerId, dateDueBack);
        }

        public Task<Hired> HireDeveloperToClientAsync(string loginEmail, int developerId, DateTime dateDueBack)
        {
            return Channel.HireDeveloperToClientAsync(loginEmail, developerId, dateDueBack);
        }

        public Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime hiredDate, DateTime dateDueBack)
        {
            return Channel.HireDeveloperToClient(loginEmail, developerId, hiredDate, dateDueBack);
        }

        public Task<Hired> HireDeveloperToClientAsync(string loginEmail, int developerId, DateTime hiredDate, DateTime dateDueBack)
        {
            return Channel.HireDeveloperToClientAsync(loginEmail, developerId, hiredDate, dateDueBack);
        }

        public void AcceptDeveloperReturn(int developerId)
        {
            Channel.AcceptDeveloperReturn(developerId);
        }

        public Task AcceptDeveloperReturnAsync(int developerId)
        {
            return Channel.AcceptDeveloperReturnAsync(developerId);
        }

        public Booking GetBooking(int bookingId)
        {
            return Channel.GetBooking(bookingId);
        }

        public Task<Booking> GetBookingAsync(int bookingId)
        {
            return Channel.GetBookingAsync(bookingId);
        }

        public Booking MakeBooking(string loginEmail, int developerId, DateTime hiredDate, DateTime returnDate)
        {
            return Channel.MakeBooking(loginEmail, developerId, hiredDate, returnDate);
        }

        public Task<Booking> MakeBookingAsync(string loginEmail, int developerId, DateTime hiredDate, DateTime returnDate)
        {
            return Channel.MakeBookingAsync(loginEmail, developerId, hiredDate, returnDate);
        }

        public void ExecuteHiringFromBooking(int bookingId)
        {
            Channel.ExecuteHiringFromBooking(bookingId);
        }

        public Task ExecuteHiringFromBookingAsync(int bookingId)
        {
            return Channel.ExecuteHiringFromBookingAsync(bookingId);
        }

        public void CancelBooking(int bookingId)
        {
            Channel.CancelBooking(bookingId);
        }

        public Task CancelBookingAsync(int bookingId)
        {
            return Channel.CancelBookingAsync(bookingId);
        }

        public ClientBookingData[] GetCurrentBookings()
        {
            return Channel.GetCurrentBookings();
        }

        public Task<ClientBookingData[]> GetCurrentBookingsAsync()
        {
            return Channel.GetCurrentBookingsAsync();
        }

        public ClientBookingData[] GetClientBookings(string loginEmail)
        {
            return Channel.GetClientBookings(loginEmail);
        }

        public Task<ClientBookingData[]> GetClientBookingsAsync(string loginEmail)
        {
            return Channel.GetClientBookingsAsync(loginEmail);
        }

        public Hired GetHired(int hiredId)
        {
            return Channel.GetHired(hiredId);
        }

        public Task<Hired> GetHiredAsync(int hiredId)
        {
            return Channel.GetHiredAsync(hiredId);
        }

        public ClientHiringData[] GetCurrentHires()
        {
            return Channel.GetCurrentHires();
        }

        public Task<ClientHiringData[]> GetCurrentHiresAsync()
        {
            return Channel.GetCurrentHiresAsync();
        }

        public Booking[] GetDeadBookings()
        {
            return Channel.GetDeadBookings();
        }

        public Task<Booking[]> GetDeadBookingsAsync()
        {
            return Channel.GetDeadBookingsAsync();
        }

        public bool IsDeveloperCurrentlyHired(int developerId)
        {
            return Channel.IsDeveloperCurrentlyHired(developerId);
        }

        public Task<bool> IsDeveloperCurrentlyHiredAsync(int developerId)
        {
            return Channel.IsDeveloperCurrentlyHiredAsync(developerId);
        }
    }
}