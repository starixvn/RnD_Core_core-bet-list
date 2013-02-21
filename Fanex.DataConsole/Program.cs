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
using System.Globalization;
using System.Data.Common;

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

                //list = db.Query<dynamic>(hashtable).ToList();

                ////db.AddOutParameter("count", DbType.Int32, 0);
                ////db.AddInParameter("id", DbType.Int32, 122);
                //var ob = db.Query<dynamic>();

                //var ob2 = db.Query<dynamic>();

                //var hashparam = db.GetOuputParams();

                //var has = db.GetParameterValue("count");

                var ds1 = db.ExecuteDataSet(hashtable);

                //dbstring
                //var ds2= db.ExecuteDataSet();

                //  db.
            }

            var strConn = ConfigurationManager.ConnectionStrings["nexhr"].ConnectionString;

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            SqlCommand command = new SqlCommand("NexHRMDemo_Employee_GetAll", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", 123));

            SqlDataAdapter da = new SqlDataAdapter(command);

            var ds = new DataSet();

            da.Fill(ds);


            //            var hash = new Hashtable();

            //foreach (DictionaryEntry item in hash)
            //{
            //    item.Value = "sdfsdf";
            //}

            Console.ReadKey();

            using (ObjectDb db = new ObjectDb("Employee_GetCategoryAll"))
            {
                using (var reader = db.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // do something with data
                    }
                }
            }

            int category = 7;

            using (ObjectDb db = new ObjectDb("GetProductsByCategory"))
            {
                var dataSet = db.ExecuteDataSet(new { CategoryId = category });
            }

            using (ObjectDb db = new ObjectDb("GetProductDetails"))
            {
                db.AddInParameter("ProductID", DbType.Int32, 3);
                db.AddOutParameter("ProductName", DbType.String, 50);
                db.AddOutParameter("UnitPrice", DbType.Currency, 8);

                db.ExecuteNonQuery();

                string results = string.Format(CultureInfo.CurrentCulture, "{0}, {1}, {2:C} ",
                  db.GetParameterValue("ProductID"),
                  db.GetParameterValue("ProductName"),
                  db.GetParameterValue("UnitPrice"));
            }

            int productId = 7;

            using (ObjectDb db = new ObjectDb("GetProductName"))
            {
                var productName = db.ExecuteScalar(new { ProdcutId = productId });
            }

            using (ObjectDb db = new ObjectDb("GetProductDetails"))
            {
                var hash = new Hashtable();
                hash.Add("productid", 1);
                hash.Add("productname", string.Empty);

                db.ExecuteNonQuery(hash);

                string results = string.Format(CultureInfo.CurrentCulture, "{0}, {1}",
                  hash["productid"],
                  hash["productname"]);
            }


            var guid = Guid.NewGuid();

            using (ObjectDb db = new ObjectDb("GetDogAll"))
            {
                //var dog = db.Query<Dog>(, new { Age = (int?)null, Id = guid });
            }
        }

        public static bool Transfer(int transactionAmount, int sourceAccount, int destinationAccount)
        {
            bool result = false;

            using (ObjectDb db = new ObjectDb("CreditAccount"))
            {
                DbTransaction trans = db.DbConnection.BeginTransaction();

                try
                {
                    // Credit the first account.
                    db.ExecuteNonQuery(trans, new { AccountID = sourceAccount, Amount = transactionAmount });
                    // Debit the second account.
                    db.ExecuteNonQuery(trans, new { AccountID = destinationAccount, Amount = transactionAmount });

                    // Commit the transaction.
                    trans.Commit();

                    result = true;
                }
                catch
                {
                    // Roll back the transaction. 
                    trans.Rollback();
                }

                return result;
            }
        }
    }
}
