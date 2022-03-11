﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryReport.aspx.cs" Inherits="SWH.Areas.IMS.Reports.InventoryReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="IRSM" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="IRRV" runat="server"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
