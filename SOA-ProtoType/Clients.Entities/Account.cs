using Core.Common.Core;

namespace Clients.Entities
{
    public class Account: ObjectBase
    {

        int _accountId;
        string _loginEmail;
        string _firstName;
        string _lastName;
        string _address;
        string _city;
        string _state;
        string _zipCode;
        string _creditCard;
        string _expDate;

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

        public string LoginEmail
        {
            get { return _loginEmail; }
            set
            {
                if (_loginEmail != value)
                {
                    _loginEmail = value;
                    OnPropertyChanged(() => LoginEmail);
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(() => LastName);
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(() => Address);
                }
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(() => City);
                }
            }
        }

        public string State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged(() => State);
                }
            }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                if (_zipCode != value)
                {
                    _zipCode = value;
                    OnPropertyChanged(() => ZipCode);
                }
            }
        }

        public string CreditCard
        {
            get { return _creditCard; }
            set
            {
                if (_creditCard != value)
                {
                    _creditCard = value;
                    OnPropertyChanged(() => CreditCard);
                }
            }
        }

        public string ExpDate
        {
            get { return _expDate; }
            set
            {
                if (_expDate != value)
                {
                    _expDate = value;
                    OnPropertyChanged(() => ExpDate);
                }
            }
        }
    }
}