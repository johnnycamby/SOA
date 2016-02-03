using System;
using System.Collections.Generic;
using Business.Entities;
using DataLayer.Contracts.Contracts;
using DataLayer.Contracts.DTOs;
using System.Linq;
using System.ComponentModel.Composition;
using Core.Common.Extensions;

namespace DataLayer.Repositories
{
    [Export(typeof(IBookingRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BookingRepository : DataRepositoryBase<Booking>, IBookingRepository
    {
        protected override Booking AddEntity(XplicitDbContext dbctx, Booking entity)
        {
            return dbctx.BookingSet.Add(entity);
        }

        protected override Booking UpdateEntity(XplicitDbContext dbctx, Booking entity)
        {
            return (from booking in dbctx.BookingSet
                where booking.BookedId == entity.BookedId
                select booking).FirstOrDefault();
        }

        protected override IEnumerable<Booking> GetEntities(XplicitDbContext dbctx)
        {
            return from booking in dbctx.BookingSet
                select booking;
        }

        protected override Booking GetEntity(XplicitDbContext dbctx, int id)
        {
            var query = (from booking in dbctx.BookingSet
                where booking.BookedId == id
                select booking);
            var result = query.FirstOrDefault();

            return result;
        }

        public IEnumerable<Booking> GetBookingsByPickupDate(DateTime pickupDate)
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = (from booking in dbctx.BookingSet
                    where booking.EndDate < pickupDate
                    select booking);
                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ClientBookingInfo> GetCurrentClientBookingInfo()
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = from booking in dbctx.BookingSet
                    join clientAccount in dbctx.AccountSet on booking.AccountId equals clientAccount.AccountId
                    join developer in dbctx.DeveloperSet on booking.DeveloperId equals developer.DeveloperId

                    select new ClientBookingInfo()
                    {
                        Client = clientAccount,
                        Developer = developer,
                        Booking = booking
                    };

                return query.ToFullyLoaded();
            }
        }

        public IEnumerable<ClientBookingInfo> GetClientOpenBookingInfos(int accountId)
        {
            using (var dbctx = new XplicitDbContext())
            {
                var query = from booking in dbctx.BookingSet
                    join clientAccount in dbctx.AccountSet on booking.AccountId equals clientAccount.AccountId
                    join developer in dbctx.DeveloperSet on booking.DeveloperId equals developer.DeveloperId
                    where booking.AccountId == accountId

                    select new ClientBookingInfo()
                    {
                        Client = clientAccount,
                        Developer = developer,
                        Booking = booking
                    };

                return query.ToFullyLoaded();
            }
        }
    }
}