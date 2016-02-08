using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using App.Common;
using Business.Common;
using Business.Entities;
using Core.Common.Exceptions;
using Core.Contracts;
using DataLayer.Contracts.Contracts;

namespace Business.Engine.Engine
{
    [Export(typeof(IDeveloperHiringEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DeveloperHiringEngine : IDeveloperHiringEngine
    {

        private readonly IDataRepositoryFactory _dataRepositoryFactory;

        [ImportingConstructor]
        public DeveloperHiringEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }


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

        public bool IsDeveloperCurrentlyHired(int developerId, int accountId)
        {
            var hired = false;
            var hiringRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();

            var currentHiring = hiringRepository.GetCurrentHiringByDeveloper(developerId);

            if (currentHiring != null && currentHiring.AccountId == accountId)
                hired = true;
            return hired;
        }

        public bool IsDeveloperCurrentlyHired(int developerId)
        {
            var hired = false;
            var hiringRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();

            var currentHiring = hiringRepository.GetCurrentHiringByDeveloper(developerId);

            if (currentHiring != null)
                hired = true;
            return hired;
        }

        public Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime startDate, DateTime endDate)
        {
            if (startDate > DateTime.Now)
                throw new UnableToHireForDateException($"Cannot hire for date {startDate.ToShortDateString()} yet");

            var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
            var hiringRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();

            var developerIsHired = IsDeveloperCurrentlyHired(developerId);

            if(developerIsHired)
                throw new DeveloperCurrentlyHiredException($"Developer {developerId} is already hired.");
            var account = accountRepository.GetByLogin(loginEmail);

            if(account == null)
                throw new NotFoundException($"No account found for such login email {loginEmail}");

            var hired = new Hired()
            {
                AccountId = account.AccountId,
                DeveloperId = developerId,
                StartDate = startDate,
                DateDue = endDate
            };

            var savedEntity = hiringRepository.Add(hired);

            return savedEntity;


        }
    }
}