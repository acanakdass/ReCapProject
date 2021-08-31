using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Helpers.Abstract;
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
      IBrandService _brandService;
      IColorService _colorService;
        IImageHelper _imageHelper;

        public CarManager(ICarDal carDal, IBrandService brandService, IColorService colorService, IImageHelper imageHelper)
        {
            _carDal = carDal;
            _brandService = brandService;
            _colorService = colorService;
            _imageHelper = imageHelper;
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId)
      {
         var brandName = _brandService.GetById(brandId).Data.Name;
            if (brandName != null)
            {
         var cars = _carDal.GetCarDetails(c => c.BrandName==brandName);
         return new SuccessDataResult<List<CarDetailDto>>(cars, Messages.Listed);
            }
            return new ErrorDataResult<List<CarDetailDto>>(Messages.NotFound);
      }


      //[CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId)
        {
            var colorName = _colorService.GetById(colorId).Data.Name;
            var cars = _carDal.GetCarDetails(c => c.ColorName == colorName);
            return new SuccessDataResult<List<CarDetailDto>>(cars, Messages.Listed);
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorIdAndBrandId(int colorId,int brandId)
        {
            
            var colorData = _colorService.GetById(colorId).Data;
            var brandData = _brandService.GetById(brandId).Data;
            if (colorData!=null && brandData!=null)
            {
                var cars = _carDal.GetCarDetails(c => c.ColorName == colorData.Name && c.BrandName==brandData.Name);
                return new SuccessDataResult<List<CarDetailDto>>(cars, Messages.Listed);
            }
            return new ErrorDataResult<List<CarDetailDto>>(Messages.NotFound);

        }
        //[CacheAspect]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
      {
         var cars = _carDal.GetCarDetails();
            //foreach (var car in cars)
            //{
            //    if (car.CarImagePaths.Count <= 0)
            //    {
            //        var defaultCarImageList = new List<CarImage>();
            //        var path = _imageHelper.GetDefaultCarImage();
            //        var defaultCarImage = new CarImage
            //        {
            //            Id = 0,
            //            CarId = car.Id,
            //            ImagePath = path.Data
            //        };
            //        defaultCarImageList.Add(defaultCarImage);
            //    }
            //}
         return new SuccessDataResult<List<CarDetailDto>>(cars, Messages.Listed);
      }

      //[CacheRemoveAspect("ICarService.Get")]
      //[SecuredOperation("product.add,admin")]
      [ValidationAspect(typeof(CarValidator))]
      public IResult Add(Car car)
      {
         //ValidationTool.Validate(new CarValidator(), car);

         _carDal.Add(car);
         return new SuccessResult(Messages.Added);
         //return new ErrorResult("Ürün ismi 1 karakterden büyük ve günlük fiyat 0'dan büyük olmalıdır.");
      }

      //[CacheRemoveAspect("ICarService.Get")]
      public IResult Delete(Car car)
      {
         _carDal.Delete(car);
         return new SuccessResult(Messages.Deleted);

      }

      //[CacheRemoveAspect("ICarService.Get")]
      public IResult Update(Car car)
      {
         _carDal.Update(car);
         return new SuccessResult(Messages.Updated);
      }

      public IDataResult<CarDetailDto> GetById(int id)
      {
         var car = _carDal.GetCarDetailById(c => c.Id == id);
         return new SuccessDataResult<CarDetailDto>(car, Messages.Listed);
      }
    }
}
