using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("GulsazeExpenses")]
    
    public class GulsazeExpenses
    {
        [Key]
        public int GEID { get; set; }
        public int GEMID { get; set; }
        public decimal? GEPricePerUnit { get; set; }
        public decimal GEQuantity { get; set; }
        public DateTime GEDate { get; set; }
        public string GERemarks { get; set; }
        public GulsazeExpensesMenu GulsazeExpensesMenu { get; set; }
    }
}