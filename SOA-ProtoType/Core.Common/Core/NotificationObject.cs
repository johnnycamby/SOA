using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Core.Common.Annotations;
using Core.Common.Utils;

namespace Core.Common.Core
{
    public class NotificationObject: INotifyPropertyChanged
    {


        private event PropertyChangedEventHandler _propertyChanged;
        readonly List<PropertyChangedEventHandler> _propertyChangedSubscribers = new List<PropertyChangedEventHandler>();

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (!_propertyChangedSubscribers.Contains(value))
                {
                    _propertyChanged += value;
                    _propertyChangedSubscribers.Add(value);
                }
            }
            remove
            {
                _propertyChanged -= value;
                _propertyChangedSubscribers.Remove(value);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }
    }
}