using System;
using Client.Bootstrapper;
using Client.Contracts.ServiceContracts;
using Client.Proxies;
using Client.Proxies.ServiceProxies;
using Core.Common.Core;
using Core.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientProxies.Tests
{
    [TestClass]
    public class ProxyObtainmentTests
    {

        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MefLoader.Init();
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            var proxy = ObjectBase.Container.GetExportedValue<IInventoryService>();

            Assert.IsTrue(proxy is InventoryClient);
        }

        [TestMethod]
        public void obtain_proxy_from_service_factory()
        {
            var factory = new ServiceFactory();
            var proxy = factory.CreateClient<IInventoryService>();

            Assert.IsTrue(proxy is InventoryClient);
        }

        [TestMethod]
        public void obtain_service_factory_and_proxy_from_container()
        {
            var factory = ObjectBase.Container.GetExportedValue<IServiceFactory>();

            var proxy = factory.CreateClient<IInventoryService>();

            Assert.IsTrue(proxy is InventoryClient);
        }
    }
}
