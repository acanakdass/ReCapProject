using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Add(User user);
        IResult Delete(int userId);
        IDataResult<User> GetByMail(string email);
        IDataResult<User> GetById(int id);
        IDataResult<User> GetCurrentUser();
    }
}
