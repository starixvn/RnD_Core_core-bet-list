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
using System.Collections;
public partial class Execute_Without_Param : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingKey = Request.Form["key"];
        int groupid = int.Parse(Request.Form["group"]);
        BindSource(settingKey, groupid);
    }

    protected void BindSource(string settingKey, int groupID)
    {
        lbError.Text = string.Empty;
        BindDataSource(settingKey, groupID);
    }


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
    }
}