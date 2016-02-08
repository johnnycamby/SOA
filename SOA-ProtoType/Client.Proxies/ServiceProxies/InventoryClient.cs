using System;
using System.ComponentModel.Composition;
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

        public Developer GetDeveloper(int developerId)
        {
            return Channel.GetDeveloper(developerId);
        }

        public Developer[] GetAllDevelopers()
        {
            return Channel.GetAllDevelopers();
        }

        public Developer UpdateDeveloper(Developer developer)
        {
            return Channel.UpdateDeveloper(developer);
        }

        public void DeleteDeveloper(int developerId)
        {
            Channel.DeleteDeveloper(developerId);
        }

        public Developer[] GetAvailableDevelopers(DateTime startDate, DateTime endDate)
        {
            return Channel.GetAvailableDevelopers(startDate, endDate);
        }

        public Task<Developer> UpdateDeveloperAsync(Developer developer)
        {
            return Channel.UpdateDeveloperAsync(developer);
        }

        public Task<Developer[]> GetAvailableDevelopersAsync(DateTime startDate, DateTime endDate)
        {
            return Channel.GetAvailableDevelopersAsync(startDate, endDate);
        }

        public Task DeleteDeveloperAsync(int developerId)
        {
            return Channel.DeleteDeveloperAsync(developerId);
        }

        public Task<Developer[]> GetAllDevelopersAsync()
        {
            return Channel.GetAllDevelopersAsync();
        }

        public Task<Developer> GetDeveloperAsync(int developerId)
        {
            return Channel.GetDeveloperAsync(developerId);
        }
    }
}
