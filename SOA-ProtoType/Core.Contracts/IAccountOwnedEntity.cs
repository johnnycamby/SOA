namespace Core.Contracts
{
    /// <summary>
    /// - l'l identify account ownership. Mainly used in adding security to an application
    /// </summary>
    public interface IAccountOwnedEntity
    {
        int OwnerAccountId { get; } 
    }
}