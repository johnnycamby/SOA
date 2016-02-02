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
using Core.Contracts;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Common.Core
{
    public class ObjectBase : NotificationObject, IDataErrorInfo, IDirtyCapable 
    {

        public ObjectBase()
        {
            _Validator = GetValidator();
            Validate();
        }

        protected bool _isDirty = false;
        protected IValidator _Validator = null;
        protected IEnumerable<ValidationFailure> _ValidationErrors = null;

#region ===============================  Property change notification ==================================

        protected override void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName, true);
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression, bool makeDirty)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName, makeDirty);
        }

        // tell if to change dirty-state of object
        protected virtual void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            base.OnPropertyChanged(propertyName);

            if (makeDirty)
                IsDirty = true;

            Validate();
        }
        #endregion  =====================================================================

#region  ============================= Validation =======================================

       
        protected IValidator GetValidator()
        {
            return null;
        }
        
        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return _ValidationErrors; }
            set { }
        }

        public void Validate()
        {
            if (_Validator != null)
            {
                var results = _Validator.Validate(this);
                _ValidationErrors = results.Errors;
            }
        }


        [NotNavigable]
        public virtual bool IsValid => _ValidationErrors == null || !_ValidationErrors.Any();

        #endregion ============================================================================


#region ==================================== IDataErrorInfo members ===========================

        string IDataErrorInfo.Error => string.Empty;

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                var errors = new StringBuilder();

                if (_ValidationErrors != null && _ValidationErrors.Any())
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

        #endregion ====================================================================================

        #region ================================ IDirtyCapable members ==============================
        public virtual bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public List<IDirtyCapable> GetDirtyObjects()
        {
            var dirtyObjects = new List<IDirtyCapable>();

            WalkObjectGraph(o =>
            {
                if (o.IsDirty)
                    dirtyObjects.Add(o);
                return false;

            }, colln => { });

            return dirtyObjects;
        }

        public virtual void CleanAll()
        {
            WalkObjectGraph(o =>
            {
                if (o.IsDirty)
                    o.IsDirty = false;
                return false;
            }, colln => { });
        }

        public virtual bool IsAnythingDirty()
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

        #endregion =====================================================================================

        #region =================================== Protected methods ==================================

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

                        foreach (var property in properties)
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

#endregion ============================================================================================

    }
}
