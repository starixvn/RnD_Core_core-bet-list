using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Common;
using Fanex.Data;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (ObjectDb objDb = new ObjectDb("Lrf_SelBalanceUpDay"))
        {
            var dates = objDb.QueryEntity<dynamic>();

            DateTime date = dates.MoveBetsDay;
            DateTime? balanceUpDay = dates.BalanceUpDay;
        }
    }
   
}