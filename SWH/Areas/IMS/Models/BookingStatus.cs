using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("BookingStatus")]
    
    public class BookingStatus
    {
        [Key]
        public int BSID { get; set; }
        public string Status { get; set; }
        public ICollection<Booking> Booking { get; set; }
    }
}