using System;
using System.ServiceModel;
using Business.Contracts.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceHosting.Tests
{
    [TestClass]
    public class ServiceAccessTests
    {

        [TestMethod]
        public void test_account_manager_as_service()
        {
            var factory = new ChannelFactory<IAccountService>("");
            var proxy = factory.CreateChannel();

            (proxy as ICommunicationObject).Open();
            factory.Close();
        }

        [TestMethod]
        public void test_inventory_manager_as_service()
        {
            var factory = new ChannelFactory<IInventoryService>("");
            var proxy = factory.CreateChannel();

            (proxy as ICommunicationObject).Open();
            factory.Close();
        }

        [TestMethod]
        public void test_hiring_manager_as_service()
        {
            var factory = new ChannelFactory<IHiringService>("");
            var proxy = factory.CreateChannel();

            (proxy as ICommunicationObject).Open();
            factory.Close();
        }
    }
}
