using System;
using System.Collections.Generic;
using Business.Entities;
using Core.Contracts;

namespace Business.Common
{
    public interface IDeveloperHiringEngine : IBusinessEngine
    {
        bool IsDeveloperAvailableForHire(int developerId, DateTime startDate, DateTime endDate,
            IEnumerable<Hired> hires, IEnumerable<Booking> bookings);

        bool IsDeveloperCurrentlyHired(int developerId, int accountId);
        bool IsDeveloperCurrentlyHired(int developerId);
        Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime startDate, DateTime enDate);
    }
}