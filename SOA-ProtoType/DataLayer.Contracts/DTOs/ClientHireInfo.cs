using Business.Entities;

namespace DataLayer.Contracts.DTOs
{
    public class ClientHireInfo
    {
        public Account Client { get; set; }
        public Developer Developer { get; set; }
        public Hired Hired { get; set; }
    }
}