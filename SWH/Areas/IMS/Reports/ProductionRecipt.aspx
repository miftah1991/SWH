<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductionRecipt.aspx.cs" Inherits="SWH.Areas.IMS.Reports.ProductionRecipt" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <rsweb:ReportViewer ID="PRRV" runat="server"></rsweb:ReportViewer>
        <asp:ScriptManager ID="PRSM" runat="server"></asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>
