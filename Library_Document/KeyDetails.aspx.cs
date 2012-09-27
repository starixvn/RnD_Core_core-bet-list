using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Data;
using Fanex.Caching;
using System.Xml.Linq;
using System.Collections;
public partial class KeyDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void btnViewKey_Click(object sender, EventArgs e)
    {
        if (rdoCache.Checked)
        {
            try
            {
                ObjectDb objDb = new ObjectDb(txtKey.Text.Trim());
                Show((DbSetting)objDb.DbSetting);
            }
            catch (Exception ex)
            {
                string msg = "<script type=\"text/javascript\">alert(\"" + ex.Message + "\");</script>";
                Response.Write(msg);
                Clear();
            }
        }
        else
        {
            string pathResource = MapPath("SpName.xml");
            DbSetting dbSetting = GetDbSettingFromResource(pathResource, txtKey.Text.Trim());
            if (dbSetting != null)
            {
                Show(dbSetting);
            }
            else
            {
                string msg = "<script type=\"text/javascript\">alert(\"Not Found\");</script>";
                Response.Write(msg);
                Clear();
            }
        }
    }

    protected void Show(DbSetting dbSetting)
    {
        lbSpName.Text = dbSetting.SpName;
        lbConnection.Text = dbSetting.ConnectionString;
        lbDbName.Text = dbSetting.DbName;
    }

    protected void Clear()
    {
        lbConnection.Text = string.Empty;
        lbDbName.Text = string.Empty;
        lbSpName.Text = string.Empty;
    }

    protected DbSetting GetDbSettingFromResource(string path, string key)
    {
        XDocument document = XDocument.Load(path);
        var functions = document.Descendants("Functions");
        foreach (var itemFunctions in functions)
        {
            var setting = itemFunctions.Descendants("Setting");
            foreach (var itemSetting in setting)
            {
                string keySource = itemSetting.Attribute("Key").Value.ToString();
                if (string.Compare(keySource, key) == 0)
                {
                    string spName = itemSetting.Attribute("SpName").Value.ToString();
                    string dbName = itemSetting.Attribute("DbName").Value.ToString();
                    bool isEncryp = true;
                    if (itemSetting.Attribute("Plain") != null)
                    {
                        isEncryp = itemSetting.Attribute("Plain").Value.ToString() != "1";
                    }
                    string conn = ConnectionStringGetter.GetConnectionString(dbName, isEncryp);
                    var dbSetting = new DbSetting()
                    {
                        SpName = spName,
                        DbName = dbName,
                        ConnectionString = conn,
                        CustomMappingColumns = new Hashtable()
                    };
                    return dbSetting;
                }
            }
        }
        return null;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtAddExt.Text.Trim() != string.Empty)
        {
            try
            {
                string[] strKey = txtAddExt.Text.Trim().Split(',');
                string path = MapPath("SpName.xml");
                foreach (var item in strKey)
                {
                    AddExt(path, item, txtExt.Text.Trim());
                }
                lbMessage.Text = "OK";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }
        else 
        {
            lbMessage.Text = "Key is empty";
        }
        //string msg = "<script type=\"text/javascript\">alert(\"OK\");</script>";
        //Response.Write(msg);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (txtAddExt.Text.Trim() != string.Empty)
        {
            try
            {
                string[] strKey = txtAddExt.Text.Trim().Split(',');
                string path = MapPath("SpName.xml");
                foreach (var item in strKey)
                {
                    ClearExt(path, item, txtExt.Text.Trim());
                }
                lbMessage.Text = "OK";
            }
            catch (Exception ex)
            {
                lbMessage.Text = ex.Message;
            }
        }
        else
        {
            lbMessage.Text = "Key is empty";
        }
        //string msg = "<script type=\"text/javascript\">alert(\"OK\");</script>";
        //Response.Write(msg);
    }

    protected void AddExt(string path, string key, string ext)
    {
        try
        {
            XDocument document = XDocument.Load(path);
            var functions = document.Descendants("Functions");
            foreach (var itemFunctions in functions)
            {
                var setting = itemFunctions.Descendants("Setting");
                foreach (var itemSetting in setting)
                {
                    string keySource = itemSetting.Attribute("Key").Value.ToString();
                    if (string.Compare(keySource, key) == 0)
                    {
                        itemSetting.Attribute("SpName").Value = itemSetting.Attribute("SpName").Value.ToString() + "_" + ext;
                    }
                }

            }
            document.Save(path);

        }
        catch (Exception ex)
        {
            //string msg = "<script type=\"text/javascript\">alert(\"" + ex.Message + "\");</script>";
            //Response.Write(msg);
            lbMessage.Text = ex.Message;
        }
    }

    protected void ClearExt(string path, string key, string ext)
    {
        try
        {
            XDocument document = XDocument.Load(path);
            var functions = document.Descendants("Functions");
            foreach (var itemFunctions in functions)
            {
                var setting = itemFunctions.Descendants("Setting");
                foreach (var itemSetting in setting)
                {
                    string keySource = itemSetting.Attribute("Key").Value.ToString();
                    if (string.Compare(keySource, key) == 0)
                    {
                        itemSetting.Attribute("SpName").Value = ClearExtString(itemSetting.Attribute("SpName").Value.ToString(), "_" + ext);
                    }
                }

            }
            document.Save(path);

        }
        catch (Exception ex)
        {
            //string msg = "<script type=\"text/javascript\">alert(\"" + ex.Message + "\");</script>";
            //Response.Write(msg);
            lbMessage.Text = ex.Message;
        }
    }

    protected string ClearExtString(string spName, string ext)
    {
        int index = spName.IndexOf(ext);
        return spName.Substring(0, index);
    }
}