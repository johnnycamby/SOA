using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Engine;
using Business.Engine.Engine;
using DataLayer.Contracts;
using DataLayer.Repositories;

namespace Bootstrapper
{
    public static class MefLoader
    {
        public static CompositionContainer Init()
        {
            // MEF Catalog
            var catalog = new AggregateCatalog();

            // build a MEF Catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountRepository).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(DataRepositoryFactory).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(DeveloperHiringEngine).Assembly));
            var container = new CompositionContainer(catalog);

            return container;
        }
    }
}
