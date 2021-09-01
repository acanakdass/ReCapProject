using System;
using System.IO;
using Core.Utilities.Helpers.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        public IResult Delete(string imageGuid)
        {
            throw new NotImplementedException();
        }

        public IDataResult<string> Upload(IFormFile file)
        {

            try
            {
                if (!Directory.Exists("wwwroot/uploadedImages"))
                {
                    Directory.CreateDirectory("wwwroot/uploadedImages");
                }

                string fileExtension = Path.GetExtension(file.FileName);
                string fileGuid = Guid.NewGuid().ToString();
                string fileName = $"{fileGuid}{fileExtension}";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadedImages", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return new SuccessDataResult<string>(path,"Fotoğraf başarıyla yüklendi");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<string>(null,"Fotoğraf yüklenirken bir hata oluştu :" + ex);
            }

        }
    }
}
