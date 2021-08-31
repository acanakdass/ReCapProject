using System;
using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IDataResult<List<User>> GetAll()
        {
            var users =_userDal.GetAll();
            return new SuccessDataResult<List<User>>(users, Messages.Listed);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            return new SuccessDataResult<User>(user,Messages.Listed);
        }

        public IDataResult<User> GetByUsername(string username)
        {
            var user = _userDal.Get(u => u.Username==username);
            return new SuccessDataResult<User>(user, Messages.Listed);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(result, Messages.Listed);
        }
    }
}
