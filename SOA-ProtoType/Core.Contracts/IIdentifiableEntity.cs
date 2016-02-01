namespace Core.Contracts
{
    /// <summary>
    /// - l'l late DataAccess Layer know which one the propertie in an Entity class is the 'identifier' for the Id in the Database
    ///   This could have been done using EntityFramework schame Key attribute or setting up a property in the DBContext. But no need for the DataAccess Layer repositories
    ///   to use Reflection to figure out which is the 'Id'  
    /// </summary>
    public interface IIdentifiableEntity
    {
         int EntityId { get; set; }
    }
}