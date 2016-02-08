using System;
using Business.Engine.Engine;
using Business.Entities;
using Core.Contracts;
using DataLayer.Contracts.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Business.Tests
{
    [TestClass]
    public class DeveloperHiringTests
    {
        [TestMethod]
        public void IsDeveloperCurrentlyHired_any_account()
        {
            var hired = new Hired()
            {
                DeveloperId = 1
            };

            var mockHiringRepository = new Mock<IHiringRepository>();
            mockHiringRepository.Setup(obj => obj.GetCurrentHiringByDeveloper(1)).Returns(hired);

            var mockRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockRepositoryFactory.Setup(obj => obj.GetDataRepository<IHiringRepository>())
                .Returns(mockHiringRepository.Object);

            var developerHiringEngine = new DeveloperHiringEngine(mockRepositoryFactory.Object);

            var try1 = developerHiringEngine.IsDeveloperCurrentlyHired(2);
            var try2 = developerHiringEngine.IsDeveloperCurrentlyHired(1);

            Assert.IsFalse(try1);
            Assert.IsTrue(try2);


        }
    }
}
