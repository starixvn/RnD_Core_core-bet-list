// <copyright file="Statistics.aspx.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <history>
// <change who="anders.le" date="2012.3.30">Created</change>
// </history>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using Fanex.Common.Extensions;
using Fanex.Data;

/// <summary>
/// Statistics page to measure performance of Dapper, Sqlreader, ADO.NET and Dataset
/// </summary>
public partial class Statistics : System.Web.UI.Page
{
    /// <summary>
    /// SP for testing in bodb02 SELECT TOP N * FROM Bettrans
    /// </summary>
    private const string SPName = "FanexTestPerformanceMapping";

    /// <summary>
    /// Number Of Runner
    /// </summary>
    private const int NumberOfRunner = 20;

    /// <summary>
    /// delegate query data
    /// </summary>
    /// <param name="totalRecord">The total record.</param>
    private delegate void QueryData(int totalRecord);

    #region Properties
    /// <summary>
    /// Gets the total record to select top
    /// </summary>
    public int TotalRecord
    {
        get
        {
            int totalRecord = Convert.ToInt32(ddlRecord.SelectedValue);
            if (totalRecord > 2000 || totalRecord <= 0)
            {
                totalRecord = 1000;
            }

            return totalRecord;
        }
    }

    /// <summary>
    /// Gets the number of running query
    /// </summary>
    public int Runner
    {
        get
        {
            int numberOfRunner = Convert.ToInt32(ddlNumberOfRuntime.SelectedValue);
            if (numberOfRunner > 20 || numberOfRunner <= 0)
            {
                numberOfRunner = 10;
            }

            return numberOfRunner;
        }
    } 
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Chart1.Series["Fanex-Dapper"].IsValueShownAsLabel = false;
        Chart1.Series["Fanex-EntlibReader"].IsValueShownAsLabel = false;
        Chart1.Series["ADOReader"].IsValueShownAsLabel = false;
        Chart1.Series["Dataset"].IsValueShownAsLabel = false;

        if (!Page.IsPostBack)
        {
            Chart1.Legends["Default"].Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Milisecond";
            Chart1.ChartAreas["ChartArea1"].AxisY.TextOrientation = TextOrientation.Horizontal;
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Running Order Number";
        }
    }

    /// <summary>
    /// Runs the statictics.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void RunStatictics(object sender, EventArgs e)
    {
        Chart1.ResetAutoValues();
        int totalRecord = this.TotalRecord;
        int runner = this.Runner;

        if (chkFanexDapper.Checked)
        {
            var data = this.Run(totalRecord, runner, DapperReader);
            this.BindChart("Fanex-Dapper", runner, data);
        }

        if (chkEntlib.Checked)
        {
            var data = this.Run(totalRecord, runner, EntlibReader);
            this.BindChart("Fanex-EntlibReader", runner, data);
        }

        if (chkADO.Checked)
        {
            var data = this.Run(totalRecord, runner, EntlibReader);
            this.BindChart("ADOReader", runner, data);
        }

        if (chkDataSet.Checked)
        {
            var data = this.Run(totalRecord, runner, EntlibReader);
            this.BindChart("Dataset", runner, data);
        }
    }

    /// <summary>
    /// Dappers the reader.
    /// </summary>
    /// <param name="numRecord">The num record.</param>
    private static void DapperReader(int numRecord)
    {
        using (ObjectDb objDb = new ObjectDb(SPName))
        {
            IEnumerable<Bettrans> events = objDb.Query<Bettrans>(new { NumberTopRecord = numRecord });
            events.ToList<Bettrans>();
        }
    }

    /// <summary>
    /// Entlibs the reader.
    /// </summary>
    /// <param name="numRecord">The num record.</param>
    private static void EntlibReader(int numRecord)
    {
        using (ObjectDb objDb = new ObjectDb(SPName))
        {
            List<Bettrans> list = new List<Bettrans>();
            IDataReader reader = objDb.ExecuteReader(new { NumberTopRecord = numRecord });
            while (reader.Read())
            {
                var entity = new Bettrans()
                {
                    acomm = reader.ToDecimal("acomm"),
                    actual_stake = reader.ToDecimal("actual_stake"),
                    actualrate = reader.ToDouble("actualrate"),
                    adiscount = reader.ToDecimal("adiscount"),
                    apositiontaking = reader.ToDecimal("apositiontaking"),
                    awinlost = reader.ToDecimal("awinlost"),
                    balance = reader.ToDecimal("balance"),
                    betcheck = reader.ToString("balance"),
                    betdaqid = reader.ToLong("betdaqid"),
                    betfrom = reader.ToString("betfrom"),
                    betid = reader.ToLong("betid"),
                    betteam = reader.ToString("betteam"),
                    bettype = reader.ToShort("bettype"),
                    checktime = reader.ToDateTime("checktime"),
                    comm = reader.ToDecimal("comm"),
                    comstatus = reader.ToString("comstatus"),
                    creator = reader.ToInt("creator"),
                    currency = reader.ToByte("currency"),
                    currency2 = reader.ToByte("currency2"),
                    CurrencyName = reader.ToString("CurrencyName"),
                    custid = reader.ToInt("custid"),
                    discount = reader.ToDecimal("discount"),
                    hdp1 = reader.ToDecimal("hdp1"),
                    hdp2 = reader.ToDecimal("hdp2"),
                    ip = reader.ToString("ip"),
                    liveawayscore = reader.ToShort("liveawayscore"),
                    livehomescore = reader.ToShort("livehomescore"),
                    liveindicator = reader.ToBool("liveindicator"),
                    matchid = reader.ToInt("matchid"),
                    modds = reader.ToDecimal("modds"),
                    mpositiontaking = reader.ToDecimal("mpositiontaking"),
                    mrecommend = reader.ToInt("mrecommend"),
                    mwinlost = reader.ToDecimal("mwinlost"),
                    odds = reader.ToDecimal("odds")
                };
                list.Add(entity);
            }
        }
    }

    /// <summary>
    /// ADOs the reader.
    /// </summary>
    /// <param name="numRecord">The num record.</param>
    private static void AdoReader(int numRecord)
    {
        using (SqlConnection conn
            = new SqlConnection(ConfigurationManager.ConnectionStrings["main"].ToString()))
        {
            conn.Open();
            List<Bettrans> list = new List<Bettrans>();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = SPName;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = conn;
            sqlCommand.Parameters.Add(new SqlParameter("@NumberTopRecord", numRecord));
            using (IDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var entity = new Bettrans()
                    {
                        acomm = reader.ToDecimal("acomm"),
                        actual_stake = reader.ToDecimal("actual_stake"),
                        actualrate = reader.ToDouble("actualrate"),
                        adiscount = reader.ToDecimal("adiscount"),
                        apositiontaking = reader.ToDecimal("apositiontaking"),
                        awinlost = reader.ToDecimal("awinlost"),
                        balance = reader.ToDecimal("balance"),
                        betcheck = reader.ToString("balance"),
                        betdaqid = reader.ToLong("betdaqid"),
                        betfrom = reader.ToString("betfrom"),
                        betid = reader.ToLong("betid"),
                        betteam = reader.ToString("betteam"),
                        bettype = reader.ToShort("bettype"),
                        checktime = reader.ToDateTime("checktime"),
                        comm = reader.ToDecimal("comm"),
                        comstatus = reader.ToString("comstatus"),
                        creator = reader.ToInt("creator"),
                        currency = reader.ToByte("currency"),
                        currency2 = reader.ToByte("currency2"),
                        CurrencyName = reader.ToString("CurrencyName"),
                        custid = reader.ToInt("custid"),
                        discount = reader.ToDecimal("discount"),
                        hdp1 = reader.ToDecimal("hdp1"),
                        hdp2 = reader.ToDecimal("hdp2"),
                        ip = reader.ToString("ip"),
                        liveawayscore = reader.ToShort("liveawayscore"),
                        livehomescore = reader.ToShort("livehomescore"),
                        liveindicator = reader.ToBool("liveindicator"),
                        matchid = reader.ToInt("matchid"),
                        modds = reader.ToDecimal("modds"),
                        mpositiontaking = reader.ToDecimal("mpositiontaking"),
                        mrecommend = reader.ToInt("mrecommend"),
                        mwinlost = reader.ToDecimal("mwinlost"),
                        odds = reader.ToDecimal("odds")
                    };
                    list.Add(entity);
                }

                reader.Close();
            }

            conn.Close();
        }
    }

    /// <summary>
    /// Executes the data set.
    /// </summary>
    /// <param name="numRecord">The num record.</param>
    private static void ExecuteDataSet(int numRecord)
    {
        using (ObjectDb objDb = new ObjectDb(SPName))
        {
            List<Bettrans> list = new List<Bettrans>();
            var ds = objDb.ExecuteDataSet(new { NumberTopRecord = numRecord });

            if (ds.Tables.Count > 0)
            {
                var rows = ds.Tables[0].Rows;
                for (int i = 0; i < numRecord; i++)
                {
                    var entity = new Bettrans
                    {
                        acomm = Convert.ToDecimal(rows[i]["acomm"]),
                        actual_stake = Convert.ToDecimal(rows[i]["actual_stake"]),
                        actualrate = Convert.ToDouble(rows[i]["actualrate"]),
                        adiscount = Convert.ToDecimal(rows[i]["adiscount"]),
                        apositiontaking = Convert.ToDecimal(rows[i]["apositiontaking"]),
                        awinlost = Convert.ToDecimal(rows[i]["awinlost"]),
                        balance = Convert.ToDecimal(rows[i]["balance"]),
                        betcheck = Convert.ToString(rows[i]["balance"]),
                        betdaqid = Convert.ToInt64(rows[i]["betdaqid"]),
                        betfrom = Convert.ToString(rows[i]["betfrom"]),
                        betid = Convert.ToInt64(rows[i]["betid"]),
                        betteam = Convert.ToString(rows[i]["betteam"]),
                        bettype = Convert.ToByte(rows[i]["bettype"]),
                        checktime = Convert.ToDateTime(rows[i]["checktime"]),
                        comm = Convert.ToDecimal(rows[i]["comm"]),
                        comstatus = Convert.ToString(rows[i]["comstatus"]),
                        creator = Convert.ToInt32(rows[i]["creator"]),
                        currency = Convert.ToByte(rows[i]["currency"]),
                        currency2 = Convert.ToByte(rows[i]["currency2"]),
                        CurrencyName = Convert.ToString(rows[i]["CurrencyName"]),
                        custid = Convert.ToInt32(rows[i]["custid"]),
                        discount = Convert.ToDecimal(rows[i]["discount"]),
                        hdp1 = Convert.ToDecimal(rows[i]["hdp1"]),
                        hdp2 = Convert.ToDecimal(rows[i]["hdp2"]),
                        ip = Convert.ToString(rows[i]["ip"]),
                        liveawayscore = Convert.ToInt16(rows[i]["liveawayscore"]),
                        livehomescore = Convert.ToInt16(rows[i]["livehomescore"]),
                        liveindicator = Convert.ToBoolean(rows[i]["liveindicator"]),
                        matchid = Convert.ToInt32(rows[i]["matchid"]),
                        modds = Convert.ToDecimal(rows[i]["modds"]),
                        mpositiontaking = Convert.ToDecimal(rows[i]["mpositiontaking"]),
                        mrecommend = Convert.ToInt32(rows[i]["mrecommend"]),
                        mwinlost = Convert.ToDecimal(rows[i]["mwinlost"]),
                        odds = Convert.ToDecimal(rows[i]["odds"])
                    };

                    list.Add(entity);
                }
            }
        }
    }

    /// <summary>
    /// Runs the specified total record.
    /// </summary>
    /// <param name="totalRecord">The total record.</param>
    /// <param name="runner">The runner.</param>
    /// <param name="queryFunction">The query function.</param>
    /// <returns>Hashtable running order number with miliseconds</returns>
    private Hashtable Run(int totalRecord, int runner, QueryData queryFunction)
    {
        Hashtable data = new Hashtable();
        Stopwatch sw;
        for (int i = 1; i <= runner; i++)
        {
            sw = Stopwatch.StartNew();
            queryFunction(totalRecord);
            sw.Stop();
            data.Add(i, sw.Elapsed.Milliseconds);
        }

        return data;
    }

    /// <summary>
    /// Binds the chart.
    /// </summary>
    /// <param name="serieName">Name of the serie.</param>
    /// <param name="runner">The runner.</param>
    /// <param name="data">The data.</param>
    private void BindChart(string serieName, int runner, Hashtable data)
    {
        for (int i = 1; i <= runner; i++)
        {
            Chart1.Series[serieName].Points.AddXY(i, data[i]);
            Chart1.Series[serieName].IsValueShownAsLabel = true;
        }
    } 
}