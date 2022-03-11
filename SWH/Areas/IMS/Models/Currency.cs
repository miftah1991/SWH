using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
   [Table("Currency")]

    public class Currency
    {
       [Key] 
       public int CRID { get; set; }
        public string CRName { get; set; }
        public ICollection<BookingPayment> BookingPayment { get; set; }
    }
}