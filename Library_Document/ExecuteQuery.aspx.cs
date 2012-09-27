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
public partial class ExecuteQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string settingKey = Request.Form["key"];

        GetAllLicensee(settingKey);

        GetLicensee(settingKey);
    }

    private void GetAllLicensee(string settingKey)
    {
        IEnumerable<Licensees> licensee = DbHelper.Query<Licensees>(settingKey);
        gvAllLicensee.DataSource = licensee;
        gvAllLicensee.DataBind();
    }

    private void GetLicensee(string settingKey)
    {
        Licensees licensee = DbHelper.QueryEntity<Licensees>(settingKey);
        lbLicenseeID.Text = licensee.LicenseeID.ToString();
        lbLicenseeName.Text = licensee.licenseename;
        lbIsDeposit.Text = licensee.IsDesposit.ToString();
        LbDescription.Text = licensee.Description;

    }

}