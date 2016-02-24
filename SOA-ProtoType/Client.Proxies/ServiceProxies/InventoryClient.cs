using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Client.Contracts.ServiceContracts;
using Clients.Entities;
using Core.Common.ServiceModel;

namespace Client.Proxies.ServiceProxies
{
    [Export(typeof(IInventoryService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class InventoryClient : UserClientBase<IInventoryService>, IInventoryService
    {

        public Developer GetDeveloper(Guid developerId)
        {
            return Channel.GetDeveloper(developerId);
        }

        public Developer[] GetAllDevelopers()
        {
            return Channel.GetAllDevelopers();
        }

        public IQueryable<Developer> GetAllDevelopers(string ownerId)
        {
            return Channel.GetAllDevelopers(ownerId);
        }


        public Developer UpdateDeveloper(Developer developer)
        {
            return Channel.UpdateDeveloper(developer);
        }

        public void DeleteDeveloper(Guid developerId)
        {
            Channel.DeleteDeveloper(developerId);
        }

        public void DeleteDeveloper(string ownerId,Guid developerId)
        {
             Channel.DeleteDeveloper(ownerId, developerId) ;
        }

        public Developer[] GetAvailableDevelopers(DateTime startDate, DateTime endDate)
        {
            return Channel.GetAvailableDevelopers(startDate, endDate);
        }

        public Developer AddDeveloper(Developer developer, string ownerId)
        {
            return Channel.AddDeveloper(developer, ownerId);
        }

        public Task<Developer> UpdateDeveloperAsync(Developer developer)
        {
            return Channel.UpdateDeveloperAsync(developer);
        }

        public Task<Developer[]> GetAvailableDevelopersAsync(DateTime startDate, DateTime endDate)
        {
            return Channel.GetAvailableDevelopersAsync(startDate, endDate);
        }

        public Task DeleteDeveloperAsync(Guid developerId)
        {
            return Channel.DeleteDeveloperAsync(developerId);
        }

        public Task<Developer[]> GetAllDevelopersAsync()
        {
            return Channel.GetAllDevelopersAsync();
        }

        public Task<Developer> GetDeveloperAsync(Guid developerId)
        {
            return Channel.GetDeveloperAsync(developerId);
        }

        public Task DeleteDeveloperAsync(string ownerId, Guid developerId )
        {
           return  Channel.DeleteDeveloperAsync(ownerId, developerId);
        }

        public Task<Developer> AddDeveloperAsync(Developer developer, string ownerId)
        {
            return Channel.AddDeveloperAsync(developer, ownerId);
        }

        public Task<IQueryable<Developer>> GetAllDevelopersAsync(string ownerId)
        {
            return Channel.GetAllDevelopersAsync(ownerId);
        }
    }
}
