using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VidlyApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        [DataType(DataType.Date)]
        [Min18YearsValidation]
        public DateTime? Birthdate { get; set; }
        public MemberShipType MemberShipType { get; set; }
        [Required]
        [Display(Name="Membership Type")]
        public int MemberShipTypeId { get; set; }
    }
}