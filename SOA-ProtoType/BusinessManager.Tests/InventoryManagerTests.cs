using System;
using System.Security.Principal;
using System.Threading;
using Business.Entities;
using Business.Managers.Managers;
using Core.Contracts;
using DataLayer.Contracts.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessManager.Tests
{
    [TestClass]
    public class InventoryManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var principal = new GenericPrincipal(new GenericIdentity("Johnny"), new string[] {"Administrators"} );
            Thread.CurrentPrincipal = principal;
        }
        [TestMethod]
        public void UpdateDeveloper_add_new()
        {
            var newDeveloper = new Developer();
            var addedDeveloper = new Developer() {DeveloperId = 1};

            var mockRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.GetDataRepository<IDeveloperRepository>().Add(newDeveloper))
                .Returns(addedDeveloper);

            var inventoryManager = new InventoryManager(mockRepositoryFactory.Object);
            var result = inventoryManager.UpdateDeveloper(newDeveloper);
            Assert.IsTrue(result == addedDeveloper);
        }

        [TestMethod]
        public void UpdateDeveloper_update_existing()
        {
            var existingDeveloper = new Developer() {DeveloperId = 1};
            var updatedDeveloper = new Developer() { DeveloperId = 1 };

            var mockRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.GetDataRepository<IDeveloperRepository>().Add(existingDeveloper))
                .Returns(updatedDeveloper);

            var inventoryManager = new InventoryManager(mockRepositoryFactory.Object);
            var result = inventoryManager.UpdateDeveloper(existingDeveloper);

            Assert.IsTrue(result != updatedDeveloper);

        }
    }
}
