using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using Bootstrapper;
using Business.Entities;
using Core.Common.Core;
using Core.Contracts;
using DataLayer.Contracts.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataLayer.Test
{
    [TestClass]
    public class DataLayerTests
    {

        private readonly List<Developer> _developers = new List<Developer>()
        {
            new Developer() {DeveloperId = 1, Description = "Java Developer"},
            new Developer() {DeveloperId = 1, Description = "C# Developer"}
        };

        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MefLoader.Init();
        }
        
        [TestMethod]
        public void test_repository_usage()
        {
            var repoTest = new RepositoryTestClass();
            var developers = repoTest.GetDevelopers();
            Assert.IsTrue(developers != null);
        }

        [TestMethod]
        public void test_repository_factory_usage()
        {
            var factoryTest = new RepositoryFactoryTestClass();
            var developers = factoryTest.GetDevelopers();
            Assert.IsTrue(developers != null);
        }

        [TestMethod]
        public void test_repository_factory_mock1()
        {
            var mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IDeveloperRepository>().Get()).Returns(_developers);

            var factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);
            var result = factoryTest.GetDevelopers();

            Assert.IsTrue(result == _developers);
        }

        [TestMethod]
        public void test_repository_factory_mock2()
        {
            var mockDevRepo = new Mock<IDeveloperRepository>();
            mockDevRepo.Setup(obj => obj.Get()).Returns(_developers);

            var mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IDeveloperRepository>()).Returns(mockDevRepo.Object);

            var factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);
            var result = factoryTest.GetDevelopers();

            Assert.IsTrue(result == _developers);
        }

        [TestMethod]
        public void test_repository_mocking()
        {
            var mockDeveloperRepository = new Mock<IDeveloperRepository>();
            mockDeveloperRepository.Setup(obj => obj.Get()).Returns(_developers);

            var repositoryTest = new RepositoryTestClass(mockDeveloperRepository.Object);
            var developers = repositoryTest.GetDevelopers();

            Assert.IsTrue(developers != null);
        }

       

    }

    public class RepositoryTestClass
    {

        [Import]
        private IDeveloperRepository _developerRepository;

        // used to tell MEF to resolve any dependencies
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public IEnumerable<Developer> GetDevelopers()
        {
            var developers = _developerRepository.Get();
            return developers;
        }
    }

    public class RepositoryFactoryTestClass
    {
        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }
        public RepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;
        
        public IEnumerable<Developer> GetDevelopers()
        {
            var developerRepo = _dataRepositoryFactory.GetDataRepository<IDeveloperRepository>();
            var developers = developerRepo.Get();
             return developers;
        }

    }
}
