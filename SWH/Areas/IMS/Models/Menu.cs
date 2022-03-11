using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("Menu")]
    
    public class Menu
    {
        [Key]
        public int MID { get; set; }
        public string Name { get; set; }
        public decimal? MenuUnitPrice { get; set; }
        public string Remarks { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}