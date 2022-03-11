using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("CategoryItems")]
    
    public class CategoryItems
    {
        [Key]
        public int CIID { get; set; }
        public int CID { get; set; }
        public int CCID { get; set; }
        public string CIName { get; set; }
        public string CICode { get; set; }
        public string CIUnit { get; set; }
        public string CIRemarks { get; set; }

        public ICollection<OrderItems> Assests { get; set; }
        public ICollection<BookingItems> BookingItems { get; set; }
        public Category Category { get; set; }
        public CategoryClass CategoryClass { get; set; }
    }
}