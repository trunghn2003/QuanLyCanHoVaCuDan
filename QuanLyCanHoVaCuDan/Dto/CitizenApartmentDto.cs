﻿namespace QuanLyCanHoVaCuDan.Dto
{
    public class CitizenApartmentDto
    {
        //public int Id { get; set; }
        public int CitizenId { get; set; }
        public int ApartmentID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
