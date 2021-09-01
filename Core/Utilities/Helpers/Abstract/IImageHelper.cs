using System;
using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.Abstract
{
    public interface IImageHelper
    {
        IDataResult<string> Upload(IFormFile file);
        IResult Delete(string imageGuid);
    }
}
