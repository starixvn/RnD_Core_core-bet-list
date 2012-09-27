<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestLogging.aspx.cs" Inherits="TestLogging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Message Log
        <asp:TextBox ID="txtMessage" runat="server" Width="300px"></asp:TextBox>
        <br />
        <asp:Button ID="btnLogging" runat="server" Text="Logging" OnClick="btnLogging_Click"/>
    </div>
    <br />
     <a href="Default.aspx">Default page</a>
    </form>
</body>
</html>
