using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data;
using Fanex.Data.Dapper;
using Fanex.Data.DNAide;
using Fanex.Common.Extensions;
using System.Data;
using Fanex.Data.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Configuration;

namespace Fanex.DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            DbSettingProviderManager.Current.Start();

            var list = new List<dynamic>();

            int? id = null;

            using (ObjectDb db = new ObjectDb("Employee_GetAll"))
            {
                var hashtable = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
                hashtable.Add("COUNT", 0);

                list = db.Query<dynamic>(hashtable).ToList();

                //db.AddOutParameter("count", DbType.Int32, 0);
                //db.AddInParameter("id", DbType.Int32, 122);
                var ob = db.Query<dynamic>();

                var ob2 = db.Query<dynamic>();

                //var hashparam = db.GetOuputParams();

                //var has = db.GetParameterValue("count");

                //var ds1 =db.ExecuteDataSet();


                //var ds2= db.ExecuteDataSet();

                
            }

            var strConn = ConfigurationManager.ConnectionStrings["nexhr"].ConnectionString;

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand command = new SqlCommand("NexHRMDemo_Employee_GetAll", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id",123));

            SqlDataAdapter da = new SqlDataAdapter(command);

            var ds = new DataSet();

            da.Fill(ds);


            var hash = new Hashtable();

            //foreach (DictionaryEntry item in hash)
            //{
            //    item.Value = "sdfsdf";
            //}

            Console.ReadKey();
        }
    }
}
