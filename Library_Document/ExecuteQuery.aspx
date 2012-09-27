<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecuteQuery.aspx.cs" Inherits="ExecuteQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>
            Query</h3>
        <asp:GridView ID="gvAllLicensee" runat="server">
        </asp:GridView>
        <h3>
            Query Entity</h3>
        <asp:Label ID="lbLicenseeID" runat="server"></asp:Label>
        <asp:Label ID="lbLicenseeName" runat="server"></asp:Label>
        <asp:Label ID="lbIsDeposit" runat="server"></asp:Label>
        <asp:Label ID="LbDescription" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
