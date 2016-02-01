using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Core.Common.Extensions;
using Core.Common.Utils;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Common.Core
{
    public class ObjectBase : INotifyPropertyChanged, IDataErrorInfo
    {

        public ObjectBase()
        {
            _Validator = GetValidator();
            Validate();
        }

        

        private event PropertyChangedEventHandler _propertyChanged;
        protected IValidator _Validator = null;
        protected IEnumerable<ValidationFailure> _ValidationErrors = null;
         

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


        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName, true);
        }

        // tell if to change dirty-state of object
        protected virtual void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            //if(PropertyChanged != null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (makeDirty)
                IsDirty = true;

            Validate();
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }

        public void Validate()
        {
            if (_Validator != null)
            {
                var results = _Validator.Validate(this);
                _ValidationErrors = results.Errors;
            }
        }

        protected IValidator GetValidator()
        {
            return null;
        }

        protected bool _isDirty = false;


        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return _ValidationErrors; }
            set { }
        }

       // [NotNavigable]
        public virtual bool IsValid => _ValidationErrors == null || !_ValidationErrors.Any();

        string IDataErrorInfo.Error => string.Empty;

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                var errors = new StringBuilder();

                if (_ValidationErrors != null && _ValidationErrors.Count() > 0)
                {
                    foreach (var validationError in _ValidationErrors)
                    {
                        if (validationError.PropertyName == columnName)
                            errors.AppendLine(validationError.ErrorMessage);
                    }
                }

                return errors.ToString();
            }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public List<ObjectBase> GetDirtyObject()
        {
            var dirtyObjects = new List<ObjectBase>();

            WalkObjectGraph(o =>
            {
                if (o.IsDirty)
                    dirtyObjects.Add(o);
                return false;

            }, colln => { });

            return dirtyObjects;
        }

        public void CleanAll()
        {
            WalkObjectGraph(o =>
            {
                if (o.IsDirty)
                    o.IsDirty = false;
                return false;
            }, colln => { });
        }

        public virtual bool IsAnyThingDirty()
        {
            var isDirty = false;

            WalkObjectGraph(o =>
            {
                if (o.IsDirty)
                {
                    isDirty = true;
                    return true;
                }
                else
                    return false;
            }, colln => { });

            return isDirty;
        }

        protected void WalkObjectGraph(Func<ObjectBase, bool> snippetForObject, Action<IList> snippetForCollection, params string[] exemptProperties)
        {
            var visited = new List<ObjectBase>();

            // deal with recursiveness
            Action<ObjectBase> walk = null;
            var exemptions = new List<string>();

            if (exemptProperties != null)
                exemptions = exemptProperties.ToList();


            walk = (o) =>
            {
                if (o != null && !visited.Contains(o))
                {
                    visited.Add(o);
                    var exitWalk = snippetForObject.Invoke(o);

                    if (!exitWalk)
                    {
                        var properties = o.GetBrowsableProperties();

                        foreach (PropertyInfo property in properties)
                        {
                            if (!exemptions.Contains(property.Name))
                            {
                                if (property.PropertyType.IsSubclassOf(typeof(ObjectBase)))
                                {
                                    var obj = (ObjectBase)(property.GetValue(o, null));
                                    walk(obj);
                                }
                                else
                                {
                                    var colln = property.GetValue(o, null) as IList;

                                    if (colln != null)
                                    {
                                        snippetForCollection.Invoke(colln);

                                        foreach (var item in colln)
                                        {
                                            if (item is ObjectBase)
                                                walk((ObjectBase)item);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            walk(this);

        }
    }
}
