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
        string cust = Request.Form["custid"];
        string net = Request.Form["net"];
        string ip = Request.Form["ip"];
        string user = Request.Form["user"];
        BindGridView(settingKey, cust, net, ip, user);
    }

    protected void BindGridView(string settingKey, string custid, string net, string ip, string user)
    {
        lbError.Text = string.Empty;
        gvSource.DataSource = BindDataSource(settingKey, custid, net, ip, user);
        //gvSource.DataSource = BindDataSource(settingKey, new object[] { custid, net, ip, user });
        gvSource.DataBind();
    }

    private DataTable BindDataSource(string settingKey, string custid, string net, string ip, string user)
    {
        try
        {
            Hashtable hstable = new Hashtable();
            hstable.Add("custid", int.Parse(custid));
            hstable.Add("netsessionid", int.Parse(net));
            hstable.Add("ip", int.Parse(ip));
            hstable.Add("on_userid", int.Parse(user));
            //Older
            //DbResult dbResult = DbHelper.Execute(settingKey, hstable);
            DbResult dbResult = DbHelper.ExecuteDbResult(settingKey, hstable);
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
    }

    //private DataTable BindDataSource(string settingKey, params object[] parameters)
    //{
    //    try
    //    {
    //        DbResult dbResult = DbHelper.Execute(settingKey, parameters);
    //        DataTable table = new DataTable();
    //        table.Columns.Add("Param Name");
    //        table.Columns.Add("Value");
    //        foreach (DictionaryEntry item in dbResult.Outputs)
    //        {
    //            DataRow rw = table.NewRow();
    //            rw[0] = item.Key.ToString();
    //            rw[1] = item.Value.ToString();
    //            table.Rows.Add(rw);
    //        }
    //        return table;
    //    }
    //    catch (Exception e)
    //    {
    //        lbError.Text = e.Message;
    //        return null;
    //    }
    //}
}