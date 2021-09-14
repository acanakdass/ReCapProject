using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarTest();
            GetCarDetails();
            Console.WriteLine("aa");
        }

        private static void CarByBrandId(int brandId)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var cars = carManager.GetCarsByBrandId(brandId);
            
        }

        private static void GetCarDetails()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            Console.WriteLine(result.Data[1].CarName);
        }
    }
}
