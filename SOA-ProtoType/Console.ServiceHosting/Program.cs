using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Transactions;
using Bootstrapper;
using Business.Managers.Managers;
using Core.Common.Core;
using Timer = System.Timers.Timer;


namespace ServiceHosting
{
    class Program
    {
        static void Main(string[] args)
        {

            var principal = new GenericPrincipal( new GenericIdentity("XPLICIT/johnny"), new string[] {"Administrators"} );
            Thread.CurrentPrincipal = principal;

            ObjectBase.Container = MefLoader.Init();

            Console.WriteLine("Starting up services...");

            Console.WriteLine("");

            var hostAccountManager = new ServiceHost(typeof(AccountManager));
            var hostInventoryManager = new ServiceHost(typeof(InventoryManager));
            var hostHiringManager = new ServiceHost(typeof(HiringManager));

            StartService(hostAccountManager, "AccountManager");
            StartService(hostInventoryManager, "InventoryManager");
            StartService(hostHiringManager, "HiringManager");

            var timer = new Timer(10000);
            timer.Elapsed += TimerOnElapsed;
            timer.Start();

            Console.WriteLine("Booking monitor started...");
            
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();

            timer.Stop();
            Console.WriteLine("Booking monitor stopped...");

            StopService(hostAccountManager, "AccountManager");
            StopService(hostInventoryManager, "InventoryManager");
            StopService(hostHiringManager, "HiringManager");
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {

            Console.WriteLine($"Looking for dead bookings at {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");

            var hiringManager = new HiringManager();
            var bookings = hiringManager.GetDeadBookings();

            if (bookings != null)
            {
                foreach (var booking in bookings)
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            hiringManager.CancelBooking(booking.BookedId);
                            Console.WriteLine($"Cancelling bookings {booking.BookedId}");
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine($"There was an exception when attempting to cancel booking '{booking.BookedId}'");
                        }
                    }
                }
            }
        }

        static void StartService(ServiceHost host, string description)
        {
            host.Open();
            Console.WriteLine("Service {0} started.", description);

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine("Listening on endpoint:");
                Console.WriteLine($"Address: {endpoint.Address.Uri}");
                Console.WriteLine($"Binding: {endpoint.Binding.Name}");
                Console.WriteLine($"Contract: {endpoint.Contract.Name}");

            }

            Console.WriteLine();
        }

        static void StopService(ServiceHost host, string description)
        {
            host.Close();
            Console.WriteLine($"Service {description} stopped.");
        }
    }
}
