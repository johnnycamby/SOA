using Business.Entities;

namespace DataLayer.Contracts.DTOs
{
    public class ClientBookingInfo
    {
        public Account Client { get; set; }
        public Developer Developer { get; set; }
        public Booking Booking { get; set; }
    }
}