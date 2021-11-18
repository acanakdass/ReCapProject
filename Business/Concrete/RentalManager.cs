using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager:IRentalService
    {
        IRentalDal _rentalDal;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        [SecuredOperation("superadmin")]
        public IResult Add(Rental rental)
        {
            var businessResult = BusinessRules.Run(CheckIfCarAvailableBetweenSelectedDates(rental.CarId, rental.RentDate, rental.ReturnDate));
            if (businessResult.Success)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentOperationSucceed);
            }
            return new ErrorResult(businessResult.Message);
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            var rental = _rentalDal.Get(b => b.Id == rentalId);
            return new SuccessDataResult<Rental>(rental, "Kiralama listelendi");
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult("Kiralama silindi");
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult("Kiralama güncellendi");
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(),"Tüm kiralamalar Listelendi");
        }

        public IDataResult<List<RentalDetailDto>> GetAllAsDto()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), "Tüm kiralamalar Listelendi");
        }

        public IResult CheckIfCarAvailableBetweenSelectedDates(int carId, DateTime rentDate, DateTime returnDate)
        {
            var car = _carService.GetById(carId).Data;
            var currentCarsRentals = _rentalDal.GetRentalDetails(r => r.Car.Id == carId);
            foreach (var rental in currentCarsRentals)
            {
                if ((rentDate >= rental.RentDate && rentDate <= rental.ReturnDate) ||
                    (returnDate >= rental.RentDate && returnDate <= rental.ReturnDate) ||
                    (rental.RentDate >= rentDate && rental.RentDate <= returnDate) ||
                    (rental.ReturnDate >= rentDate && rental.ReturnDate <= returnDate))
                {
                    return new ErrorResult(Messages.carIsNotAvailableBetweenSelectedDates);
                }
            }
            return new SuccessResult();
        }
    }
}
