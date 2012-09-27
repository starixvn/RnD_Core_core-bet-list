<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecuteTable.aspx.cs" Inherits="Execute_Without_Param" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <%--  <h1>
                Function</h1>
            <p>
                public static DbResult Execute(string settingKey)
            </p>
            <p>
                public static DbResult Execute(string settingKey, int timeOut)
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <asp:TextBox ID="txtSpName" runat="server" Text="OA_ResellersGet"></asp:TextBox>
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
