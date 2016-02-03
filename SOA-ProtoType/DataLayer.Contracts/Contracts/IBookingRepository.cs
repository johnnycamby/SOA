using System;
using System.Collections.Generic;
using Business.Entities;
using Core.Contracts;
using DataLayer.Contracts.DTOs;

namespace DataLayer.Contracts.Contracts
{
    public interface IBookingRepository: IDataRepository<Booking>
    {
        IEnumerable<Booking> GetBookingsByPickupDate(DateTime pickupDate);
        IEnumerable<ClientBookingInfo> GetCurrentClientBookingInfo();
        IEnumerable<ClientBookingInfo> GetClientOpenBookingInfos(int accountId);

    }
}