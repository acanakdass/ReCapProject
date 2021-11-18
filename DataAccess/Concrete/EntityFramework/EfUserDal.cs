﻿using System;
using System.Collections.Generic;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DataAccess.Concrete.EntityFramework
{
   public class EfUserDal : EfEntityRepositoryBase<User, ReCapContext>, IUserDal
   {
        //private HttpContextAccessor httpContext;

        //public EfUserDal(HttpContextAccessor httpContext)
        //{
        //    this.httpContext = httpContext;
        //}

        public List<OperationClaim> GetClaims(User user)
      {
         using (var context = new ReCapContext())
         {
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims
                         on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();
         }
      }

        //public User GetCurrentUser()
        //{
        //    var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    return this.Get(u => u.Id.ToString() == userId);
        //}
    }
}
