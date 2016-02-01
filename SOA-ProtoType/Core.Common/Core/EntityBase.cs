using System.Runtime.Serialization;

namespace Core.Common.Core
{
    /// <summary>
    /// - Use to serve as a base class for all Business-side entities
    /// - Implement 'IExtensibleDataObject' making any classes (Entities) that implement it become version-torelent 
    ///   thus if they receive data they cann't accomodate they l'l not just explode and break contract, instead they l'l just access that data
    ///   and store it temporalily in the 'ExtensionData' property incase it's be passed over some later.
    /// </summary>
    [DataContract]
    public abstract class EntityBase : IExtensibleDataObject 
    {
        public ExtensionDataObject ExtensionData { get; set; }
    }
}