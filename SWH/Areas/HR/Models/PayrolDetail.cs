using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("PayrolDetail")]
    
    public class PayrolDetail
    {
        [Key]
        public int PID { get; set; }
        public DateTime Date { get; set; }
        public int USERID { get; set; }
        public int Days { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        public decimal Tax { get; set; }
        public decimal AttDeducted { get; set; }
        public decimal PrePaidDeductd { get; set; }
        public decimal Payable { get; set; }
    }
}