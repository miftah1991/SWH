using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWH.Areas.IMS.ViewModels
{
    public class MenuItemsSettingVM
    {
    public int? MISID {get;set;}
    public int? MID {get;set;}
    public int? CIID {get;set;}
    public int? Person {get;set;}
    public decimal? Amount {get;set;}
    public string Remarks {get;set;}
    public string MenuName {get;set;}
    public decimal? MenuUnitPrice { get; set; }
    public string CIName {get;set;}
    public int? CID { get; set; }
    public string CICode { get; set; }
    public string CIUnit { get; set; }
    

    }
}