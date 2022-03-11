using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("CategoryClass")]
    
    public class CategoryClass
    {
        [Key]
        public int CCID { get; set; }
        public string CCName { get; set; }
        public string CCRemarks { get; set; }
        public ICollection<CategoryItems> CategoryItems { get; set; }
    }
}