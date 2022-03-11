using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("GulsazeItem")]
    
    public class GulsazeItem
    {
        [Key]
        public int GIID { get; set; }
        public int GID { get; set; }
        public int GMID { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal? Quantity { get; set; }
        public string Remarks { get; set; }
        public Gulsaze Gulsaze { get; set; }
    }
}