using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CID { get; set; }
        public string CName { get; set; }
        public string CCode { get; set; }
        public string CRemarks { get; set; }

        public ICollection<CategoryItems> CategoryItems { get; set; }
    }
}