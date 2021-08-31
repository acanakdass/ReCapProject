using System;
using System.Collections.Generic;
using Business.Abstract;
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
        ICarDal _carDal;

        public RentalManager(IRentalDal rentalDal, ICarDal carDal)
        {
            _rentalDal = rentalDal;
            _carDal = carDal;
        }

        public IResult Add(Rental rental)
        {
            var car = _carDal.Get(c => c.Id == rental.CarId);
            var rentals = _rentalDal.GetRentalDetails();
            foreach (var item in rentals)
            {
                if(item.Car.Id==rental.CarId && item.ReturnDate != null)
                {
                    return new ErrorResult("Araç uygun değil");
                }
                return new SuccessResult("Aracı kiraladınız");
            }
            _rentalDal.Add(rental);
            return new SuccessResult("Marka Eklendi");
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            var rental = _rentalDal.Get(b => b.Id == rentalId);
            return new SuccessDataResult<Rental>(rental, "Marka listelendi");
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult("Marka silindi");
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult("Marka güncellendi");
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(),"Tüm kiralamalar Listelendi");

        }

        public IDataResult<List<RentalDetailDto>> GetAllAsDto()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), "Tüm kiralamalar Listelendi");
        }
    }
}
