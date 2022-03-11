using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.Models
{
    [Table("GulsazeExpensesMenu")]
    
    public class GulsazeExpensesMenu
    {
        [Key]
        public int GEMID { get; set; }
        public string GEMName { get; set; }
        public string Unit { get; set; }
        public string Remarks { get; set; }
        public ICollection<GulsazeExpenses> GulsazeExpenses { get; set; }
    }
}