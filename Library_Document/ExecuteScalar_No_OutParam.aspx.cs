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
        string settingKey = Request.Form["key"];
        string groupID = Request.Form["groupid"];
        LoadData(settingKey, groupID);

    }

    protected void LoadData(string settingKey, string groupID)
    {
        lbError.Text = string.Empty;
        lbSource.Text = BindDataSource(settingKey, groupID).ToString();
    }

    private int BindDataSource(string settingKey, string groupID)
    {
        try
        {
            //One way
            //Hashtable hstable = new Hashtable();
            //hstable.Add("groupid", int.Parse(groupID));
            //int id = (int)DbHelper.ExecuteScalar(settingKey, hstable);

            //Other way
            int id = (int)DbHelper.ExecuteScalar(settingKey, new { groupid = groupID });
            return id;
        }
        catch (Exception e)
        {
            lbError.Text = e.Message;
            return -1;
        }
    }
    private DataTable LoadData(string settingKey)
    {
        try
        {
            DbResult dbResult = DbHelper.ExecuteDbResult(settingKey);
            return dbResult.ResultSet.Tables[0];
        }
        catch (Exception e)
        {
            return null;
        }
    }
}