using System;
using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{   
    public interface ICarService
    {
        IDataResult<CarDetailDto> GetById(int id);
        IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarsByColorIdAndBrandId(int colorId, int brandId);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);

    }
}
