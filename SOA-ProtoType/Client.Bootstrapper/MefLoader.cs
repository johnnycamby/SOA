using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Proxies.ServiceProxies;

namespace Client.Bootstrapper
{
    public static class MefLoader
    {

        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogCollection)
        {
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(InventoryClient).Assembly));

            if(catalogCollection != null)
                foreach(var part in catalogCollection)
                    catalog.Catalogs.Add(part);

            var container = new CompositionContainer(catalog);

            return container;

        }

    }
}
