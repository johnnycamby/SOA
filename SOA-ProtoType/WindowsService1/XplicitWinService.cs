using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Business.Managers.Managers;

namespace WindowsService1
{
    public partial class XplicitWinService : ServiceBase
    {
        public XplicitWinService()
        {
            InitializeComponent();
            _host = new ServiceHost(typeof(InventoryManager));
        }

        private readonly ServiceHost _host;

        protected override void OnStart(string[] args)
        {
           _host.Open();
        }

        protected override void OnStop()
        {
            _host.Close();
        }
    }
}
