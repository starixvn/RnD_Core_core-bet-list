using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Reflection;
using System.Diagnostics;
using Fanex.Data;
using System.Web.UI.HtmlControls;
public partial class MultiMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    DataTable table = new DataTable();
        //    table = GetSource(@"D:\Working\Fanex_BrachCode\QueryMap\Library_Document\Statistics.xml");

        //    foreach (DataRow rw in table.Rows)
        //    {
        //        double x = double.Parse(rw["NumRecord"].ToString());
        //        Chart1.Series["AutoMap"].Points.AddXY(x, double.Parse(rw["AutoMap"].ToString()));
        //        Chart1.Series["EntlibReader"].Points.AddXY(x, double.Parse(rw["EntlibReader"].ToString()));
        //        Chart1.Series["ADOReader"].Points.AddXY(x, double.Parse(rw["ADOReader"].ToString()));
        //    }
        //}
        //Chart1.Series["AutoMap"].IsValueShownAsLabel = true;
        //Chart1.Series["EntlibReader"].IsValueShownAsLabel = true;
        //Chart1.Series["ADOReader"].IsValueShownAsLabel = true;

        //Chart1.Legends["Default"].Enabled = true;

        //Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;

    }

    //private DataTable GetSource(string path)
    //{
    //    XDocument document = XDocument.Load(path);
    //    var statistics = document.Descendants("Statistics");
    //    DataTable table = new DataTable();
    //    table.Columns.Add("NumRecord");
    //    table.Columns.Add("AutoMap");
    //    table.Columns.Add("EntlibReader");
    //    table.Columns.Add("ADOReader");
    //    foreach (var item in statistics)
    //    {
    //        foreach (var itemStatistic in item.Descendants("Statistic"))
    //        {
    //            string numRecord = itemStatistic.Attribute("NumRecord").Value.ToString();
    //            string autoMap = itemStatistic.Attribute("AutoMap").Value.ToString();
    //            string entlibReader = itemStatistic.Attribute("EntlibReader").Value.ToString();
    //            string adoReader = itemStatistic.Attribute("ADOReader").Value.ToString();
    //            DataRow rw = table.NewRow();
    //            rw["NumRecord"] = numRecord;
    //            rw["AutoMap"] = autoMap;
    //            rw["EntlibReader"] = entlibReader;
    //            rw["ADOReader"] = adoReader;
    //            table.Rows.Add(rw);
    //        }

    //    }
    //    return table;
    //}

    //protected void btnLoadNew_Click(object sender, EventArgs e)
    //{
    //    Chart1.ResetAutoValues();
    //    for (int i = 0; i <= 49000; i += 5000)
    //    {
    //        Chart1.Series["AutoMap"].Points.AddXY(i, AutoMap(i));
    //        Chart1.Series["EntlibReader"].Points.AddXY(i, EntlibReader(i));
    //        Chart1.Series["ADOReader"].Points.AddXY(i, AdoReader(i));
    //    }        
    //}


    //private static double AutoMap(int numRecord)
    //{
    //    Stopwatch sw = new Stopwatch();
    //    sw.Start();
    //    using (ObjectDb objDb = new ObjectDb("CheckPerformanceQueryMap"))
    //    {
    //        IEnumerable<EventPerformances> events = objDb.Query<EventPerformances>(new { numrecord = numRecord });
    //        events.ToList<EventPerformances>();
    //    }
    //    sw.Stop();
    //    Console.WriteLine("Auto map : {0}", sw.Elapsed.TotalMilliseconds);
    //    return sw.Elapsed.TotalMilliseconds;
    //}

    //private static double EntlibReader(int numRecord)
    //{
    //    Stopwatch sw = new Stopwatch();
    //    sw.Start();
    //    using (ObjectDb objDb = new ObjectDb("CheckPerformanceQueryMap"))
    //    {
    //        IDataReader reader = objDb.ExecuteReader(new { numrecord = numRecord });
    //        List<EventPerformances> events = new List<EventPerformances>();
    //        while (reader.Read())
    //        {
    //            var temp = new EventPerformances()
    //            {
    //                EventID = reader.ToInt("EventID"),
    //                Name = reader.ToString("Name"),
    //                Player = reader.ToString("Player"),
    //                ServerTime = reader.ToString("ServerTime"),
    //                MatchID = reader.ToInt("MatchID"),
    //                CreateDate = reader.ToDateTime("CreateDate")
    //            };
    //            events.Add(temp);
    //        }
    //    }
    //    sw.Stop();
    //    Console.WriteLine("Entlib reader : {0}", sw.Elapsed.TotalMilliseconds);
    //    return sw.Elapsed.TotalMilliseconds;
    //}

    //private static double AdoReader(int numRecord)
    //{
    //    Stopwatch sw = new Stopwatch();
    //    sw.Start();
    //    ExecuteData objData = new ExecuteData();
    //    objData.GetEvents(numRecord);
    //    sw.Stop();
    //    Console.WriteLine("ADO reader : {0}", sw.Elapsed.TotalMilliseconds);
    //    return sw.Elapsed.TotalMilliseconds;
    //}
}