using System;
using System.Runtime.Serialization;
using Core.Common.Core;

namespace Clients.Entities
{
    public class Hired : ObjectBase
    {
        private int _hiredId;
        private int _accountId;
        private int _developerId;
        private DateTime _startDate;
        private DateTime? _endDate;
        private DateTime _dateDue;

        public int HiredId
        {
            get { return _hiredId; }
            set
            {
                if (_hiredId != value)
                {
                    _hiredId = value;
                    OnPropertyChanged(() => HiredId);
                }
            }
            
        }

        public int AccountId
        {
            get { return _accountId; }
            set {
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

        public DateTime? EndDate
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

        public DateTime DateDue
        {
            get { return _dateDue; }
            set
            {
                if (_dateDue != value)
                {
                    _dateDue = value;
                    OnPropertyChanged(() => DateDue);
                }
            }
        }
    }
}