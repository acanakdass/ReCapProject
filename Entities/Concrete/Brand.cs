using System;
using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
    public class Brand:EntityBase, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
