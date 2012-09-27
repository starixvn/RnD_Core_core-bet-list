using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Data;
using Fanex.Data.Dapper;
using Fanex.Data.DNAide;
using System.Data;
public partial class Execute_Without_Param : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingKey = Request.Form["key"];
        BindSource(settingKey);
    }

    protected void BindSource(string settingKey)
    {
        lbError.Text = string.Empty;
        BindDataSource(settingKey);
    }

    private void BindDataSource(string settingKey)
    {
        try
        {
            DataSet ds = DbHelper.ExecuteDataSet(settingKey);
            gvSource.DataSource = ds.Tables[0];
            gvSource.DataBind();
        }
        catch (Exception e)
        {
            lbError.Text = e.Message;
        }
    }
}