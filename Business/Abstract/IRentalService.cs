using System;
using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<Rental> GetById(int id);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<RentalDetailDto>> GetAllAsDto();
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);
    }
}
    