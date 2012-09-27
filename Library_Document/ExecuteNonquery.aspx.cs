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
        string id = Request.Form["id"];
        string name = Request.Form["name"];
        gvOldSource.DataSource = LoadData("GetAllLicenseesTestTin");
        gvOldSource.DataBind();
        BindSource(settingKey, id, name);
        gvNewSource.DataSource = LoadData("GetAllLicenseesTestTin");
        gvNewSource.DataBind();
    }

    protected void BindSource(string settingKey, string id, string name)
    {
        lbError.Text = BindDataSource(settingKey, id, name).ToString();

    }

    private int BindDataSource(string settingKey, string id, string name)
    {
        try
        {
            //One way
            Hashtable hstable = new Hashtable();
            hstable.Add("licenseesid", int.Parse(id));
            hstable.Add("licenseesname", name);
            int rowAffect = (int)DbHelper.ExecuteNonQuery(settingKey, hstable);

            //Other way
            //int rowAffect = (int)DbHelper.ExecuteNonQuery(settingKey, new object[] { id, name });
            return rowAffect;
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
            DbResult dbResult = DbHelper.Execute(settingKey);
            return dbResult.ResultSet.Tables[0];
        }
        catch (Exception e)
        {
            return null;
        }
    }
}