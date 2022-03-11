using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("Orders")]
    
    public class Orders
    {
        [Key]
        public int OID { get; set; }
        public int OCode { get; set; }
        public DateTime? ODate { get; set; }
        public string ORemarks { get; set; }
        public int BOOKINGID { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; }
        //public ICollection<BookingItems> BookingItems { get; set; }
    }
}