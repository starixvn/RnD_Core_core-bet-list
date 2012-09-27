<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KeyDetails.aspx.cs" Inherits="KeyDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:RadioButton ID="rdoCache" runat="server" Text="From Cache" GroupName="group1"
            Checked="true" />
        <asp:RadioButton ID="rdoResource" runat="server" Text="From Resource" GroupName="group1" />
        <br />
        Key
        <asp:TextBox ID="txtKey" runat="server" Width="300px"></asp:TextBox>
        <asp:Button ID="btnViewKey" runat="server" Text="Search" OnClick="btnViewKey_Click" />
        <br />
        <br />
        <div>
            <div>
                Sp Name :
                <%--  <asp:TextBox ID="txtSpName" runat="server" Style="margin-left: 60px" Width="300px"></asp:TextBox>--%>
                <asp:Label ID="lbSpName" runat="server"></asp:Label>
            </div>
            <br />
            <div>
                Db Name :<%--<asp:TextBox ID="txtDbName" runat="server" Style="margin-left: 60px"></asp:TextBox>--%>
                <asp:Label ID="lbDbName" runat="server"></asp:Label>
            </div>
            <br />
            <div>
                Connection :
                <%--<asp:TextBox ID="txtConection" runat="server" Style="margin-left: 60px" Width="500px"
                    TextMode="MultiLine" Height="50px"></asp:TextBox>--%>
                <asp:Label ID="lbConnection" runat="server"></asp:Label>
            </div>
            <br />
        </div>
        <br />
        <br />
        <h3>
            Add or Clear Ext</h3>
        <div>
            Key<asp:TextBox ID="txtAddExt" runat="server" Width="700px"></asp:TextBox>Ext<asp:TextBox
                ID="txtExt" runat="server" Text="xtest" Width="70px"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="Add Ext" OnClick="btnAdd_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Clear Ext" OnClick="btnClear_Click" />
        </div>
        <div>
            <asp:Label ID="lbMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <br />
         <a href="Default.aspx">Default page</a>
    </div>
    </form>
</body>
</html>
