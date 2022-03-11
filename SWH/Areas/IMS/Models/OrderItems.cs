using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("OrderItems")]
    
    public class OrderItems
    {
            [Key]
            public int OIID { get; set; }
            public int CIID { get; set; }
            public int OID { get; set; }
            public decimal OIQuantity { get; set; }
            public decimal OIPricePerUnit { get; set; }
            public decimal? OIDiscount { get; set; }
            public int OIStatus { get; set; }
            public string OIRemarks { get; set; }
            public CategoryItems CategoryItems { get; set; }
            public Orders Orders { get; set; }
            //public ICollection<BookingItems> BookingItems { get; set; }
    }
}