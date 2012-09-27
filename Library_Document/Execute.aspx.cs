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
        BindGridView(settingKey);
    }

    protected void BindGridView(string settingKey)
    {
        lbError.Text = string.Empty;
        gvSource.DataSource = BindDataSource(settingKey);
        gvSource.DataBind();
    }

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
    }
}