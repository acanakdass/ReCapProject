using System;
using System.Collections.Generic;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from r in context.Rentals
                             join car in context.Cars on r.CarId equals car.Id
                             join customer in context.Customers
                             on r.CustomerId equals customer.Id
                             join b in context.Brands
                             on car.BrandId equals b.Id
                             join u in context.Users
                             on customer.UserId equals u.Id
                             select new RentalDetailDto
                             {
                                 Id=r.Id,
                                 BrandName = b.Name,
                                 CustomerFullName = u.FirstName + " " + u.LastName,
                                 RentDate = r.RentDate,
                                 ReturnDate = (DateTime)r.ReturnDate,
                                 Car=car
                             };
                return result.ToList();
            }
        }
    }
}
