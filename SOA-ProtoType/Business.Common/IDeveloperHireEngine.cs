using System;
using System.Collections.Generic;
using Business.Entities;
using Core.Contracts;

namespace Business.Common
{
    public interface IDeveloperHireEngine : IBusinessEngine
    {
        bool IsDeveloperAvailableForHire(int developerId, DateTime startDate, DateTime endDate,
            IEnumerable<Hired> hires, IEnumerable<Booking> bookings);
    }
}