<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        code
        {
            color: Blue;
        }
        pre
        {
            white-space: pre-wrap;
            white-space: -moz-pre-wrap;
            white-space: -o-pre-wrap;
            background: #faf8f0;
            font-size: 15px;
            color: Teal;
        }
        #content #pre-stylish code
        {
            display: block;
            padding: 0.5em 1em;
            border: 1px solid #bebab0;
        }
        settingKey
        {
            color: Red;
        }
    </style>
    <script type="text/javascript" src="jquery.js"></script>
    <link href="Css/core.css" rel="Stylesheet" type="text/css" />
    <link href="Css/ph_detail.css" rel="Stylesheet" type="text/css" />
</head>
<body style="margin-left: 200px">
    <form id="form1" runat="server">
    <h2>
        Document Fanex Data</h2>
    <h4>
        <dl>
            <dt>
                <p>
                    <a href="DocumentObjectDB.aspx">ObjectDb Documents</a> (View Fanex.Data document)
                </p>
            </dt>
            <dt>
                <p>
                    <a href="Statistics.aspx">Statistics</a> (View how good Fanex - Dapper is)
                </p>
            </dt>
            <dt>
                <p>
                    <a href="KeyDetails.aspx">Search and Edit file resource</a> (Check watching config)
                </p>
            </dt>
            <dt>
                <p>
                    <a href="TestLogging.aspx">Test Logging Services</a> (Testing Logging Service)
                </p>
            </dt>
        </dl>
    </h4>
    </form>
</body>
</html>
