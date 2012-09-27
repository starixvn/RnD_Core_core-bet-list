<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExecuteDefault.aspx.cs" Inherits="_Default" %>

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
    <script type="text/javascript">
        $(document).ready(function () {
            HideAll();
        });

        function HideAll() {

            $("#execute").hide();
            $("#executeparam").hide();
            $("#executeReader").hide();
            $("#executeReaderParam").hide();
            $("#executeNonquery").hide();
            $("#executeScalar").hide();
            $("#executeCode").hide();
            $("#executeparamCode").hide();
            $("#executeReaderCode").hide();
            $("#executeReaderParamCode").hide();
            $("#executeNonqueryCode").hide();
            $("#executeScalarCode").hide();
            $("#executeScalarNonParam").hide();
            $("#executeDataTable").hide();
            $("#executeparamTable").hide();
            $("#executeQuery").hide();
            $("#executeDataTableParam").hide();
            $("#executeDataSet").hide();
            $("#executeDataSetParam").hide();

        }

        function LoadExample() {
            HideAll();
            var executeType = $("#ddlExecuteType").val();
            var paramType = $("#ddlParamType").val();

            if (executeType == 1) {
                if (paramType == 1) {
                    $("#execute").show();
                }
                if (paramType == 2) {
                    $("#executeparam").show();
                }
            }

            if (executeType == 2) {
                if (paramType == 1) {
                    $("#executeReader").show();
                }
                if (paramType == 2) {
                    $("#executeReaderParam").show();
                }
            }

            if (executeType == 3) {
                $("#executeNonquery").show();
            }

            if (executeType == 4) {
                if (paramType == 1) {
                    $("#executeScalarNonParam").show();
                }
                if (paramType == 2) {
                    $("#executeScalar").show();
                }
            }

            if (executeType == 5) {
                if (paramType == 1) {
                    $("#executeDataTable").show();
                }
                if (paramType == 2) {
                    $("#executeDataTableParam").show();
                }
            }

            if (executeType == 6) {
                if (paramType == 1) {
                    $("#executeDataSet").show();
                }
                if (paramType == 2) {
                    $("#executeDataSetParam").show();
                }
            }

            if (executeType == 7) {
                //if (paramType == 1) {
                $("#executeQuery").show();
                //}
            }
        }





        function BindExampleExecute() {
            var settingKey = $("#settingKeyExecute").val();

            $.ajax({
                type: "POST",
                url: "Execute.aspx",
                data: { key: settingKey },
                success: function (html) {
                    $("#executeContent").html(html);
                }
            });
        }

        function BindExampleExecuteParam() {
            var settingKey = $("#settingKeyExecuteParam").val();
            var cusid = $("#custid").val();
            var netsessionid = $("#netsessionid").val();
            var ipTmp = $("#ip").val();
            var onuser = $("#user").val();

            $.ajax({
                type: "POST",
                url: "Execute_OutParam.aspx",
                data: { key: settingKey, custid: cusid, net: netsessionid, ip: ipTmp, user: onuser },
                success: function (html) {
                    $("#executeparamContent").html(html);
                }
            });
        }

        function BindExampleExecuteReader() {
            var settingKey = $("#settingKeyReader").val();

            $.ajax({
                type: "POST",
                url: "ExecuteReader.aspx",
                data: { key: settingKey },
                success: function (html) {
                    $("#contentReader").html(html);
                }
            });
        }


        function BindExampleExecuteReaderParam() {
            var settingKey = $("#settingKeyReaderParam").val();
            var groupid = $("#groupID").val();

            $.ajax({
                type: "POST",
                url: "ExecuteReader_No_OutParam.aspx",
                data: { key: settingKey, group: groupid },
                success: function (html) {
                    $("#contentReaderParam").html(html);
                }
            });
        }


        function BindExampleExecuteNonquery() {
            var settingKey = $("#settingKeyNonquery").val();
            var lisID = $("#licenseeID").val();
            var lisName = $("#licenseeName").val();
            var groupid = $("#groupID").val();

            $.ajax({
                type: "POST",
                url: "ExecuteNonquery.aspx",
                data: { key: settingKey, id: lisID, name: lisName },
                success: function (html) {
                    $("#contentNonquery").html(html);
                }
            });
        }

        function BindExampleExecuteScalar() {
            var settingKey = $("#spScalar").val();
            var id = $("#groupID").val();

            $.ajax({
                type: "POST",
                url: "ExecuteScalar_No_OutParam.aspx",
                data: { key: settingKey, groupid: id },
                success: function (html) {
                    $("#contentScalar").html(html);
                }
            });
        }

        function BindExampleExecuteScalarNonParam() {
            var settingKey = $("#settingKeyScalarNonParam").val();

            $.ajax({
                type: "POST",
                url: "ExecuteScalar.aspx",
                data: { key: settingKey },
                success: function (html) {
                    $("#contentScalarNonParam").html(html);
                }
            });
        }

        function BindExampleExecuteTable() {
            var settingKey = $("#settingKeyExecuteTable").val();

            $.ajax({
                type: "POST",
                url: "ExecuteTable.aspx",
                data: { key: settingKey },
                success: function (html) {
                    $("#executeDataTableContent").html(html);
                }
            });
        }

        function BindExampleExecuteQuery() {
            var settingKey = $("#spExecuteQuery").val();

            $.ajax({
                type: "POST",
                url: "ExecuteQuery.aspx",
                data: { key: settingKey },
                success: function (html) {
                    $("#executeQueryContent").html(html);
                }
            });
        }

        function BindExampleExecuteDataTableParam() {
            var settingKey = $("#settingKeyDataTableParam").val();
            var groupid = $("#txtGroupIdTableParam").val();

            $.ajax({
                type: "POST",
                url: "ExecuteDataTableParam.aspx",
                data: { key: settingKey, group: groupid },
                success: function (html) {
                    $("#executeDataTableParamContent").html(html);
                }
            });
        }

        function BindExampleExecuteDataSet() {
            var settingKey = $("#settingKeyDataSet").val();

            $.ajax({
                type: "POST",
                url: "ExecuteDataSet.aspx",
                data: { key: settingKey },
                success: function (html) {
                    $("#executeDataSetContent").html(html);
                }
            });
        }


        function BindExampleExecuteDataSetParam() {
            var settingKey = $("#settingKeyDataSetParam").val();
            var id = $("#txtGroupIdDataSetParam").val();

            $.ajax({
                type: "POST",
                url: "ExecuteDataSetParam.aspx",
                data: { key: settingKey, group: id },
                success: function (html) {
                    $("#executeDataSetParamContent").html(html);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <select id="ddlExecuteType" style="width: 200px">
                <option value="1">Execute DbResult</option>
                <option value="2">Execute Reader</option>
                <option value="3">Execute Nonquery</option>
                <option value="4">Execute Scalar</option>
                <option value="5">Execute DataTable</option>
                <option value="6">Execute DataSet</option>
                <option value="7">Execute Query</option>
            </select>
            <select id="ddlParamType" style="width: 200px">
                <option value="1">Without Parameter</option>
                <option value="2">Has Input And Out Parameter</option>
            </select>
            <button type="button" id="btnDemo" onclick="LoadExample()">
                Example</button>
        </div>
        <div id="execute">
            <h1>
                Function</h1>
            <p>
                <code>public static DbResult Execute(string settingKey)</code>
            </p>
            <p>
                <code>public static DbResult Execute(string settingKey, int timeOut)</code>
            </p>
            <h1>
                --------</h1>
            <h2>
                New Library</h2>
            <p>
                <code>public static DbResult ExecuteDbResult(string settingKey)</code>
            </p>
            <p>
                <code>public static DbResult ExecuteDbResult(string settingKey, int timeOut)</code>
            </p>
            <p>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <%-- <input id="settingKeyExecute" value="OA_ResellersGet" />--%>
                <select id="settingKeyExecute">
                    <option value="OA_ResellersGet">OA_ResellersGet</option>
                    <%-- <option value="Acc_BA_LicConfigSel">Acc_BA_LicConfigSel</option>--%>
                </select>
                <button type="button" onclick="BindExampleExecute()">
                    Execute</button>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="executeContent">
                        </div>
                        <script type="text/javascript">
                            function ShowCode1() {
                                $("#executeCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCode1()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeCode" style="display: none">
                            <%--<pre>
                            //BindDataSource(OA_ResellersGet);
                            private DataTable BindDataSource(string settingKey)
                            { 
                                try 
                                { 
                                    //Older
                                    //DbResult dbResult = DbHelper.Execute(settingKey);
                                    DbResult dbResult = DbHelper.ExecuteDbResult(settingKey);
                                    return dbResult.ResultSet.Tables[0]; 
                                } 
                                catch (Exception e) 
                                { 
                                    lbError.Text = e.Message;
                                    return null; 
                                } 
                            } </pre>--%>
                            <pre>
                                   [Obsolete]
                                   public DataTable BindDataTable()
                                   {
                                       <code>DbResult dbResult = DbHelper.Execute(<settingkey>"OA_ResellersGet"</settingkey>);</code>

                                        if(dbResult!=null&&dbResult.Table.Count>0)
                                            return dbResult.ResultSet.Tables[0]; 
                                        return new DataTable();
                                   }
                            

                                   public DataTable BindDataTable()
                                   {
                                         <code>DbResult dbResult = DbHelper.ExecuteDbResult(<settingkey>"OA_ResellersGet"</settingkey>);</code>

                                         if(dbResult!=null&&dbResult.Table.Count>0)
                                            return dbResult.ResultSet.Tables[0]; 
                                        return new DataTable(); 
                                   }
                            </pre>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="executeparam">
            <div>
                <h1>
                    Function</h1>
                <p>
                    <code>public static DbResult Execute(string settingKey, Hashtable parameters)</code>
                </p>
                <p>
                    <code>public static DbResult Execute(string settingKey, Hashtable parameters, int timeOut)</code>
                </p>
                <h1>
                    --------</h1>
                <h2>
                    New Library</h2>
                <p>
                    <code>public static DbResult ExecuteDbResult(string settingKey, Hashtable parameters)</code>
                </p>
                <p>
                    <code>public static DbResult ExecuteDbResult(string settingKey, Hashtable parameters,
                        int timeOut)</code>
                </p>
                <h1>
                    Example</h1>
                <table>
                    <tr>
                        <td>
                            SP Name :
                            <input id="settingKeyExecuteParam" value="OA_UserCheckIn" />
                            <br />
                            <br />
                            Param Name
                            <table>
                                <tr>
                                    <td>
                                        CustID
                                    </td>
                                    <td>
                                        <input id="custid" value="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        NetSessionID
                                    </td>
                                    <td>
                                        <input id="netsessionid" value="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        IP
                                    </td>
                                    <td>
                                        <input id="ip" value="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        On_UserID
                                    </td>
                                    <td>
                                        <input id="user" value="1" />
                                    </td>
                                </tr>
                            </table>
                            <button type="button" onclick="BindExampleExecuteParam()">
                                Execute
                            </button>
                            <div id="executeparamContent">
                            </div>
                            <script type="text/javascript">
                                function ShowCode2() {
                                    $("#executeparamCode").show();
                                }
                            </script>
                            <a href="javascript:void(0);" onclick="ShowCode2()">Show Code</a>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <div id="executeparamCode" style="display: none">
                                            <%--   <pre>
                    private DataTable BindDataSource(string settingKey, string custid, string net, string ip,string user)
                    {
                        try
                        {
                            //Add param must be lower case
                            Hashtable hstable = new Hashtable();
                            hstable.Add("custid", int.Parse(custid));
                            hstable.Add("netsessionid", int.Parse(net));
                            hstable.Add("ip", int.Parse(ip));
                            hstable.Add("on_userid",int.Parse(user));
                            DbResult dbResult = DbHelper.Execute(settingKey, hstable);

                            //Older
                            //DbResult dbResult = DbHelper.ExecuteDbResult(settingKey, hstable);
                            

                            DataTable table = new DataTable();
                            table.Columns.Add("Param Name");
                            table.Columns.Add("Value");
                            foreach (DictionaryEntry item in dbResult.Outputs)
                            {
                                DataRow rw = table.NewRow();
                                rw[0] = item.Key.ToString();
                                rw[1] = item.Value.ToString();
                                table.Rows.Add(rw);
                            }
                            return table;
                        }
                        catch (Exception e)
                        {
                            lbError.Text = e.Message;
                            return null;
                        }
                   } </pre>--%>
                                            <pre>
                    [Obsolete]
                    public Hashtable BindDataOutParam(){
                            //Add param must be lower case
                            Hashtable hstable = new Hashtable();
                            hstable.Add("custid", int.Parse(custid));
                            hstable.Add("netsessionid", int.Parse(net));
                            hstable.Add("ip", int.Parse(ip));
                            hstable.Add("on_userid",int.Parse(user));

                            <code>DbResult dbResult = DbHelper.Execute(<settingkey>"OA_UserCheckIn"</settingkey>,hstable);</code>

                            if(dbResult.Outputs!=null)
                                return dbResult.Outputs;
                            return new HashTable();
                    }


                     public Hashtable BindDataOutParam(){
                            //Add param must be lower case
                            Hashtable hstable = new Hashtable();
                            hstable.Add("custid", int.Parse(custid));
                            hstable.Add("netsessionid", int.Parse(net));
                            hstable.Add("ip", int.Parse(ip));
                            hstable.Add("on_userid",int.Parse(user));

                            <code>DbResult dbResult = DbHelper.ExecuteDbResult(<settingkey>"OA_UserCheckIn"</settingkey>,hstable);</code>

                            if(dbResult.Outputs!=null)
                                return dbResult.Outputs;
                            return new HashTable();
                    }
                   </pre>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="content">
            </div>
        </div>
        <div id="executeReader">
            <h1>
                Function</h1>
            <p>
                <code>public static IDataReader ExecuteReader(string settingKey)</code></p>
            <p>
                <code>public static IDataReader ExecuteReader(string settingKey, int timeOut)</code></p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <input id="settingKeyReader" value="OA_ResellersGet" />
                <button type="button" onclick="BindExampleExecuteReader()">
                    Execute</button>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="contentReader">
                        </div>
                        <script type="text/javascript">
                            function ShowCode3() {
                                $("#executeReaderCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCode3()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeReaderCode" style="display: none">
                            <%-- <pre>
                private void BindDataSource(string settingKey)
                {
                    try
                    {
                        using (IDataReader reader = DbHelper.ExecuteReader(settingKey))
                        {
                            string strValue = "";
                            while (reader.Read())
                            {
                                strValue += "< div>";
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    strValue += reader[i].ToString() + "--";
                                }
                                strValue += "< /div>";
                            }

                            lbSource.Text = strValue;
                        }
                    }
                    catch (Exception e)
                    {
                        lbError.Text = e.Message;
                    }
                } </pre>--%>
                            <pre>
                    public void ReadDataReader(){
                        using (<code>IDataReader reader = DbHelper.ExecuteReader(<settingkey>"OA_ResellersGet"</settingkey>)</code>)
                        {
                            string strValue = "";
                            while (reader.Read())
                            {
                                strValue += "< div>";
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    strValue += reader[i].ToString() + "--";
                                }
                                strValue += "< /div>";
                            }

                            lbSource.Text = strValue;
                        }
                    }
                </pre>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="executeReaderParam">
            <h1>
                Function</h1>
            <p>
                <code>public static IDataReader ExecuteReader(string settingKey)</code>
            </p>
            <p>
                <code>public static IDataReader ExecuteReader(string settingKey, int timeOut)</code>
            </p>
            <h1>
                --------</h1>
            <h2>
                Note: Use when only have Input parameters</h2>
            <p>
                <code>public static IDataReader ExecuteReader(string settingKey,dynamic parameters)</code>
            </p>
            <p>
                <code>public static IDataReader ExecuteReader(string settingKey, int timeOut, dynamic
                    parameters)</code>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <input id="settingKeyReaderParam" value="OA_GroupResellersGetByGroupID" />
                <div>
                    Param Name</div>
                <div>
                    GroupID
                    <input id="groupID" value="1" />
                    <button type="button" onclick="BindExampleExecuteReaderParam()">
                        Execute</button></div>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="contentReaderParam">
                        </div>
                        <script type="text/javascript">
                            function ShowCode4() {
                                $("#executeReaderParamCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCode4()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeReaderParamCode" style="display: none">
                            <%-- <pre>
                 private void BindDataSource(string settingKey, int groupID)
                 {
                    try
                    {
                        //One way
                        Hashtable hsTable = new Hashtable();
                        hsTable.Add("groupid", groupID);
                        using( IDataReader reader = DbHelper.ExecuteReader(settingKey, hsTable))
                        {
                            //Other way
                            // using( IDataReader reader = DbHelper.ExecuteReader(settingKey, new object[] { groupID }))
                            //{
                            //Read reader
                            //}           
                            string strValue = "";
                            while (reader.Read())
                            {
                                strValue += "< div>";
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    strValue += reader[i].ToString() + "--";
                                }
                                strValue += "< /div>";
                            }

                            lbSource.Text = strValue;
                        }
                    }
                    catch (Exception e)
                    {
                        lbError.Text = e.Message;
                    }
                } </pre>--%>
                            <pre>
                    //One way
                    public void ReadDataReader(){

                        Hashtable hsTable = new Hashtable();
                        hsTable.Add("groupid", groupID);
                        using( <code>IDataReader reader = DbHelper.ExecuteReader(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,
                            hsTable)</code>)
                        {
                            string strValue = "";
                            while (reader.Read())
                            {
                                strValue += "< div>";
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    strValue += reader[i].ToString() + "--";
                                }
                                strValue += "< /div>";
                            }
                        }
                    }


                    //Other way 
                    public void ReadDataReader(int Id){
                      
                        using( <code>IDataReader reader = DbHelper.ExecuteReader(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,
                            new { groupID = Id})</code>)                       
                        {
                            string strValue = "";
                            while (reader.Read())
                            {
                                strValue += "< div>";
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    strValue += reader[i].ToString() + "--";
                                }
                                strValue += "< /div>";
                            }
                        }
                    }
                </pre>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="executeScalar">
            <h1>
                Function</h1>
            <p>
                <code>public static object ExecuteScalar(string settingKey, Hashtable parameters)</code>
            </p>
            <p>
                <code>public static object ExecuteScalar(string settingKey, Hashtable parameters, int
                    timeOut)</code>
            </p>
            <h1>
                --------</h1>
            <h2>
                Note: Use when only have Input parameters</h2>
            <p>
                <code>public static object ExecuteScalar(string settingKey, dynamic parameters)</code>
            </p>
            <p>
                <code>public static object ExecuteScalar(string settingKey, int timeOut, dynamic parameters)</code>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <input id="spScalar" value="OA_GroupResellersGetByGroupID" />
                <div>
                    Param Name
                </div>
                <div>
                    GroupID</div>
                <input id="groupIdScalar" value="1" />
                <button type="button" onclick="BindExampleExecuteScalar()">
                    Execute</button>
            </p>
            <div id="contentScalar">
            </div>
            <script type="text/javascript">
                function ShowCode5() {
                    $("#executeScalarCode").show();
                }
            </script>
            <a href="javascript:void(0);" onclick="ShowCode5()">Show Code</a>
            <div id="executeScalarCode" style="display: none">
                <%--<pre>
                private int BindDataSource(string settingKey,string groupID)
                {
                    try
                    {
                        //One ways
                        Hashtable hstable = new Hashtable();
                        hstable.Add("groupid", int.Parse(groupID));
                        int id = (int)DbHelper.ExecuteScalar(settingKey, hstable);

                        //Other way
                        //int id = (int)DbHelper.ExecuteScalar(settingKey, groupID );
                        return id;
                    }
                    catch (Exception e)
                    {
                        lbError.Text = e.Message;
                        return -1;
                    }
                } </pre>
                --%>
                <pre>
                //One ways
                public int BindDataSource(){
                    Hashtable hstable = new Hashtable();
                    hstable.Add("groupid", int.Parse(groupID));
                    int id = (int)<code>DbHelper.ExecuteScalar(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,
                        hstable);</code>
                    return id;
                }

                //Other way
                public int BindDataSource(int id){                   
                    int id = (int)<code>DbHelper.ExecuteScalar(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,
                        new { groupID =id});</code>
                    return id;
                }
            </pre>
            </div>
        </div>
        <div id="executeScalarNonParam">
            <h1>
                Function</h1>
            <p>
                <code>public static object ExecuteScalar(string settingKey)</code>
            </p>
            <p>
                <code>public static object ExecuteScalar(string settingKey, int timeOut)</code>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <input value="OA_ResellersGet" id="settingKeyScalarNonParam" />
                <button type="button" onclick="BindExampleExecuteScalarNonParam()">
                    Execute
                </button>
            </p>
            <div id="contentScalarNonParam">
            </div>
            <script type="text/javascript">
                function ShowCode8() {
                    $("#executeScalarNonParamCode").show();
                }
            </script>
            <a href="javascript:void(0);" onclick="ShowCode8()">Show Code</a>
            <div id="executeScalarNonParamCode" style="display: none">
                <pre>
                private int BindDataSource()
                {
                    int id = (int)<code>DbHelper.ExecuteScalar(<settingkey>"OA_ResellersGet"</settingkey>);</code>
                    return id;
                } </pre>
            </div>
        </div>
        <div id="executeNonquery">
            <h1>
                Function</h1>
            <p>
                <code>public static int ExecuteNonQuery(string settingKey)</code>
            </p>
            <p>
                <code>public static int ExecuteNonQuery(string settingKey, int timeOut)</code>
            </p>
            <h1>
                --------</h1>
            <h2>
                Note: Use when only have Input parameters</h2>
            <p>
                <code>public static int ExecuteNonQuery(string settingKey, dynamic parameters)</code>
            </p>
            <p>
                <code>public static int ExecuteNonQuery(string settingKey, int timeOut, dynamic parameters)</code>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <input id="settingKeyNonquery" value="Update_Licensees_TestTin" />
                <div>
                    Param Name</div>
                <table>
                    <tr>
                        <td>
                            LicenseeID
                        </td>
                        <td>
                            <input id="licenseeID" value="5" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            LicenseeName
                        </td>
                        <td>
                            <input id="licenseeName" value="Sample Update" />
                        </td>
                    </tr>
                </table>
                <button type="button" onclick="BindExampleExecuteNonquery()">
                    Execute</button>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="contentNonquery">
                        </div>
                        <script type="text/javascript">
                            function ShowCode7() {
                                $("#executeNonqueryCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCode7()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeNonqueryCode" style="display: none">
                            <%--                    <pre>
private int BindDataSource(string settingKey, string id, string name)
{
try
{
    int rowAffect=-1;

    //One way
    Hashtable hstable = new Hashtable();
    hstable.Add("licenseesid", int.Parse(id));
    hstable.Add("licenseesname", name);
    rowAffect = (int)DbHelper.ExecuteNonQuery(settingKey, hstable);

    //Other way
    //rowAffect=(int)DbHelper.ExecuteNonQuery(settingKey,new object[]{id,name });

    return rowAffect;
}
catch (Exception e)
{
    lbError.Text = e.Message;
    return -1;
}
} </pre>--%>
                            <pre>
//One way
public int BindDataSource(){
    int rowAffect=-1;
    Hashtable hstable = new Hashtable();
    hstable.Add("licenseesid", int.Parse(id));
    hstable.Add("licenseesname", name);
    rowAffect = (int)<code>DbHelper.ExecuteNonQuery(<settingkey>"Update_Licensees_TestTin"</settingkey>,
        hstable);</code>
    return rowAffect;
}

//Other way
public int BindDataSource(){
    int rowAffect=-1;
    rowAffect = (int)<code>DbHelper.ExecuteNonQuery(<settingkey>"Update_Licensees_TestTin"</settingkey>,new
        {licenseesid=id,licenseesname=name });</code>
    return rowAffect;
}

</pre>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="executeDataTable">
            <h1>
                Function</h1>
            <p>
                <code>public static DataTable ExecuteDataTable(string settingKey)</code>
            </p>
            <p>
                <code>public static DataTable ExecuteDataTable(string settingKey, int timeOut)</code>
            </p>
            <p>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <%-- <input id="settingKeyExecute" value="OA_ResellersGet" />--%>
                <select id="settingKeyExecuteTable">
                    <option value="OA_ResellersGet">OA_ResellersGet</option>
                    <%-- <option value="Acc_BA_LicConfigSel">Acc_BA_LicConfigSel</option>--%>
                </select>
                <button type="button" onclick="BindExampleExecuteTable()">
                    Execute</button>
            </p>
            <div id="executeDataTableContent">
            </div>
            <script type="text/javascript">
                function ShowCodeTable() {
                    $("#executeDataTableCode").show();
                }
            </script>
            <a href="javascript:void(0);" onclick="ShowCodeTable()">Show Code</a>
            <div id="executeDataTableCode" style="display: none">
                <pre>
              private DataTable BindDataSource()
              {
                    DataTable table = DbHelper.ExecuteDataTable(
<settingkey>"OA_ResellersGet"</settingkey>
                ); return table; }
            </div>
        </div>
        <div id="executeDataTableParam">
            <h1>
                Function</h1>
            <p>
                <code>public static DataTable ExecuteDataTable(string settingKey, Hashtable parameters)</code>
            </p>
            <p>
                <code>public static DataTable ExecuteDataTable(string settingKey, Hashtable parameters,
                    int timeOut)</code>
            </p>
            <h1>
                --------</h1>
            <h2>
                Note: Use when only have Input parameters</h2>
            <p>
                <code>public static DataTable ExecuteDataTable(string settingKey, dynamic parameters)</code>
            </p>
            <p>
                <code>public static DataTable ExecuteDataTable(string settingKey, int timeOut, dynamic
                    parameters)</code>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <input id="settingKeyDataTableParam" value="OA_GroupResellersGetByGroupID" />
                <div>
                    Param Name</div>
                <div>
                    GroupID
                    <input id="txtGroupIdTableParam" value="1" />
                    <button type="button" onclick="BindExampleExecuteDataTableParam()">
                        Execute</button></div>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="executeDataTableParamContent">
                        </div>
                        <script type="text/javascript">
                            function ShowCodeDataTableParam() {
                                $("#executeDataTableParamCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCodeDataTableParam()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeDataTableParamCode" style="display: none">
                            <%--  <pre>
                     private void BindDataSource(string settingKey, int groupID)
                     {
                        try
                        {
                            //One way
                            Hashtable hsTable = new Hashtable();
                            hsTable.Add("groupid", groupID);
                            DataTable table = DbHelper.ExecuteDataTable(settingKey, hsTable);

                            //Onther way
                            //DataTable table = DbHelper.ExecuteDataTable(settingKey, new object[] { groupID });
                            gvSource.DataSource = table;
                            gvSource.DataBind();
                        }
                        catch (Exception e)
                        {
                            lbError.Text = e.Message;
                        }
                     }</pre>--%>
                            <pre>
                     //One way
                     public DataTable BindDataSource(){
                            Hashtable hsTable = new Hashtable();
                            hsTable.Add("groupid", groupID);
                            DataTable table = <code>DbHelper.ExecuteDataTable(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,
                                hsTable);</code>
                            return table;
                     }

                     //Onther way
                     public DataTable BindDataSource(int id){
                           DataTable table = <code>DbHelper.ExecuteDataTable(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,
                               new { groupID =id});</code>
                            return table;
                     }
                     </pre>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="executeQuery">
            <h1>
                Function</h1>
            <p>
                <code>public static IEnumerable< T> Query< T>(string settingKey)</code>
            </p>
            <p>
                <code>public static IEnumerable< T> Query< T>(string settingKey, int timeOut)</code>
            </p>
            <h2>
                ----------</h2>
            <p>
                <code>public static T QueryEntity< T>(string settingKey)</code>
            </p>
            <p>
                <code>public static T QueryEntity< T>(string settingKey, int timeOut)</code>
            </p>
            <p>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <%-- <input id="settingKeyExecute" value="OA_ResellersGet" />--%>
                <select id="spExecuteQuery">
                    <option value="GetAllLicenseesTestTin">GetAllLicenseesTestTin</option>
                </select>
                <button type="button" onclick="BindExampleExecuteQuery()">
                    Execute</button>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="executeQueryContent">
                        </div>
                        <script type="text/javascript">
                            function ShowCodeQuery() {
                                $("#executeQueryCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCodeQuery()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeQueryCode" style="display: none">
                            <pre>
    //Get IEnumerable< Licensees>

        private void GetAllLicensee()
        {
        IEnumerable< Licensees> licensee = <code>DbHelper.Query< Licensees>(<settingkey>"GetAllLicenseesTestTin"</settingkey>);</code>
        gvAllLicensee.DataSource = licensee;
        gvAllLicensee.DataBind();
        }
                

    //Get Entity Licensee

        private void GetLicensee(string settingKey)
        {
        Licensees licensee = <code>DbHelper.QueryEntity< Licensees>(<settingkey>"GetAllLicenseesTestTin"</settingkey>);</code>

        lbLicenseeID.Text = licensee.LicenseeID.ToString();
        lbLicenseeName.Text = licensee.licenseename;
        lbIsDeposit.Text = licensee.IsDesposit.ToString();
        LbDescription.Text = licensee.Description;

        }
</pre>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="executeDataSet">
            <h1>
                Function</h1>
            <p>
                <code>public static DataSet ExecuteDataSet(string settingKey)</code>
            </p>
            <p>
                <code>public static DataSet ExecuteDataSet(string settingKey, int timeOut)</code>
            </p>
            <p>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <%-- <input id="settingKeyExecute" value="OA_ResellersGet" />--%>
                <select id="settingKeyDataSet">
                    <option value="OA_ResellersGet">OA_ResellersGet</option>
                    <%-- <option value="Acc_BA_LicConfigSel">Acc_BA_LicConfigSel</option>--%>
                </select>
                <button type="button" onclick="BindExampleExecuteDataSet()">
                    Execute</button>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="executeDataSetContent">
                        </div>
                        <script type="text/javascript">
                            function ShowCodeDataSet() {
                                $("#executeDataSetCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCodeDataSet()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeDataSetCode" style="display: none">
                            <pre>
                            private DataSet BindDataSource()
                            {
                                    DataSet ds = <code>DbHelper.ExecuteDataSet(<settingkey>"OA_ResellersGet"</settingkey>);</code>
                        return ds; }
                    </td>
                </tr>
            </table>
        </div>
        <div id="executeDataSetParam">
            <h1>
                Function</h1>
            <p>
                <code>public static DataSet ExecuteDataSet(string settingKey, Hashtable parameters)</code>
            </p>
            <p>
                <code>public static DataSet ExecuteDataSet(string settingKey, Hashtable parameters,
                    int timeOut)</code>
            </p>
            <h1>
                --------</h1>
            <h2>
                Note: Use when only have Input parameters</h2>
            <p>
                <code>public static DataSet ExecuteDataSet(string settingKey, dynamic parameters)</code>
            </p>
            <p>
                <code>public static DataSet ExecuteDataSet(string settingKey, int timeOut, dynamic parameters)</code>
            </p>
            <h1>
                Example</h1>
            <p>
                SP Name :
                <input id="settingKeyDataSetParam" value="OA_GroupResellersGetByGroupID" />
                <div>
                    Param Name
                </div>
                <div>
                    GroupID</div>
                <input id="txtGroupIdDataSetParam" value="1" />
                <button type="button" onclick="BindExampleExecuteDataSetParam()">
                    Execute</button>
            </p>
            <table>
                <tr>
                    <td>
                        <div id="executeDataSetParamContent">
                        </div>
                        <script type="text/javascript">
                            function ShowCodeDataSetParam() {
                                $("#executeDataSetParamCode").show();
                            }
                        </script>
                        <a href="javascript:void(0);" onclick="ShowCodeDataSetParam()">Show Code</a>
                    </td>
                    <td>
                        <div id="executeDataSetParamCode" style="display: none">
                            <%--<pre>
                private void BindDataSource(string settingKey, int groupID)
                {
                    try
                    {
                        DataSet ds = new DataSet();
                        //One way
                        Hashtable hsTable = new Hashtable();
                        hsTable.Add("groupid", groupID);
                        ds = DbHelper.ExecuteDataSet(settingKey, hsTable);

                        //Other way
                        //ds = DbHelper.ExecuteDataSet(settingKey, new object[] { groupID});

                        gvSource.DataSource = ds.Tables[0];
                        gvSource.DataBind();
                    }
                    catch (Exception e)
                    {
                        lbError.Text = e.Message;
                    }
                } </pre>--%>
                            <pre>
                    //One way
                    public DataSet BindDataSource(){
                        Hashtable hsTable = new Hashtable();
                        hsTable.Add("groupid", groupID);
                        DataSet ds = <code>DbHelper.ExecuteDataSet(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,
                            hsTable);</code>
                        return ds;
                    }

                    //Other way
                    public DataSet BindDataSource(int id){
                        DataSet ds = <code>DbHelper.ExecuteDataSet(<settingkey>"OA_GroupResellersGetByGroupID"</settingkey>,new
                            { groupID =id});</code>                        
                        return ds;
                    }


                </pre>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
