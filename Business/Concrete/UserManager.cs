using System;
using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

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
            return new SuccessResult("Renk Eklendi");
        }


        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), "Tüm markalar listelendi");
        }

        public IDataResult<User> GetById(int userId)
        {
            var user = _userDal.Get(b => b.Id == userId);
            return new SuccessDataResult<User>(user, "Renk listelendi");
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult("Renk silindi");
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult("Renk güncellendi");
        }
    }
}
