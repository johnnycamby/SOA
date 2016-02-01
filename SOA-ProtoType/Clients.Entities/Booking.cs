using System;
using Core.Common.Core;

namespace Clients.Entities
{
    public class Booking: ObjectBase
    {

        private int _bookedId;
        private int _accountId;
        private int _developerId;
        private DateTime _startDate;
        private DateTime _endDate;

        public int BookedId
        {
            get { return _bookedId; }
            set
            {
                if (_bookedId != value)
                {
                    _bookedId = value;
                    OnPropertyChanged(() => BookedId);
                }
            }
        }
        public int AccountId
        {
            get { return _accountId; }
            set
            {
                if (_accountId != value)
                {
                    _accountId = value;
                    OnPropertyChanged(() => AccountId);
                }
            }
        }

        public int DeveloperId
        {
            get { return _developerId; }
            set
            {
                if (_developerId != value)
                {
                    _developerId = value;
                    OnPropertyChanged(() => DeveloperId);
                }
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(() => StartDate);
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(() => EndDate);
                }
            }
        }

    }
}