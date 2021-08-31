using System;
using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile file, CarImage carImage);
        IResult Delete(int carImageId);
        IResult Update(IFormFile file, CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetImagesByCarId(int carId);
    }
}
