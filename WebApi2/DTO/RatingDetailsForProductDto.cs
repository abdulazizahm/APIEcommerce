using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.DTO
{
    public class RatingDetailsForProductDto
    {
        public int aveg { get; set; }
        public int count { get; set; }
        public double pres { get; set; }
        public int rat5 { get; set; }
        public int rat4 { get; set; }
        public int rat3 { get; set; }
        public int rat2 { get; set; }
        public int rat1 { get; set; }
        public double rat5pres { get; set; }
        public double rat4pres { get; set; }
        public double rat3pres { get; set; }
        public double rat2pres { get; set; }
        public double rat1pres { get; set; }

    }
}