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
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var cars = carManager.GetCarsByBrandId(2);
            foreach (var car in cars)
            {
                Console.WriteLine(car.Description + " " + car.ModelYear + " " + car.DailyPrice);
            }
        }
    }
}
