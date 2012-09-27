<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecuteNonquery.aspx.cs"
    Inherits="Execute_InputParam_Without_OutputParam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <%--  <h1>
                Function</h1>
            <p>
              public static int ExecuteNonQuery(string settingKey)
            </p>
            <p>
                public static int ExecuteNonQuery(string settingKey, int timeOut)
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <asp:TextBox ID="txtSpName" runat="server" Text="Update_Licensees_TestTin" Width="300px"></asp:TextBox>
                Param Name
                <div>
                    LicenseeID
                    <asp:TextBox ID="txtLicenseeID" runat="server" Text="5"></asp:TextBox></div>
                <div>
                    LicenseeName
                    <asp:TextBox ID="txtLicenseeName" runat="server" Text="Sample"></asp:TextBox></div>
                <asp:Button ID="btnExecute" runat="server" Text="Execute" OnClick="btnExecute_Click" />
            </p>--%>
            <table>
                <tr>
                    <td>
                        <div>
                            Before Execute</div>
                        <asp:GridView ID="gvOldSource" runat="server">
                        </asp:GridView>
                    </td>
                    <td>
                        <div>
                            After Execute</div>
                        <asp:GridView ID="gvNewSource" runat="server">
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbError" runat="server"></asp:Label>
            <div>
                Row Affect</div>
            <asp:Label ID="lbRowAffect" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
