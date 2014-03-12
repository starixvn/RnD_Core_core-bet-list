// ---------------------------------------------------------------------------------
// <copyright file="BetListDAO.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.25">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Tests.Integration.Data
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class Bet List DAO.
    /// </summary>
    public class BetListDAO
    {
        /// <summary>
        /// Gets the bet list data.
        /// </summary>
        /// <returns>Data Set of result.</returns>
        public static DataSet GetBetListData()
        {
            string conn = ConfigurationManager.ConnectionStrings["BetListDataConn"].ConnectionString;
            string sql = @"select * from corebetlist
                           select * from coreBetlistDetail";

            var ds = new DataSet();

            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(sql, sqlConn);
                command.CommandType = CommandType.Text;

                sqlConn.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }
            }

            return ds;
        }

        /// <summary>
        /// Gets the running bet data.
        /// </summary>
        /// <returns>Data Table.</returns>
        public static DataTable GetRunningBetData()
        {
            string conn = ConfigurationManager.ConnectionStrings["BetListDataConn"].ConnectionString;
            string sql = @"select * from corebetlist_running
                           select * from corebetlist_running_ref";
            var ds = new DataSet();
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(sql, sqlConn);
                command.CommandType = CommandType.Text;
                sqlConn.Open();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(ds);
                }
            }

            DataTable mainDB = ds.Tables[0];
            DataTable refDB = ds.Tables[1];
            mainDB.Columns.Add("showtime", typeof(string));
            string bettypeVirtualSportsOutrightList = "1231,1232,1233";
            int bettype;
            int matchId;
            foreach (DataRow row in mainDB.Rows)
            {
                bettype = Convert.ToInt32(row["bettype"]);

                if (!string.IsNullOrWhiteSpace(bettypeVirtualSportsOutrightList) &&
                Regex.IsMatch(string.Format(",{0},", bettypeVirtualSportsOutrightList), string.Format(",{0},", bettype), RegexOptions.IgnoreCase))
                {
                    matchId = Convert.ToInt32(row["race"]);
                }
                else
                {
                    matchId = Convert.ToInt32(row["matchid"]);
                }

                DataRow[] matchedRow = refDB.Select("matchid=" + matchId);
                var matchDetailInfo = matchedRow != null && matchedRow.Length > 0 ? matchedRow[0] : null;
                if (matchDetailInfo == null)
                {
                    continue;
                }

                UpdateRunningBetDataRow(matchDetailInfo, row);                
            }

            return mainDB;
        }

        private static void UpdateRunningBetDataRow(DataRow matchDetailInfo, DataRow destinationRow)
        {
            destinationRow["matchcode"] = matchDetailInfo["matchcode"];
            destinationRow["sporttype"] = matchDetailInfo["sporttype"];
            destinationRow["leagueid"] = matchDetailInfo["leagueid"];
            destinationRow["homeid"] = matchDetailInfo["homeid"];
            destinationRow["awayid"] = matchDetailInfo["awayid"];
            destinationRow["race"] = matchDetailInfo["race"];
            destinationRow["showtime"] = matchDetailInfo["showtime"];
        }
    }
}
