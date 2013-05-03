<%@ Application Language="C#" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Sunplus.Agent.Utilities" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        Sunplus.Agent.Configurations.AppSettings.LoadComplete += new Sunplus.Agent.Configurations.AppSettings.UpdateEventHandler(AppSettings_LoadComplete);
        Sunplus.Agent.Configurations.AppSettings.Configuration.Load();
        // Code that runs on application startup
        Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
    }

    void AppSettings_LoadComplete(object sender, bool error)
    {
        if (error) return;
        
        string[] keys = Sunplus.Agent.Configurations.AppSettings.Configuration.GetKeys();
        foreach (string key in keys)
        {
            ConfigurationManager.AppSettings[key] = Sunplus.Agent.Configurations.AppSettings.Configuration.Get(key);
        }

        TplCore.TplSettings.UrlVersion = Sunplus.Agent.Configurations.AppSettings.Configuration.Get("JsVersion");
    }
    
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Exception ex = Server.GetLastError().GetBaseException();
        if (ex is HttpException)
        {
            return;
        }

        string message = ex.Message +
                        "\r\nSOURCE: " + ex.Source +
                        "\r\nDOMAIN: " + HttpContext.Current.Request.Headers["Host"] +
                        "\r\nFORM: " + Request.Form.ToString() +
                        "\r\nQUERYSTRING: " + Request.QueryString.ToString() +
                        "\r\nTARGETSITE: " + ex.TargetSite +
                        "\r\nSTACKTRACE: " + ex.StackTrace;
        Logs.Write(message);
    }



    void Application_AcquireRequestState(object sender, EventArgs e)
    {
        if (!(HttpContext.Current.Handler is IRequiresSessionState)) return;

        // Validate request & session
        string executeFileName = Request.AppRelativeCurrentExecutionFilePath;
        bool isNewSession = executeFileName.EndsWith("SignIn.aspx")
            || executeFileName.EndsWith("ProcessAdminLogin.ashx")
            || executeFileName.EndsWith("ProcessAfterLogin.ashx")
            || executeFileName.EndsWith("Captcha.ashx")
            || executeFileName.EndsWith("ChartImg.axd");
            ;     

        // Validate url and request parameters
        int custId = 0; long transId = 0;
        int masterId = 0;

        bool isDataWarehouse;
        string url;
        GetRequestParams(ref custId, ref transId, ref masterId, out isDataWarehouse, out url);     

        if (Request.QueryString["custids"] != null)
        {
            var items = Request.QueryString["custids"].Split('^');
            var custids = string.Empty;
            
            foreach (var custid in items)
            {
                if (custid != string.Empty)
                {
                    var ret = 0; int.TryParse(custid, out ret);
                    custids += ret.ToString() + "^";
                }
            }
        }       
    }

    private void GetRequestParams(ref int custId, ref long transId, ref int masterId, out bool isDataWarehouse, out string url)
    {
        int.TryParse(Request["custid"], out custId);
        if (custId == 0) int.TryParse(Request["cid"], out custId);
        if (custId == 0) int.TryParse(Request["recommend"], out custId);
        if (custId == 0) int.TryParse(Request["mrecommend"], out custId);
        if (custId == 0) int.TryParse(Request["idagent"], out custId);
        long.TryParse(Request["transid"], out transId);

        if (transId == 0) long.TryParse(Request["refno"], out transId);
        int.TryParse(Request["idmaster"], out masterId);

        isDataWarehouse = (Request["iswhcheck"] == "1");

        // Get file path (remove /)
        url = Request.Path.Remove(0, 1);
        url = url.ToLower();
    }



    void Session_Start(object sender, EventArgs e)
    {

    }

    void Session_End(object sender, EventArgs e)
    {

    }


</script>

