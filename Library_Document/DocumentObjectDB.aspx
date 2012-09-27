<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentObjectDB.aspx.cs"
    Inherits="MultiMapping" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="css/core.css" />
    <link type="text/css" rel="stylesheet" href="css/ph_detail.css" />
    <script type="text/javascript" src="jquery.js"></script>
    <script type="text/javascript">
        function HideAll() {
            $("#simpleMapperNo").hide();
            $("#simpleMapperYes").hide();
            $("#multiMapperNo").hide();
            $("#multiMapperYes").hide();
            $("#mappingColumn").hide();
            $("#howConfig").hide();
            $("#executeDataSet").hide();
            $("#executeDataReader").hide();
            $("#executeNonquery").hide();
            $("#executeScalar").hide();
            $("#gettingstart").hide();
        }

        $(document).ready(function () {
            HideAll();
        });

        function SimpleMapperNo() {
            HideAll();
            $("#simpleMapperNo").show();
        }
        function SimpleMapperYes() {
            HideAll();
            $("#simpleMapperYes").show();
        }

        function MultiMapperNo() {
            HideAll();
            $("#multiMapperNo").show();
        }
        function MultiMapperYes() {
            HideAll();
            $("#multiMapperYes").show();
        }

        function MappingClumn() {
            HideAll();
            $("#mappingColumn").show();
        }

        function HowToConfig() {
            HideAll();
            $("#howConfig").show();
        }

        function ExecuteDataSet() {
            HideAll();
            $("#executeDataSet").show();
        }
        function ExecuteDataReader() {
            HideAll();
            $("#executeDataReader").show();
        }
        function ExecuteNonquery() {
            HideAll();
            $("#executeNonquery").show();
        }
        function ExecuteScalar() {
            HideAll();
            $("#executeScalar").show();
        }
        function GettingStart() {
            HideAll();
            $("#gettingstart").show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tbody>
                <tr class="pscontent">
                    <td class="pscolumnl">
                        <div class="phead">
                            Fanex Data Document</div>
                        <li class="psgap"></li>
                        <ul class="pslist">
                            <li class="psmeta"><b>Fanex Data Overview</b></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="HowToConfig()" style="margin-left: 20px">
                                How to config</a></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="GettingStart()" style="margin-left: 20px">
                                Getting start</a></li>
                            <br />
                            <li class="psmeta"><b>Fanex Data ObjectDb</b></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="ExecuteDataSet()" style="margin-left: 20px">
                                ExecuteDataSet</a></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="ExecuteDataReader()" style="margin-left: 20px">
                                ExecuteDataReader</a></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="ExecuteNonquery()" style="margin-left: 20px">
                                ExecuteNonquery</a></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="ExecuteScalar()" style="margin-left: 20px">
                                ExecuteScalar</a></li>
                            <li class="psmeta"><b>A simple object mapper</b></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="SimpleMapperNo()" style="margin-left: 20px">
                                Without input parameters</a></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="SimpleMapperYes()" style="margin-left: 20px">
                                With input parameters</a></li>
                            <li class="psgap"></li>
                            <%-- <li class="psmeta"><b>Multi mapping</b></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="MultiMapperNo()">Without input
                                parameters</a></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="MultiMapperYes()">With input
                                parameters</a></li>
                            <li class="psgap"></li>--%>
                            <li class="psmeta"><b>Specific</b></li>
                            <li class="psmeta"><a href="javascript:void(0);" onclick="MappingClumn()" style="margin-left: 20px">
                                Mapping colums</a></li>
                            <li>
                            <br />
                            <br />
                            <br />
                            <a href="Default.aspx">Default page</a> </li>
                        </ul>
                    </td>
                    <td id="wikicontent" class="psdescription">
                        <div id="howConfig">
                            <h1>
                                <a>Fanex Data- How to config</a></h1>
                            <p>
                                Config file follow
                            </p>
                            <pre class="prettyprint">
<span class="typ">< configsections> </span>
<span class="typ">< section name="<span class="kwd">Fanex.Data.DbSettingProvider</span>"
    type="<span class="kwd">Fanex.Data.Configuration.DbSettingProviderConfigurationSection,Fanex.Data</span>"/></span> 
<span class="typ">< /configsections></span>
                            </pre>
                            <pre class="prettyprint">
<span class="typ">< Fanex.Data.DbSettingProvider default="XmlDbSettingProvider"></span>
    <span class="typ">< providers></span>
        <span class="typ">< add</span>
        <span class="typ">name="<span class="kwd">XmlDbSettingProvider</span>"</span>
        <span class="typ">type="<span class="kwd">Fanex.Data.Configuration.XmlDbSettingProvider,Fanex.Data</span>"</span>
        <span class="typ">resourcePath="<span class="kwd">SpName.xml</span>"</span>
        <span class="typ">enableWatching = "<span class="kwd">1</span>" /></span>
    <span class="typ">< /providers></span>
<span class="typ">< /Fanex.Data.DbSettingProvider></span>
                            </pre>
                            <p>
                                resourcePath : path of file resource contains Sp name (xml file)
                            </p>
                            <p>
                                enableWatching : enable (1/true) or disable(0/false) watching file resource (re
                                cache)
                            </p>
                            <pre class="prettyprint">
<span class="typ">< connectionStrings></span>
            <span class="typ">< add name="<span class="kwd">RTSportcast</span>" connectionString="<span
                class="kwd">Data Source=10.19.10.21\SQLEXPRESS;Initial Catalog=RTSportcast;</span>"
                providerName="<span class="kwd">System.Data.SqlClient</span>"/></span>
<span class="typ">< /connectionStrings></span>
                            </pre>
                            <h1>
                                Struct of file resource (XML file)</h1>
                            <pre class="prettyprint">
<span class="typ">< ?xml version="1.0" encoding="utf-8"?></span>
<span class="typ">< Functions></span>
    <span class="typ">< Function Name="<span class="kwd">Report</span>"></span>
        <span class="typ">< Setting Key="<span class="kwd">uspGetEmployeeManagers</span>" SpName="<span
            class="kwd">uspGetEmployeeManagers</span>" DbName="<span class="kwd">Adv</span>"
            Plain="<span class="kwd">1</span>" /></span>
        <span class="typ">< Setting Key="<span class="kwd">OA_GetFunctionByUserAndParentID</span>"
            SpName="<span class="kwd">OA_GetFunctionByUserAndParentID</span>" DbName="<span class="kwd">Licensee</span>"/></span>
    <span class="typ">< /Functions></span>
<span class="typ">< /Functions></span>
                            </pre>
                        </div>
                        <div id="gettingstart">
                            <h1>
                                Getting start</h1>
                            <p>
                                Add references
                            </p>
                            <pre class="prettyprint">
                                System.Configuration
                            </pre>
                            In code that run application start up you must implement code :
                            <pre class="prettyprint">
                                Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
                            </pre>
                            Example : ASP.NET Global.asax file
                            <pre class="prettyprint">
void Application_Start(object sender, EventArgs e) 
{
        // Code that runs on application startup
        Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
}
                            </pre>
                        </div>
                        <div id="executeDataSet">
                            <h1>
                                Fanex Data ExecuteDataSet</h1>
                            <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    Name nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )
                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_Sel
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events;</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_SelFilter(@eventid INT)
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events WHERE Events.EventID=@eventid</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <p>
                                Example</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                DataSet ds=db.ExecuteDataSet();
            }
                                 </pre>
                            <p>
                                With input param you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_SelFilter"</span>))
            {
                DataSet ds=db.ExecuteDataSet(new {eventid = 1 });
            }
                                 </pre>
                            <p>
                                Or you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                db.AddInParameter("eventid",DbType.Int32,1);
                DataSet ds=db.ExecuteDataSet();
            }
                                 </pre>
                        </div>
                        <div id="executeDataReader">
                            <h1>
                                Fanex Data ExecuteDataSet</h1>
                            <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    Name nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )
                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_Sel
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events;</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_SelFilter(@eventid INT)
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events WHERE Events.EventID=@eventid</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <p>
                                Example</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                using(var reader=db.ExecuteDataReader())
                {
                    //blocks
                }
            }
                                 </pre>
                            <p>
                                With input param you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_SelFilter"</span>))
            {               
                using(var reader=db.ExecuteDataReader( new {eventid = 1 }))
                {
                    //blocks
                }
            }
                                 </pre>
                            <p>
                                Or you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                db.AddInParameter("eventid",DbType.Int32,1);
                using(var reader=db.ExecuteDataReader())
                {
                    //blocks
                }
            }
                                 </pre>
                        </div>
                        <div id="executeNonquery">
                            <h1>
                                Fanex Data ExecuteDataSet</h1>
                            <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    Name nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )
                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_Upd(@eventid INT,@name NVARCHAR(100))
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> UPDATE Events SET Events.Name=@name WHERE Events.EventID=@eventid</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <p>
                                Example</p>
                            <p>
                                With input param you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Upd"</span>))
            {  
                 db.ExecuteNonquery(new {eventid = 1, name = "New Name"});
            }
                                 </pre>
                            <p>
                                Or you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Upd"</span>))
            {
                db.AddInParameter("eventid",DbType.Int32,1);
                db.AddInParameter("name",DbType.NVarChar,"New Name");
                db.ExecuteNonquery();
                
            }
                                 </pre>
                        </div>
                        <div id="executeScalar">
                            <h1>
                                Fanex Data ExecuteDataSet</h1>
                            <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    Name nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )
                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_Sel
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events;</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_SelFilter(@eventid INT)
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events WHERE Events.EventID=@eventid</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <p>
                                Example</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                object obj = db.ExecuteScalar();
            }
                                 </pre>
                            <p>
                                With input param you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_SelFilter"</span>))
            {               
                object obj = db.ExecuteScalar(new {eventid =1 });
            }
                                 </pre>
                            <p>
                                Or you can use</p>
                            <pre class="prettyprint">
            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                db.AddInParameter("eventid",DbType.Int32,1);
                object obj = db.ExecuteScalar();
            }
                                 </pre>
                        </div>
                        <div id="simpleMapperNo">
                            <h1>
                                <a>Fanex - A simple object mapper</a></h1>
                            <h3>
                                <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    Name nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )
                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_Sel
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events;</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <h3>
                                Data object class</h3>
                            <pre class="prettyprint">
                                    <span class="kwd">pubic class</span> <span class="typ">FanexEvents</span>
                                    {
                                         <span class="kwd">pubic int</span> EventID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> Name {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> Player {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> ServerTime {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic DateTime</span> MatchID {<span class="kwd">get; set;</span>}
                                    }
                                </pre>
                            <p>
                                <h3>
                                    Using Fanex mapper is easier for mapping data</h3>
                            </p>
                            <p>
                                Execute a query and map the results to a strongly typed List</p>
                            <pre class="prettyprint">

            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                IEnumerable< <span class="kwd">FanexEvents</span> listEvents = db.Query<<span class="kwd">FanexEvents</span>>();
            }
                                    </pre>
                            <p>
                                Execute a query and map the results to an objects</p>
                            <pre class="prettyprint">

            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
               <span class="kwd">FanexEvents</span> events = db.QueryEntity<<span class="kwd">FanexEvents</span>>();
            }
                                    </pre>
                        </div>
                        <div id="simpleMapperYes">
                            <h1>
                                <a>Fanex - A simple object mapper</a></h1>
                            <h3>
                                <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    Name nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )
                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
                                     <span class="kwd">CREATE PROCEDURE</span> Events_Sel (@eventid INT)
                                     <span class="kwd">AS BEGIN</span>
                                       <span class="str"> SELECT * FROM Events WHERE Events.EventID=@eventid;</span>
                                     <span class="kwd">END</span>
                                </pre>
                            <h3>
                                Data object class</h3>
                            <pre class="prettyprint">
                                    <span class="kwd">pubic class</span> <span class="typ">FanexEvents</span>
                                    {
                                         <span class="kwd">pubic int</span> EventID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> Name {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> Player {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> ServerTime {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic DateTime</span> MatchID {<span class="kwd">get; set;</span>}
                                    }
                                </pre>
                            <p>
                                <h3>
                                    Using Fanex mapper is easier for mapping data</h3>
                            </p>
                            <p>
                                Execute a query and map the results to a strongly typed List</p>
                            <pre class="prettyprint">

            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                IEnumerable< <span class="kwd">FanexEvents</span> listEvents = db.Query<<span class="kwd">FanexEvents</span>>(new {eventid= 1 });
            }
                                    </pre>
                            <p>
                                If use want add in parameters is more details you can use</p>
                            <pre class="prettyprint">

            <span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
            {
                db.AddInParameter("eventid", DbType.Int32, 1);
                IEnumerable< <span class="kwd">FanexEvents</span> listEvents = db.Query<<span class="kwd">FanexEvents</span>>();
            }
                                    </pre>
                        </div>
                        <div id="multiMapperNo">
                            <h1>
                                <a>Fanex - Multi object mapper</a></h1>
                            <h3>
                                <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    EventName nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )

                                    CREATE TABLE Matches(
	                                    MatchID int PRIMARY KEY,
	                                    MatcheName nvarchar(100) NULL,
	                                    StatusID varchar(2) NULL
                                    )

                                    CREATE TABLE Status(
	                                    StatusID varchar(2) PRIMARY KEY,
	                                    StatusName nvarchar(100) NULL	                                    
                                    )

                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
    <span class="kwd">CREATE PROCEDURE</span> Events_Sel
    <span class="kwd">AS BEGIN</span>
    <span class="str"> SELECT Events.EventID,Events.EventName ,Matches.MatcheID,Matches.MatchName,Status.StatusID,Status.StatusName
        FROM Events INNER JOIN Matches On Events.EventID=Matches.MatchID INNER JOIN Status
        On Matches.StatusID=Status.StausID;</span>
    <span class="kwd">END</span>
                                </pre>
                            <h3>
                                Data object class</h3>
                            <pre class="prettyprint">
                                    <span class="kwd">pubic class</span> <span class="typ">FanexEvents</span>
                                    {
                                         <span class="kwd">pubic int</span> EventID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> EventName {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic FanexMatches</span> TempMatch {<span class="kwd">get; set;</span>}      
                                         <span class="kwd">pubic FanexStatus</span> TempStatus {<span class="kwd">get; set;</span>}                            
                                    }

                                    <span class="kwd">pubic class</span> <span class="typ">FanexMatches</span>
                                    {
                                         <span class="kwd">pubic int</span> MatchID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> MatcheName {<span class="kwd">get; set;</span>}                             
                                    }

                                    <span class="kwd">pubic class</span> <span class="typ">FanexStatus</span>
                                    {
                                         <span class="kwd">pubic int</span> StatusID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> StatusName {<span class="kwd">get; set;</span>}                             
                                    }
                                </pre>
                            <p>
                                <h3>
                                    Using Fanex mapper is easier for mapping data</h3>
                            </p>
                            <p>
                                The splitOn param needs to be specified as the split point, it defaults to Id. If
                                there are multiple split points, you will need to add them in a comma delimited
                                list.
                            </p>
                            <p>
                                Return recordset look like
                            </p>
                            <pre class="prettyprint">EventID    |EventName  |MatchID |MatcheName |StatusID  |StatusName</pre>
                            <p>
                                Fanex Data needs to know how to split the columns in this order into 2 objects.
                                A cursory look shows that the FanexMatches starts at the column MatchID, hence splitOn:
                                MatchID. And the FanexStatus start at the column StatusID,hence splitOn : StatusID
                            </p>
                            <p>
                                If recordset return like
                            </p>
                            <pre class="prettyprint">EventID    |EventName  |MatcheName  |StatusName |MatchID  |StatusID</pre>
                            <p>
                                splitOn : MathID and StatusID will return a result in a null for MatchName,StatusName
                            </p>
                            <p>
                                If you specify all columns of objects in splitOn,Fanex Data will try return value
                                for some loop. splitOn:MatchID,MatchName,StatusID,StatusName
                            </p>
                            <p>
                                Execute a query and map the results to a strongly typed List</p>
                            <pre class="prettyprint">

<span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
{
    IEnumerable< <span class="kwd">FanexEvents</span>> listEvents = db.Query<<span class="kwd">FanexEvents,FanexMatches,FanexStatus,FanexEvents</span>>((e,m,s)=>{e.TempMatch=m;e.TempStatus=s;return e;},"MatchID,StatusID");
}
                                    </pre>
                            <p>
                                Execute a query and map the results to an objects</p>
                            <pre class="prettyprint">

<span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
{
     <span class="kwd">FanexEvents</span> events = db.QueryEntity<<span class="kwd">FanexEvents,FanexMatches,FanexStatus,FanexEvents</span>>((e,m,s)=>{e.TempMatch=m;e.TempStatus=s;return e;},"MatchID,StatusID");
}
                                    </pre>
                            <h3>
                                Data object class</h3>
                            <pre class="prettyprint">
                                    <span class="kwd">pubic class</span> <span class="typ">FanexEvents</span>
                                    {
                                         <span class="kwd">pubic int</span> EventID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> EventName {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic FanexMatches</span> TempMatch {<span class="kwd">get; set;</span>}                             
                                    }
                                    <span class="kwd">pubic class</span> <span class="typ">FanexMatches</span>
                                    {
                                         <span class="kwd">pubic int</span> MatchID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> MatcheName {<span class="kwd">get; set;</span>}    
                                         <span class="kwd">pubic FanexStatus</span> TempStatus {<span class="kwd">get; set;</span>}                             
                                    }

                                    <span class="kwd">pubic class</span> <span class="typ">FanexStatus</span>
                                    {
                                         <span class="kwd">pubic int</span> StatusID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> StatusName {<span class="kwd">get; set;</span>}                             
                                    }
                                </pre>
                            <p>
                                Execute a query and map the results to a strongly typed List</p>
                            <pre class="prettyprint">

<span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
{
    IEnumerable< <span class="kwd">FanexEvents</span>> listEvents = db.Query<<span class="kwd">FanexEvents,FanexMatches,FanexStatus,FanexEvents</span>>((e,m,s)=>{e.TempMatch=m;m.TempStatus=s;return e;},"MatchID,StatusID");
}
                                    </pre>
                        </div>
                        <div id="multiMapperYes">
                            <h1>
                                <a>Fanex - Multi object mapper</a></h1>
                            <h3>
                                <a>Tables</a></h3>
                            <pre class="prettyprint">
                                   CREATE TABLE Events(
	                                    EventID int PRIMARY KEY,
	                                    EventName nvarchar(100) NULL,
	                                    Player nvarchar(100) NULL,
	                                    ServerTime nvarchar(100) NULL,
	                                    MatchID int NULL,
	                                    CreateDate datetime NULL
                                    )

                                    CREATE TABLE Matches(
	                                    MatchID int PRIMARY KEY,
	                                    MatcheName nvarchar(100) NULL,
	                                    StatusID varchar(2) NULL
                                    )

                                    CREATE TABLE Status(
	                                    StatusID varchar(2) PRIMARY KEY,
	                                    StatusName nvarchar(100) NULL	                                    
                                    )

                                </pre>
                            <h3>
                                Store procedure</h3>
                            <pre class="prettyprint">
    <span class="kwd">CREATE PROCEDURE</span> Events_Sel(@eventid INT)
    <span class="kwd">AS BEGIN</span>
    <span class="str"> SELECT Events.EventID,Events.EventName ,Matches.MatcheID,Matches.MatchName,Status.StatusID,Status.StatusName
        FROM Events INNER JOIN Matches On Events.EventID=Matches.MatchID INNER JOIN Status
        On Matches.StatusID=Status.StausID WHERE Events.EventID=@eventid;</span>
    <span class="kwd">END</span>
                                </pre>
                            <h3>
                                Data object class</h3>
                            <pre class="prettyprint">
                                    <span class="kwd">pubic class</span> <span class="typ">FanexEvents</span>
                                    {
                                         <span class="kwd">pubic int</span> EventID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> EventName {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic FanexMatches</span> TempMatch {<span class="kwd">get; set;</span>}      
                                         <span class="kwd">pubic FanexStatus</span> TempStatus {<span class="kwd">get; set;</span>}                            
                                    }

                                    <span class="kwd">pubic class</span> <span class="typ">FanexMatches</span>
                                    {
                                         <span class="kwd">pubic int</span> MatchID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> MatcheName {<span class="kwd">get; set;</span>}                             
                                    }

                                    <span class="kwd">pubic class</span> <span class="typ">FanexStatus</span>
                                    {
                                         <span class="kwd">pubic int</span> StatusID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> StatusName {<span class="kwd">get; set;</span>}                             
                                    }
                                </pre>
                            <p>
                                <h3>
                                    Using Fanex mapper is easier for mapping data</h3>
                            </p>
                            <p>
                                Execute a query and map the results to a strongly typed List</p>
                            <pre class="prettyprint">

<span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
{
    IEnumerable< <span class="kwd">FanexEvents</span>> listEvents = db.Query<<span class="kwd">FanexEvents,FanexMatches,FanexStatus,FanexEvents</span>>((e,m,s)=>{e.TempMatch=m;e.TempStatus=s;return e;},"MatchID,StatusID",1);
}
                                    </pre>
                            <pre class="prettyprint">

<span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
{
    db.AddInParameter("eventid", DbType.Int32, 1);
    IEnumerable< <span class="kwd">FanexEvents</span>> listEvents = db.Query<<span class="kwd">FanexEvents,FanexMatches,FanexStatus,FanexEvents</span>>((e,m,s)=>{e.TempMatch=m;e.TempStatus=s;return e;},"MatchID,StatusID");
}
                                    </pre>
                            <p>
                                Execute a query and map the results to an objects</p>
                            <pre class="prettyprint">

<span class="kwd">using</span> (ObjectDb db = new ObjectDb(<span class="typ">"Events_Sel"</span>))
{
     <span class="kwd">FanexEvents</span> events = db.QueryEntity<<span class="kwd">FanexEvents,FanexMatches,FanexStatus,FanexEvents</span>>((e,m,s)=>{e.TempMatch=m;e.TempStatus=s;return e;},"MatchID,StatusID",1);
}
                                    </pre>
                        </div>
                        <div id="mappingColumn" runat="server">
                            <p>
                                Return recordset look like
                            </p>
                            <pre class="prettyprint">EventID    |EventName  |MatchID |MatcheName |StatusID  |StatusName</pre>
                            <%--<p>
                                Default object class</p>
                            <pre class="prettyprint">
                                     <span class="kwd">pubic class</span> <span class="typ">FanexEvents</span>
                                    {
                                         <span class="kwd">pubic int</span> EventID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> EventName {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic int</span> MatchID {<span class="kwd">get; set;</span>}      
                                         <span class="kwd">pubic string</span> MatcheName {<span class="kwd">get; set;</span>}                            
                                         <span class="kwd">pubic int</span> StatusID {<span class="kwd">get; set;</span>}                          
                                         <span class="kwd">pubic string</span> StatusName {<span class="kwd">get; set;</span>}                            
                                    }
                                </pre>--%>
                            <%--<p>
                                With file sp xml is contains sp name
                            </p>
                            <pre class="prettyprint">
<span class="typ">< setting key="<span class="kwd">Events_Sel</span>" spname="<span
    class="kwd">Events_Sel</span>" dbname="<span class="kwd">RTSportcast</span>" plain="<span
        class="kwd">1</span>"/> 
</pre>
                          <p>
                                If you want change properties name differen recordset return, you must to change
                                file SpName xml
                            </p>
                            <pre class="prettyprint">
<span class="typ">< setting key="<span class="kwd">Events_Sel</span>" spname="<span
    class="kwd">Events_Sel</span>" dbname="<span class="kwd">RTSportcast</span>" plain="<span
        class="kwd">1</span>"> < Column SpColumn="<span class="kwd">EventName</span>"
    ClassColumn="<span class="kwd">EventNameX</span>" /> < Column SpColumn="<span class="kwd">MatcheName</span>"
    ClassColumn="<span class="kwd">MatcheNameX</span>" /> < /setting></span>
</pre>--%>
                            <p>
                                We can map with calss object</p>
                            <pre class="prettyprint">
                                     <span class="kwd">pubic class</span> <span class="typ">FanexEvents</span>
                                    {
                                         <span class="kwd">pubic int</span> EventID {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic string</span> EventNameX {<span class="kwd">get; set;</span>}
                                         <span class="kwd">pubic int</span> MatchID {<span class="kwd">get; set;</span>}      
                                         <span class="kwd">pubic string</span> MatcheNameX {<span class="kwd">get; set;</span>}                            
                                         <span class="kwd">pubic int</span> StatusID {<span class="kwd">get; set;</span>}                          
                                         <span class="kwd">pubic string</span> StatusName {<span class="kwd">get; set;</span>}                            
                                    }
                                </pre>
                            <pre class="prettyprint">
using (ObjectDb db = new ObjectDb("Event_Sel"))
{
    var functions = db.Query< FanexEvents>(new Hashtable{ {"EventName", "EventNameX"}, {"MatcheName", "MatcheNameX"} });
}
                                 </pre>
                            <%--  <asp:Chart ID="Chart1" runat="server" Palette="BrightPastel" BackColor="#F3DFC1"
                                ImageType="Png" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="1100px"
                                Height="500px" BorderDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2"
                                BorderColor="181, 64, 1">
                                <Legends>
                                    <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent"
                                        Font="Trebuchet MS, 8.25pt, style=Bold">
                                    </asp:Legend>
                                </Legends>
                                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                <Series>
                                    <asp:Series MarkerSize="8" BorderWidth="3" XValueType="Double" Name="AutoMap" ChartType="Line"
                                        MarkerStyle="Circle" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240"
                                        ShadowOffset="2" YValueType="Double">
                                    </asp:Series>
                                    <asp:Series MarkerSize="9" BorderWidth="3" XValueType="Double" Name="EntlibReader"
                                        ChartType="Line" MarkerStyle="Diamond" ShadowColor="Black" BorderColor="180, 26, 59, 105"
                                        Color="220, 224, 64, 10" ShadowOffset="2" YValueType="Double">
                                    </asp:Series>
                                    <asp:Series MarkerSize="9" BorderWidth="3" XValueType="Double" Name="ADOReader" ChartType="Line"
                                        MarkerStyle="Diamond" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="222, 225, 60, 240"
                                        ShadowOffset="2" YValueType="Double">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                        BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                        <Area3DStyle Rotation="25" Perspective="9" LightStyle="Realistic" Inclination="40"
                                            IsRightAngleAxes="False" WallWidth="3" IsClustered="False" />
                                        <AxisY LineColor="64, 64, 64, 64">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisY>
                                        <AxisX LineColor="64, 64, 64, 64">
                                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                                            <MajorGrid LineColor="64, 64, 64, 64" />
                                        </AxisX>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                            <asp:Button ID="btnLoadNew" runat="server" Text="Re Statistics" OnClick="btnLoadNew_Click" />--%>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
