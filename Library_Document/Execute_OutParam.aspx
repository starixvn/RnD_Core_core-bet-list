<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Execute_OutParam.aspx.cs"
    Inherits="Execute_InputParam_Without_OutputParam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- <div>
            <h1>
                Function</h1>
            <p>
                public static DbResult Execute(string settingKey, Hashtable parameters)
            </p>
            <p>
                public static DbResult Execute(string settingKey, Hashtable parameters, int timeOut)
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <asp:TextBox ID="txtSpName" runat="server" Text="OA_UserCheckIn" Width="300px"></asp:TextBox>
                Param Name
                <div>
                    CustID
                    <asp:TextBox ID="txtCustID" runat="server" Text="1"></asp:TextBox></div>
                <div>
                    NetSessionID
                    <asp:TextBox ID="txtNetSessionID" runat="server" Text="1"></asp:TextBox></div>
                <div>
                    IP
                    <asp:TextBox ID="txtIP" runat="server" Text="1"></asp:TextBox></div>
                <asp:Button ID="btnExecute" runat="server" Text="Execute" OnClick="btnExecute_Click" />
            </p>--%>
            <asp:Label ID="lbError" runat="server"></asp:Label>
            <asp:DataGrid ID="gvSource" runat="server">
            </asp:DataGrid>
        </div>
    </div>
    </form>
</body>
</html>
