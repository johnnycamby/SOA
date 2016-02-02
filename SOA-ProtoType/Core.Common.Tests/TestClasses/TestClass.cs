using System;
using Core.Common.Core;
using FluentValidation;

namespace Core.Common.Tests.TestClasses
{
    public class TestClass : ObjectBase
    {
         private string _cleanProp = string.Empty;
        private string _dirtyProp = string.Empty;
        private string _stringProp = string.Empty;
        TestChild _child = new TestChild();
        TestChild _notNavigableChild = new TestChild();

        public string CleanProp
        {
            get { return _cleanProp; }
            set
            {
                if(_cleanProp == value)
                    return;
                _cleanProp = value;
                OnPropertyChanged(() => CleanProp, false);
            }
        }

        public string DirtyProp
        {
            get { return _dirtyProp; }
            set
            {
                if (_dirtyProp == value)
                    return;

                _dirtyProp = value;
                OnPropertyChanged(() => DirtyProp);
            }
        }

        public string StringProp
        {
            get { return _stringProp; }
            set
            {
                if (_stringProp == value)
                    return;

                _stringProp = value;
                OnPropertyChanged("StringProp", false);
            }
        }

        public TestChild Child => _child;

        [NotNavigable]
        public TestChild NotNavigableChild => _notNavigableChild;

        class TestClassValidator : AbstractValidator<TestClass>
        {
            public TestClassValidator()
            {
                RuleFor(obj => obj.StringProp).NotEmpty();
            }
        }

        protected  IValidator GetValidator()
        {
            return new TestClassValidator();
        }
    }
}