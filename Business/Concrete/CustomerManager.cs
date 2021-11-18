using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager:ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Added);
        }


        public IDataResult<List<Customer>> GetAll() => new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.Listed);

        public IDataResult<Customer> GetById(int customerId)
        {
            var customer = _customerDal.Get(b => b.Id == customerId);
            return new SuccessDataResult<Customer>(customer, Messages.Listed);
        }

        [SecuredOperation("superadmin")]
        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("superadmin")]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Updated);
        }
    }
}
