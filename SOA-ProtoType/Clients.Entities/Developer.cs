using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using FluentValidation;

namespace Clients.Entities
{
    public class Developer : ObjectBase
    {
        private int _developerId;
        private string _email;
        private string _link;
        private string _description;
        private int _yearsOfExprience;
        private decimal _monthlySalary;
        private bool _isCurrentlyHired;


        public int DeveloperId
        {
            get { return _developerId; }
            set
            {
                if (_developerId != value)
                {
                    _developerId = value;
                    //OnPropertyChanged(nameof(DeveloperId));
                    OnPropertyChanged(() => DeveloperId);
                }

            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }

        public string Link
        {
            get { return _link; }
            set
            {
                if (_link != value)
                {
                    _link = value;
                    OnPropertyChanged(() => Link);
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public int YearsOfExprience
        {
            get { return _yearsOfExprience; }
            set
            {
                if (_yearsOfExprience != value)
                {
                    _yearsOfExprience = value;
                    OnPropertyChanged(() => YearsOfExprience);
                }
            }
        }

        public decimal MonthlySalary
        {
            get { return _monthlySalary; }
            set
            {
                if (_monthlySalary != value)
                {
                    _monthlySalary = value;
                    OnPropertyChanged(() => MonthlySalary);
                }
            }
        }

        public bool IsCurrentlyHired
        {
            get { return _isCurrentlyHired; }
            set
            {
                if (_isCurrentlyHired != value)
                {
                    _isCurrentlyHired = value;
                    OnPropertyChanged(() => IsCurrentlyHired);
                }
            }
        }
    }

    class DeveloperValidator: AbstractValidator<Developer>
    {
        public DeveloperValidator()
        {
            RuleFor(obj => obj.Description).NotEmpty();
            RuleFor(obj => obj.Email).NotEmpty();
            RuleFor(obj => obj.Link).NotEmpty();
            RuleFor(obj => obj.MonthlySalary).GreaterThan(0);
            RuleFor(obj => obj.YearsOfExprience).GreaterThan(1000).LessThanOrEqualTo(DateTime.Now.Year);
        }

        protected IValidator GetValidator()
        {
            return new DeveloperValidator();
        }
    }
}
