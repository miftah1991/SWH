using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("BookingItems")]
    
    public class BookingItems
    {
        [Key]
        public int BIID { get; set; }
        public int BOOKINGID { get; set; }
        //public int OIID { get; set; }
        public int CIID { get; set; }
        public decimal BIQuantity { get; set; }
        public decimal? BIPricePerUnit { get; set; }
        public int IsExpandable { get; set; }
        public DateTime? BIDate { get; set; }
        public string OtherExpenseName { get; set; }
        public Booking Booking { get; set; }
        public CategoryItems CategoryItems { get; set; }
    }
}