using System;
using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class CarImage: EntityBase, IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
