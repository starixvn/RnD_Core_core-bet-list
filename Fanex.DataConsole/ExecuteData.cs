using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
namespace Fanex.DataConsole
{
    public class ExecuteData
    {

        public List<EventPerformances> GetEvents(int num)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=CP0080-EDWARD\SQLEXPRESS;Initial Catalog=RTSportcast;User ID=sa;password=1234aa;"))
            {
                conn.Open();
                List<EventPerformances> list = new List<EventPerformances>();
                string sql = "CheckPerformanceQueryMap";
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = conn;
                sqlCommand.Parameters.Add(new SqlParameter("@numrecord", num));
                using (IDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var temp = new EventPerformances()
                        {
                            EventID = int.Parse(reader["EventID"].ToString()),
                            Name = reader["Name"].ToString(),
                            Player = reader["Player"].ToString(),
                            ServerTime = reader["ServerTime"].ToString(),
                            MatchID = int.Parse(reader["MatchID"].ToString()),
                            CreateDate = DateTime.Parse(reader["CreateDate"].ToString())
                        };
                        list.Add(temp);
                    }
                    reader.Close();
                }
                conn.Close();
                return list;
            }

        }
    }
}
