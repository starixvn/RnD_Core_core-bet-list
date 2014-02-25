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
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

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
    }
}
