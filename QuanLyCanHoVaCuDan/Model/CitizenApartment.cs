using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCuDan.Model
{
    public class CitizenApartment
    {
        public int Id { get; set; } 
        public int CitizenId { get; set; }

        public int ApartmentID { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Citizen Citizen { get; set; }
        public virtual Apartment Apartment { get; set; }
    }
}
