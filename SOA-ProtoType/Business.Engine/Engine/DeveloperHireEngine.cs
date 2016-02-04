using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Business.Common;
using Business.Entities;

namespace Business.Engine.Engine
{
    [Export(typeof(IDeveloperHireEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DeveloperHireEngine : IDeveloperHireEngine
    {
        public bool IsDeveloperAvailableForHire(int developerId, DateTime startDate, DateTime endDate,
            IEnumerable<Hired> hires, IEnumerable<Booking> bookings)
        {
            var available = true;

            var booked = bookings.FirstOrDefault(i => i.DeveloperId == developerId);

            if (booked != null && (startDate >= booked.StartDate && startDate <= booked.EndDate) || (endDate >= booked.StartDate && endDate <= booked.EndDate))
            {
                available = false;
            }

            if (available)
            {
                var hired = hires.FirstOrDefault(i => i.DeveloperId == developerId);

                if (hired != null && (startDate <= hired.DateDue))
                    available = false;
            }

            return available;

        }
    }
}