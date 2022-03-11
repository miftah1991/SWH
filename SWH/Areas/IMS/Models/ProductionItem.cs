using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("ProductionItem")]
    
    public class ProductionItem
    {
        [Key]
        public int PIID { get; set; }
        public int PID { get; set; }
        public int PMID { get; set; }
        public decimal Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public string Remarks { get; set; }

        public Production Production { get; set; }
        public ProductionMenu ProductionMenu { get; set; }
    }
}