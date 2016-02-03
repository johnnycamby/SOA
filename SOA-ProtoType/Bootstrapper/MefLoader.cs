using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var container = new CompositionContainer(catalog);

            return container;
        }
    }
}
