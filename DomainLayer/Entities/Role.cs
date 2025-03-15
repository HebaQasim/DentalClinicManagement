using System.Numerics;

namespace DentalClinicManagement.DomainLayer.Entities
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = [];
        public Admin Admin { get; set; }
        public ICollection<CustomerService> CustomerServices { get; set; } = [];


    }
}
