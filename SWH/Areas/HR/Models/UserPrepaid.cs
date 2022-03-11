using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("UserPrepaid")]
    
    public class UserPrepaid
    {
        [Key]
        public int PrepaidID { get; set; }
        public int USERID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public decimal DeductedAmount { get; set; }
    }
}