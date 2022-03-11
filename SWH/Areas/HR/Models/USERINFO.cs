using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SWH.Areas.HR.Models
{
    [Table("USERINFO")]
    public class USERINFO
    {
        [Key]
        public int USERID { get; set; }
        public int PositionID { get; set; }
        public string Name { get; set; }
        public string FName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public decimal GorssSalary { get; set; }
        public DateTime DOB { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? ResignDate { get; set; }
        public string Gender { get; set; }
        public string CAddress { get; set; }
        public string PAddress { get; set; }
        public int Status { get; set; }
        public int QualificationID { get; set; }
        public string Specilization { get; set; }
        public string University { get; set; }
        public int Experiance { get; set; }
        public string Remarks { get; set; }
        public string ResignReason { get; set; }
    }
}