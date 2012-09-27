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
        LoadSource(settingKey);
    }

    protected void LoadSource(string settingKey)
    {
        lbError.Text = string.Empty;
        lbSource.Text = BindDataSource(settingKey).ToString();
    }

    private int BindDataSource(string settingKey)
    {
        try
        {
            int id = (int)DbHelper.ExecuteScalar(settingKey);
            return id;
        }
        catch (Exception e)
        {
            lbError.Text = e.Message;
            return -1;
        }
    }
}