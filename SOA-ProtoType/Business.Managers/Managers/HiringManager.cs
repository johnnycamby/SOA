using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using App.Common;
using Business.Common;
using Business.Contracts.DataContracts;
using Business.Contracts.ServiceContracts;
using Business.Entities;
using Core.Common.Exceptions;
using Core.Contracts;
using DataLayer.Contracts.Contracts;

namespace Business.Managers.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class HiringManager : ManagerBase, IHiringService
    {

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;

        [Import]
        private IBusinessEngineFactory _businessEngineFactory;

        public HiringManager()
        { }

        public HiringManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        public HiringManager(IBusinessEngineFactory businessEngineFactory)
        {
            _businessEngineFactory = businessEngineFactory;
        }

        public HiringManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
            _businessEngineFactory = businessEngineFactory;
        }

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
            var authAccount = accountRepository.GetByLogin(loginName);

            if (authAccount == null)
            {
                var ex = new NotFoundException($"Cannot find an account with such a {loginName} to use for security purposes!");

                throw new FaultException<NotFoundException>(ex, ex.Message);
            }


            return authAccount;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public IEnumerable<Hired> GetHiringHistory(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                var hiringRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();

                var account = accountRepository.GetByLogin(loginEmail);

                if (account == null)
                {
                    var ex = new NotFoundException($"No Account with such an {loginEmail} was found!");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(account);

                var hiringHistory = hiringRepository.GetHiringHistoryByAccount(account.AccountId);

                return hiringHistory;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime dateDueBack)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var developerHiringEngine = _businessEngineFactory.GetBusinessEngine<IDeveloperHiringEngine>();

                try
                {
                    var hiring = developerHiringEngine.HireDeveloperToClient(loginEmail, developerId, DateTime.Now, dateDueBack);
                    return hiring;
                }
                catch (UnableToHireForDateException ex)
                {

                    throw new FaultException<UnableToHireForDateException>(ex, ex.Message);
                }
                catch (DeveloperCurrentlyHiredException ex)
                {
                    throw new FaultException<DeveloperCurrentlyHiredException>(ex, ex.Message);
                }
                catch (NotFoundException ex)
                {
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public Hired HireDeveloperToClient(string loginEmail, int developerId, DateTime hiredDate, DateTime dateDueBack)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var developerHiringEngine = _businessEngineFactory.GetBusinessEngine<IDeveloperHiringEngine>();

                try
                {
                    var hiring = developerHiringEngine.HireDeveloperToClient(loginEmail, developerId, hiredDate,
                        dateDueBack);

                    return hiring;
                }
                catch (UnableToHireForDateException ex)
                {

                    throw new FaultException<UnableToHireForDateException>(ex, ex.Message);
                }
                catch (DeveloperCurrentlyHiredException ex)
                {
                    throw new FaultException<DeveloperCurrentlyHiredException>(ex, ex.Message);
                }
                catch (NotFoundException ex)
                {
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public void AcceptDeveloperReturn(int developerId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var hiringRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();
              
                var hired = hiringRepository.GetCurrentHiringByDeveloper(developerId);

                if (hired == null)
                {
                    var ex = new DeveloperNotHiredException($"Developer {developerId} is currently not hired");
                    throw new FaultException<DeveloperNotHiredException>(ex, ex.Message);
                }

                hired.EndDate = DateTime.Now;
                hiringRepository.Update(hired);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public Booking GetBooking(int bookingId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();
                var booking = bookingRepository.Get(bookingId);

                if (booking == null)
                {
                    var ex = new NotFoundException($"No booking found for id {bookingId}");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(booking);

                return booking;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public Booking MakeBooking(string loginEmail, int developerId, DateTime hiredDate, DateTime returnDate)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();

                var account = accountRepository.GetByLogin(loginEmail);

                if(account == null)
                {
                    var ex = new NotFoundException($"No account found for such a loginemail {loginEmail}");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(account);

                var booking = new Booking()
                {
                    AccountId = account.AccountId,
                    DeveloperId = developerId,
                    StartDate = hiredDate,
                    EndDate = returnDate
                };

                var saveEntity = bookingRepository.Add(booking);

                return saveEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public void ExecuteHiringFromBooking(int bookingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();
                var developerHiringEngine = _businessEngineFactory.GetBusinessEngine<IDeveloperHiringEngine>();
                var booking = bookingRepository.Get(bookingId);

                if (booking == null)
                {
                    var ex = new NotFoundException($"No booking record found for id '{bookingId}'");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                var account = accountRepository.Get(booking.AccountId);

                if (account == null)
                {
                    var ex = new NotFoundException($"No account record found for id '{booking.AccountId}'");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                try
                {
                    var hired = developerHiringEngine.HireDeveloperToClient(account.LoginEmail, booking.DeveloperId,
                        booking.StartDate, booking.EndDate);
                }
                catch (UnableToHireForDateException ex)
                {
                    throw new FaultException<UnableToHireForDateException>(ex, ex.Message);
                }
                catch (DeveloperCurrentlyHiredException ex)
                {
                    throw new FaultException<DeveloperCurrentlyHiredException>(ex, ex.Message);
                }
                catch (NotFoundException ex)
                {
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                bookingRepository.Remove(booking);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public void CancelBooking(int bookingId)
        {
            ExecuteFaultHandledOperation(() =>
            {
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();
                var booking = bookingRepository.Get(bookingId);

                if (booking == null)
                {
                    var ex = new NotFoundException($"No booking record found for id '{bookingId}'");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(booking);

                bookingRepository.Remove(bookingId);
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public ClientBookingData[] GetCurrentBookings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();
                var bookingInfoSet = bookingRepository.GetCurrentClientBookingInfo();

                return bookingInfoSet.Select(bookingInfo => new ClientBookingData()
                {
                    BookingId = bookingInfo.Booking.BookedId,
                    Developer = bookingInfo.Developer.Email + " " + bookingInfo.Developer.YearsOfExprience + " " + bookingInfo.Developer.Description,
                    ClientName = bookingInfo.Client.FirstName + " " + bookingInfo.Client.LastName,
                    HiredDate = bookingInfo.Booking.StartDate,
                    ReturnDate = bookingInfo.Booking.EndDate

                }).ToArray();
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public ClientBookingData[] GetClientBookings(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var accountRepository = _dataRepositoryFactory.GetDataRepository<IAccountRepository>();
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();
                var account = accountRepository.GetByLogin(loginEmail);

                if (account == null)
                {
                    var ex = new NotFoundException($"No account found for login-email '{loginEmail}'");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(account);

               // var bookingData = new List<ClientBookingData>();
                var bookingInfoSet = bookingRepository.GetClientOpenBookingInfos(account.AccountId);

                return bookingInfoSet.Select(bookingInfo => new ClientBookingData()
                {
                    BookingId = bookingInfo.Booking.BookedId,
                    Developer = bookingInfo.Developer.Email + " " + bookingInfo.Developer.YearsOfExprience + " " + bookingInfo.Developer.Description,
                    HiredDate = bookingInfo.Booking.StartDate,
                    ReturnDate = bookingInfo.Booking.EndDate
                }).ToArray();

            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.AppUser)]
        public Hired GetHired(int hiredId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
               // var accountRepository = _dataRepositoryFactory.GetBusinessEngine<IAccountRepository>();
                var hiringRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();
                var hired = hiringRepository.Get(hiredId);

                if (hired == null)
                {
                    var ex = new NotFoundException($"No hiring record found for id '{hiredId}'");
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(hired);

                return hired;
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public ClientHiringData[] GetCurrentHires()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var hiringRepository = _dataRepositoryFactory.GetDataRepository<IHiringRepository>();
                var hiringInfoSet = hiringRepository.GetCurrentClientHiringInfo();

                return hiringInfoSet.Select(hiringInfo => new ClientHiringData()
                {
                    HiredId = hiringInfo.Hired.HiredId,
                    Developer = hiringInfo.Developer.Email + " " + hiringInfo.Developer.Link + " " + hiringInfo.Developer.YearsOfExprience + " " + hiringInfo.Developer.Description,
                    ClientName = hiringInfo.Client.FirstName + " " + hiringInfo.Client.LastName,
                    HiredDate = hiringInfo.Hired.StartDate,
                    ExpectedReturn = hiringInfo.Hired.DateDue
                }).ToArray();
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public Booking[] GetDeadBookings()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var bookingRepository = _dataRepositoryFactory.GetDataRepository<IBookingRepository>();
                var bookings = bookingRepository.GetBookingsByPickupDate(DateTime.Now.AddDays(-1));

                //return bookings != null ? bookings.ToArray() : null;
                return bookings?.ToArray();
            });
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Security.AppAdmin)]
        public bool IsDeveloperCurrentlyHired(int developerId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                var developerHiringEngine = _businessEngineFactory.GetBusinessEngine<IDeveloperHiringEngine>();

                return developerHiringEngine.IsDeveloperCurrentlyHired(developerId);
            });
        }
    }
}