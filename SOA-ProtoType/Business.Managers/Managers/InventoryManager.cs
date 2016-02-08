using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using App.Common;
using Business.Common;
using Business.Contracts.ServiceContracts;
using Business.Entities;
using Core.Common.Core;
using Core.Common.Exceptions;
using Core.Contracts;
using DataLayer.Contracts.Contracts;

namespace Business.Managers.Managers
{
    // Each call happens on a new instance of a service and after the call is complete the service l'l be disposed.
    // Multiple-Concurrency : call(s) are serviced concurrently with no worry about multi-threading issues coz they are all serviced on different Service instance.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class InventoryManager : ManagerBase, IInventoryService
    {

        public InventoryManager()
        { }

        public InventoryManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        public InventoryManager(IBusinessEngineFactory businessEngineFactory)
        {
            _businessEngineFactory = businessEngineFactory;
        }

        public InventoryManager(IBusinessEngineFactory businessEngineFactory, IDataRepositoryFactory dataRepositoryFactory)
        {
            _businessEngineFactory = businessEngineFactory;
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;

        [Import] private IBusinessEngineFactory _businessEngineFactory;

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public Developer GetDeveloper(int developerId)
        {

            return ExecuteFaultHandledOperation(() =>
            {
                var developerRepository = _dataRepositoryFactory.GetDataRepository<IDeveloperRepository>();
                var developerEntity = developerRepository.Get(developerId);

                if (developerEntity == null)
                {
                    var ex =
                        new NotFoundException($"The Developer with such an ID {developerId} is not found in the database");

                    // This is how one throws an exception in WCF so that the Client whose on a different machine can caught this exception as 
                    // an SOAP message ( SOAP-Fault wrap).
                    // FaultException<T> l'l not Fault the proxy thus that proxy l'l still be reusable if needed.
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                return developerEntity;

            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public Developer[] GetAllDevelopers()
        {
            return ExecuteFaultHandledOperation(() => {

                var developerRepository = _dataRepositoryFactory.GetDataRepository<IDeveloperRepository>();
                var hiredRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();
                var developers = developerRepository.Get();
                var hiredDevelopers = hiredRepository.GetCurrentlyHiredDevelopers();

                foreach (var developer in developers)
                {
                    var hiredDeveloper = hiredDevelopers.Where(hired => hired.DeveloperId == developer.DeveloperId);
                    //developer.CurrentlyHired = (hiredDeveloper != null);
                    developer.CurrentlyHired = true;
                }

                return developers.ToArray();
            });
                
        }
        
        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public Developer UpdateDeveloper(Developer developer)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var develoeprRepository = _dataRepositoryFactory.GetDataRepository<IDeveloperRepository>();
                Developer updateEntity = null;

                updateEntity = developer.DeveloperId == 0 ? develoeprRepository.Add(developer) : develoeprRepository.Update(developer);

                return updateEntity;

            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public void DeleteDeveloper(int developerId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var developerRepository = _dataRepositoryFactory.GetDataRepository<IDeveloperRepository>();
                
                developerRepository.Remove(developerId);

            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public Developer[] GetAvailableDevelopers(DateTime startDate, DateTime enDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var developerRepository = _dataRepositoryFactory.GetDataRepository<IDeveloperRepository>();
                var hiredRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();

                var developerHiredEngine = _businessEngineFactory.GetBusinessEngine<IDeveloperHiringEngine>();

                var developers = developerRepository.Get();
                var hiredDeveloper = hiredRepository.GetCurrentlyHiredDevelopers();
                var bookedDevelopers = bookingRepository.Get();

                //foreach (var developer in developers)
                //{
                //    if(developerHiredEngine.IsDeveloperAvailableForHire(developer.DeveloperId, startDate, enDate, hiredDeveloper, bookedDevelopers))
                //        availableDevelopers.Add(developer);
                //}

                return developers.Where(developer => developerHiredEngine.IsDeveloperAvailableForHire(developer.DeveloperId, startDate, enDate, hiredDeveloper, bookedDevelopers)).ToArray();

            });
        }
    }
}