using System.ServiceModel;
using Client.Contracts.ServiceContracts;
using Client.Proxies.ServiceProxies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientProxies.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {

        [TestMethod]
        public void test_inventory_client_connection()
        {
            var proxy = new InventoryClient();
            proxy.Open();
        }

        [TestMethod]
        public void test_hiring_client_connection()
        {
            var proxy = new HiringClient();
            proxy.Open();
        }

        [TestMethod]
        public void test_account_client_connection()
        {
            var proxy = new AccountClient();
            proxy.Open();
        }


    }
}
