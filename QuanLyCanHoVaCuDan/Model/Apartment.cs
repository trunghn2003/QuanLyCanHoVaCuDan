using System.ComponentModel.DataAnnotations;

namespace QuanLyCuDan.Model
{
    public class Apartment
    {
        [Key]
        public int ApartmentID { get; set; }
        public string UnitNumber { get; set; }
        public int Floor { get; set; }
        public double Size { get; set; }

        public ICollection<CitizenApartment> ? CitizenApartments { get; set; }
    }
}
