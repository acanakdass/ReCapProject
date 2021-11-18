using System;
using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Rental: EntityBase, IEntity
    {
        public int Id { get; set; } 
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set;}
        public DateTime ReturnDate { get; set; }
        public bool? IsDelivered { get; set; }
    }
}
