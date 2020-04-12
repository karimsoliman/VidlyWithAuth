using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyApp.Models;

namespace VidlyApp.ViewModels
{
    public class CustomerFormVm
    {
        public Customer Customer { get; set; }
        public IEnumerable<MemberShipType> MembershipTypes { get; set; }
    }
}