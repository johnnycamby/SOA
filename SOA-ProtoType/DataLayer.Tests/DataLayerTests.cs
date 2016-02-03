using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrapper;
using Business.Entities;
using Core.Common.Core;
using DataLayer.Contracts.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayer.Tests
{
    [TestClass]
    public class DataLayerTests
    {
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
}
