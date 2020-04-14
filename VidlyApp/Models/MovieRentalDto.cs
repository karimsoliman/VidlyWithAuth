using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyApp.Models
{
    public class MovieRentalDto
    {
        public int CustomerID { get; set; }
        public List<int> MovieIDs { get; set; }
    }
}