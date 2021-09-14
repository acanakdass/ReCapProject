﻿using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {

            var cars = _carDal.GetAll(c => c.BrandId == brandId);
            return new SuccessDataResult<List<Car>>(cars, Messages.ProductsListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            var cars =  _carDal.GetAll(c => c.ColorId == colorId);
            return new SuccessDataResult<List<Car>>(cars, Messages.ProductsListed);

        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var cars = _carDal.GetCarDetails();
            return new SuccessDataResult<List<CarDetailDto>>(cars, Messages.ProductsListed);
        }

        public IResult Add(Car car)
        {
            if (car.Description.Length >= 2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.ProductAdded);

            }

            return new ErrorResult("Ürün ismi 1 karakterden büyük ve günlük fiyat 0'dan büyük olmalıdır.");
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.ProductDeleted);

        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IDataResult<Car> GetById(int id)
        {
            var car = _carDal.Get(c => c.Id == id);
            return new SuccessDataResult<Car>(car,Messages.ProductsListed);

        }
    }
}
