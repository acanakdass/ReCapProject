using System;
using System.Collections.Generic;
using Business.Abstract;
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

        public IResult Delete(CarImage carImage)
        {
            throw new NotImplementedException();
        }

        public IDataResult<CarImage> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();
            return new SuccessDataResult<List<CarImage>>(result,Messages.Listed);
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(result,Messages.Listed);
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            throw new NotImplementedException();
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
