using System;
using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Customer: EntityBase, IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
    }
}
