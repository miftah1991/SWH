using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWH.Areas.IMS.Models;

namespace SWH.Areas.IMS.ViewModels
{
    public class CategoryItemsVM
    {
        public int CIID { get; set; }
        public int? CID { get; set; }
        public string CIName { get; set; }
        public string CICode { get; set; }
        public string CIUnit { get; set; }
        public string CIRemarks { get; set; }
        public string CName { get; set; }
        public string CCode { get; set; }
        public string CRemarks { get; set; }
        public int? CCID { get; set; }
        public string CCName { get; set; }
    }
}