using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.Abstract;
using Core.Utilities.Helpers.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        private IImageHelper _imageHelper;

        public CarImageManager(ICarImageDal carImageDal, IImageHelper imageHelper)
        {
            _carImageDal = carImageDal;
            _imageHelper = imageHelper;
        }

        [SecuredOperation("superadmin")]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfCarImagesCountMaxedOut(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            var uploadResult = _imageHelper.Upload(file);

            if (uploadResult.Success)
            {
                carImage.ImagePath = uploadResult.Data;
                _carImageDal.Add(carImage);
                return new SuccessResult(Messages.ImageUploaded);
            }
            return new ErrorResult(Messages.ImageUploadError);
        }

        [SecuredOperation("superadmin")]
        public IResult Delete(int carImageId)
        {
            var carImage = _carImageDal.Get(ci => ci.Id==carImageId);
            if (carImage!=null)
            {
                var deleteResult = _imageHelper.Delete(carImage.ImagePath);
                if (deleteResult.Success)
                {
                    _carImageDal.Delete(carImage);
                    return new SuccessResult(deleteResult.Message);
                }
            }
            return new ErrorResult("Dosya silinirken bir hata oluştu");
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();
            return new SuccessDataResult<List<CarImage>>(result,Messages.Listed);
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId);
            if (result.Count==0)
            {
                var defaultCarImageList = new List<CarImage>();
                var path = _imageHelper.GetDefaultCarImage();
                var defaultCarImage = new CarImage
                {
                    Id = 0,
                    CarId = carId,
                    ImagePath = path.Data
                };
                defaultCarImageList.Add(defaultCarImage);
                return new SuccessDataResult<List<CarImage>>(defaultCarImageList,Messages.Listed);
            }          
            return new SuccessDataResult<List<CarImage>>(result,Messages.Listed);
        }

        [SecuredOperation("superadmin")]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            var result = _imageHelper.Update(file, carImage.ImagePath);
            if (result.Success)
            {
                return new SuccessResult(result.Message);
            }
            return new ErrorResult(result.Message);
        }

        private IResult CheckIfCarImagesCountMaxedOut(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImagesCountMaxedOut);
            }
            return new SuccessResult();
        }
        

    }
}
