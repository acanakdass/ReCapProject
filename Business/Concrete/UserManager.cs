using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class UserManager:IUserService
    {
        IUserDal _userDal;
        private IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user != null)
            {
                user.IsDeleted = true;
                _userDal.Update(user);
                return new SuccessResult(Messages.Deleted);
            }
                return new ErrorDataResult<User>(Messages.UserNotFound);
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
        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(u => u.Id== id);
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(user, Messages.Listed);
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

        public IDataResult<User> GetCurrentUser()
        {
            var result = _httpContextAccessor.HttpContext.User.Identities.FirstOrDefault();
            var userId = result.Claims.FirstOrDefault().Value;
            //var userMail = result.Claims.ElementAt(1).Value;
            //var userName = result.Claims.ElementAt(2).Value;
            //var userRole = result.Claims.ElementAt(3).Value;

            User currentUser = GetById(Int32.Parse(userId)).Data;
            return new SuccessDataResult<User>(currentUser, Messages.Listed);
            //return new ErrorDataResult<IEnumerable<ClaimsIdentity>>(null,Messages.NotFound);
        }
    }
}
