using System;
using System.Collections.Generic;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetCarsByBrandId(int brandId);
        List<Car> GetCarsByColorId(int colorId);
    }
}
