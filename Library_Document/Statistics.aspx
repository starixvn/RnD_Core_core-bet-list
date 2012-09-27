<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Statistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js"></script>
    <%=StackExchange.Profiling.MiniProfiler.RenderIncludes()%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Number of Records:
        <asp:DropDownList ID="ddlRecord" runat="server">
            <asp:ListItem Text="100" Value="100" Selected="True"></asp:ListItem>
            <asp:ListItem Text="300" Value="300"></asp:ListItem>
            <asp:ListItem Text="500" Value="500"></asp:ListItem>
            <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
            <asp:ListItem Text="2000" Value="2000"></asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp; Running Number:
        <asp:DropDownList ID="ddlNumberOfRuntime" runat="server">
            <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
            <asp:ListItem Text="10" Value="10"></asp:ListItem>
            <asp:ListItem Text="15" Value="15"></asp:ListItem>
            <asp:ListItem Text="20" Value="20"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <br />
    <div>
        <asp:CheckBox ID="chkFanexDapper" runat="server" Checked="true" Text="Fanex-Dapper" />
        <asp:CheckBox ID="chkEntlib" runat="server" Checked="true" Text="Fanex-Entlib" />
        <asp:CheckBox ID="chkADO" runat="server" Checked="true" Text="ADO.Net" />
        <asp:CheckBox ID="chkDataSet" runat="server" Checked="true" Text="Dataset" />
        <asp:Button ID="btnLoadNew" runat="server" Text="Run" OnClick="RunStatictics" />
        <i>
            
                Select top @record from bettrans
        </i><a href="Default.aspx">Go to default page</a>
    </div>
    <br />
    <div>
        <asp:Chart ID="Chart1" runat="server" Palette="Excel" BackColor="255, 224, 192" ImageType="Png"
            ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="1200px" Height="600px"
            BorderDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="181, 64, 1">
            <Series>
                <asp:Series BorderColor="180, 26, 59, 105" BorderWidth="3" ChartArea="ChartArea1"
                    ChartType="Line" Color="220, 65, 140, 240" Legend="Default" MarkerSize="8" MarkerStyle="Circle"
                    Name="Fanex-Dapper" ShadowColor="Black" ShadowOffset="2" XValueType="Double"
                    YValueType="Double">
                </asp:Series>
                <asp:Series BorderColor="180, 26, 59, 105" BorderWidth="3" ChartArea="ChartArea1"
                    ChartType="Line" Color="220, 224, 64, 10" Legend="Default" MarkerSize="9" MarkerStyle="Diamond"
                    Name="Fanex-EntlibReader" ShadowColor="Black" ShadowOffset="2" XValueType="Double"
                    YValueType="Double">
                </asp:Series>
                <asp:Series BorderColor="180, 26, 59, 105" BorderWidth="3" ChartArea="ChartArea1"
                    ChartType="Line" Color="222, 225, 60, 240" Legend="Default" MarkerSize="9" MarkerStyle="Diamond"
                    Name="ADOReader" ShadowColor="Black" ShadowOffset="2" XValueType="Double" YValueType="Double">
                </asp:Series>
                <asp:Series BorderColor="180, 26, 59, 105" BorderWidth="3" ChartArea="ChartArea1"
                    ChartType="Line" Color="Gray" Legend="Default" MarkerSize="9" MarkerStyle="Diamond"
                    Name="Dataset" ShadowColor="Black" ShadowOffset="2" XValueType="Double" YValueType="Double">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea BackColor="OldLace" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                    BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" Name="ChartArea1" ShadowColor="Transparent">
                    <AxisY LineColor="64, 64, 64, 64">
                        <MajorGrid LineColor="64, 64, 64, 64" />
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                    </AxisY>
                    <AxisX LineColor="64, 64, 64, 64">
                        <MajorGrid LineColor="64, 64, 64, 64" />
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                    </AxisX>
                    <Area3DStyle Inclination="40" IsRightAngleAxes="False" LightStyle="Realistic" Perspective="9"
                        Rotation="25" WallWidth="3" />
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent"
                    Font="Trebuchet MS, 8.25pt, style=Bold" AutoFitMinFontSize="10" 
                    DockedToChartArea="ChartArea1" Docking="Left" 
                    TitleFont="Microsoft Sans Serif, 10pt, style=Bold" >
                </asp:Legend>
            </Legends>
            <BorderSkin SkinStyle="Emboss"></BorderSkin>
        </asp:Chart>
    </div>
    </form>
</body>
</html>
