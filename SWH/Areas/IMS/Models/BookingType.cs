using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
     [Table("BookingType")]
    public class BookingType
    {
         [Key]
        public int BTID { get; set; }
        public string BTName { get; set; }
        public string Remarks { get; set; }
        public ICollection<Booking> Booking { get; set; }
    }
}