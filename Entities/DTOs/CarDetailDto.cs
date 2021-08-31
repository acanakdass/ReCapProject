﻿using System;
using System.Collections.Generic;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class CarDetailDto:IDto
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public int ModelYear { get; set; }
        public List<string> CarImagePaths { get; set; }
    }
}
