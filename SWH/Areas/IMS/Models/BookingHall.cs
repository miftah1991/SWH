using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("BookingHall")]
    
    public class BookingHall
    {
        [Key]
        public int BHID { get; set; }
        public string Name { get; set; }
        public string ShiftName { get; set; }
        public ICollection<Booking> Booking { get; set; }
    }
}