using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("ProductionMenu")]
    
    public class ProductionMenu
    {
        [Key]
        public int PMID { get; set; }
        public string PMName { get; set; }
        public string PMUnit { get; set; }
        public decimal? PMUnitPrice { get; set; }

        public ICollection<ProductionItem> ProductionItems { get; set; }
    }
}