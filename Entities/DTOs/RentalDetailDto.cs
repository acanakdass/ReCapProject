using System;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Car Car { get; set; }
    }
}
