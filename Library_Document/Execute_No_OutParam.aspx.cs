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
using System.Data.SqlClient;
using System.Collections;
public partial class Execute_InputParam_Without_OutputParam : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        string settingKey = txtSettingKey.Text;
        lbError.Text = string.Empty;
        gvSource.DataSource = BindDataSource(settingKey);
        gvSource.DataBind();
    }

    private DataTable BindDataSource(string settingKey)
    {
        try
        {
            Hashtable hstable = new Hashtable();
            hstable.Add("groupid", int.Parse(txtGroupID.Text));
            DbResult dbResult = DbHelper.Execute(settingKey, hstable);
            return dbResult.ResultSet.Tables[0];
        }
        catch (Exception e)
        {
            lbError.Text = e.Message;
            return null;
        }
    }

   
}