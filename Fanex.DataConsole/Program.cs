using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data;
using Fanex.Data.Dapper;
using Fanex.Data.DNAide;
using Fanex.Common.Extensions;
using System.Data;
//using Fanex.DataConsole.DTO;
using Fanex.Data.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using System.Runtime.Serialization;
using Fanex.NexHR.Models;
using System.ComponentModel;

namespace Fanex.DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();

            using (ObjectDb db = new ObjectDb("GetEmployee"))
            {
                
                var entity = db.Query<Employee, dynamic, Employee>(
                   
                    (e, t) => 
                    {                        
                        e.Group = new Team { Id = t.GroupId, Name = t.TeamName }; 
                        
                        return e; 
                    }
                    , splitOn: "GroupId", parameterValues: new { id = 165 }).FirstOrDefault();

                //var entity = db.Query<Employee>(new { id = 165 }).FirstOrDefault();                

            }

            Console.ReadKey();


            //#region Test
            ////IEnumerable<Function> functionList = DbHelper.Query<Function>("OA_GetFunctionByUserAndParentID",
            ////  4, 1);            

            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    db.AddInParameter("UserId", DbType.Int32, 1);
            ////    db.AddInParameter("ParentId", DbType.Int32, 1);

            ////    using (var reader = db.ExecuteReader())
            ////    {
            ////        if (reader != null && reader.Read())
            ////        {
            ////            Function function = new Function();
            ////            function.Id = reader.ToInt("FunctionId");
            ////            function.Name = reader.ToString("FunctionName");
            ////        }
            ////    }
            ////}
            ////  int temp = string.Compare("trungtin", "TrunGTin",true);
            ////Hashtable hs = new Hashtable();
            ////hs.Add("TrungTin", 1);

            ////bool check = hs.ContainsKeyPassCaseSensitive("TRUNGTIN");

            //using (var reader = DbHelper.ExecuteReader("OA_GetFunctionByUserAndParentID", new { userid = 1, ParentId = 1 }))
            //{

            //    while (reader.Read())
            //    {
            //        string temp = reader.ToString("functionname");
            //    }
            //}

            //using (ObjectDb db = new ObjectDb("CheckDynamicInOutParam"))
            //{
            //    db.AddInParameter("eventid", DbType.Int32, 48922);
            //    db.AddOutParameter("matchid", DbType.Int32, 0);
            //    var functions = db.ExecuteDataSet();
            //    int temp = (int)db.GetParameterValue("matchid");

            //}



            //using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            //{

            //    //db.AddInParameter("UserId", DbType.Int32, 1);
            //    //db.AddInParameter("ParentId", DbType.Int32, 1);
            //    //db.AddInParameter("ParentId", DbType.Int32, 1);
            //    //var functions = db.Query<Function>(new Hashtable { { "functionId", "Id" }, { "functionname", "Name" } });
            //    //var funtions = db.ExecuteScalar(new { userid = 1, ParentId = 1 });
            //    // var funtions = db.ExecuteScalar(1,1);

            //    // var funtions = db.ExecuteDataSet(new { UserId = 1, parentId = 1 });
            //    //var functions = db.Query<FunctionsTestCase>(new { UserId = 1, ParentId = 1 });

            //    //check for datareaderextension
            //    //using (var reader = db.ExecuteReader(new { UserId = 1, ParentId = 1 })) 
            //    //{
            //    //    while (reader.Read()) 
            //    //    {
            //    //        string id = reader.ToString("i");
            //    //    }
            //    //}

            //    //check for add param more way differnce
            //    db.AddInParameter("UserId", DbType.Int32, 1);
            //    //db.AddInParameter("UserId", DbType.Int32, 1);
            //    var functions = db.ExecuteDataSet(new { parentid = 1, userid = 1 });
            //}

            //using (ObjectDb db = new ObjectDb("OA_ResellersGet"))
            //{
            //    var reseller = db.Query<Reseller>();
            //}


            ////var functions = DbHelper.ExecuteDbResult("OA_GetFunctionByUserAndParentID", new { UserId = 1, parentId = 1 });

            //Hashtable hsTable = new Hashtable();
            //hsTable.Add("userid", 1);
            //hsTable.Add("parentid", 1);

            //var functions1 = DbHelper.ExecuteDataSet("OA_GetFunctionByUserAndParentID", hsTable);

            ////PrintFunctionList(functionList);

            ////IEnumerable<Resellers> resellers;
            ////using (ObjectDb db = new ObjectDb("OA_rptUserResellersGetByUserID"))
            ////{
            ////    resellers = db.Query<Resellers>(1);
            ////   // resellers=db.DbConnection.Query<Resellers,TestReseller,Resellers>(db.DbSetting.SpName,(resller,test)=>{resller.},
            ////    //Hashtable custom = new Hashtable();
            ////    //custom.Add("ResellerName", "Testxx");
            ////    //custom.Add("ResellerID", "ResellerIDxx");
            ////    //resellers = db.MapQuery<Reseller>(custom, 1);
            ////}

            ////PrintResellerList(resellers);

            ////IEnumerable<Functions> functionList;
            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    var temp = functionList = db.Query<Functions, NameUrl, Functions>((f, u) => { f.TestNested = u; return f; }, "splitOn:", 1, 1);
            ////    var temp1 = functionList.First();
            ////    //Hashtable custom = new Hashtable();
            ////    //custom.Add("FunctionName", "FunctionNamexx");
            ////    //functionList = db.MapQuery<dynamic>(custom, 1, 1);

            ////}

            ////PrintFunctionList(functionList);

            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    functionList = db.QueryDynamic<Function>(new { ParentId = 1, UserId = 1 });
            ////}

            ////PrintFunctionList(functionList);


            //#region rem
            ////foreach (var entity in functionList)
            ////{
            ////    Console.WriteLine(string.Format("FunctionID: {0} - FunctionName: {1}", entity.FunctionID, entity.FunctionName));
            ////}

            //////IDbSetting setting = DbSettingProviderManager.Current.GetSetting("uspGetEmployeeManagers");
            //////Console.WriteLine(setting.SpName);
            //////Console.ReadKey();
            //////setting = DbSettingProviderManager.Current.GetSetting("uspGetEmployeeManagers");
            //////Console.WriteLine(setting.SpName);

            ////List<Reseller> resellerList = new List<Reseller>();
            //////using (ObjectDB db = new ObjectDB("OA_ResellersGet"))
            //////{
            //////    using (var dbConnection = db.DbConnection)
            //////    {
            //////        dbConnection.Open();
            //////        resellerList = dbConnection.Query<Reseller>(db.DbSetting.SpName).ToList<Reseller>();
            //////        dbConnection.Close();
            //////    }
            //////}
            ////using (ObjectDb db = new ObjectDb("OA_ResellersGet"))
            ////{
            ////    resellerList = db.Query<Reseller>(db.DbSetting.SpName).ToList<Reseller>();
            ////}

            ////Console.WriteLine(resellerList[0].ResellerName);

            ////Reseller tmpReseller = new Reseller();
            ////using (ObjectDb db = new ObjectDb("OA_ResellersGet"))
            ////{
            ////    tmpReseller = db.QueryEntity<Reseller>(db.DbSetting.SpName);
            ////}

            ////Console.WriteLine(tmpReseller.ResellerName);

            ////DataSet ds;
            ////using (ObjectDB objDB = new ObjectDB("uspGetEmployeeManagers"))
            ////{
            ////    objDB.Database.AddInParameter(objDB.DbCommand, "@EmployeeID", DbType.Int32, 2);
            ////    ds = objDB.Database.ExecuteDataSet(objDB.DbCommand);
            ////    Console.WriteLine(objDB.DbSetting.SpName);
            ////}
            ////Console.ReadKey();
            ////using (ObjectDB objDB = new ObjectDB("uspGetEmployeeManagers"))
            ////{
            ////    objDB.Database.AddInParameter(objDB.DbCommand, "@EmployeeID", DbType.Int32, 2);
            ////    ds = objDB.Database.ExecuteDataSet(objDB.DbCommand);
            ////    Console.WriteLine(objDB.DbSetting.SpName);
            ////}
            ////foreach (DataRow dataRow in ds.Tables[0].Rows)
            ////{
            ////    Console.WriteLine(dataRow[2].ToString());
            ////}

            ////Console.WriteLine("===================================================");

            //////Hashtable param = new Hashtable();
            //////param.Add("employeeid", 2);
            ////ds = DbHelper.ExecuteDataSet("uspGetWhereUsedProductID", new object[] { 1, new DateTime(2010, 1, 1)});


            ////foreach (DataRow dataRow in ds.Tables[0].Rows)
            ////{
            ////    Console.WriteLine(dataRow[2].ToString() + "    " + dataRow[3].ToString());
            ////}

            ////Hashtable param = new Hashtable();
            ////param.Add("employeeid", 2);

            ////ds = DbHelper.ExecuteDataSet("uspGetEmployeeManagers", param);

            ////foreach (DataRow dataRow in ds.Tables[0].Rows)
            ////{
            ////    Console.WriteLine(dataRow[2].ToString() + "    " + dataRow[3].ToString());
            ////}


            ////ds = DbHelper.ExecuteDataSet("uspGetEmployeeManagers", param);

            ////foreach (DataRow dataRow in ds.Tables[0].Rows)
            ////{
            ////    Console.WriteLine(dataRow[2].ToString() + "    " + dataRow[3].ToString());
            ////}


            ////var ds = DbHelper.ExecuteDataset("OA_ResellersGet");
            ////Hashtable param = new Hashtable();
            ////param.Add("custid", 1);
            ////param.Add("netsessionid", 1);
            ////param.Add("on_userid", 1);
            ////param.Add("ip", 1);

            ////var dbResult = DbHelper.Execute("OA_UserCheckIn", param);

            ////param = new Hashtable();
            ////param.Add("groupid", 1);

            ////IEnumerable<Reseller> reseller = DbHelper.Query<Reseller>("OA_GroupResellersGetByGroupID", param);

            ////var list = reseller.ToList();

            ////var id = list[0].ResellerID;


            ////IEnumerable<Reseller> reseller = DbHelper.Query<Reseller>("OA_ResellersGet");

            ////foreach (DataRow dataRow in ds.Tables[0].Rows)
            ////{
            ////    Console.WriteLine(dataRow[1].ToString());
            ////}
            ////Console.ReadLine();

            //// IEnumerable<Reseller> resellerList = DbHelper.Query<Reseller>("OA_ResellersGet");

            ////DbSetting setting = DbSettingProviderManager.Current.GetSetting("OA_ResellersGet");
            ////using (ObjectDB objDb = new ObjectDB(setting))
            ////{
            ////    objDb.DbConnection.Open();
            ////    IEnumerable<Reseller> list = objDb.DbConnection.Query<Reseller>(setting.SpName, 
            ////        null, null, true, 90, CommandType.StoredProcedure);
            ////    objDb.DbConnection.Close();
            ////}

            //#endregion

            //#endregion

            //#region Nested Class
            //using (ObjectDb db = new ObjectDb("TestNestedClss"))
            //{

            //    IEnumerable<EventsX> temp1 = db.Query<EventsX, Matches, EventsX>((c, t) => { c.Match = t; return c; }, "MatchID");
            //    // db.ExecuteScalar(
            //}

            ////using (ObjectDb db = new ObjectDb("TestNestedClss", "RTSportcast", false))
            ////{

            ////    IEnumerable<EventsX> temp1 = db.Query<EventsX, Matches, EventsX>((c, t) => { c.Match = t; return c; }, "MatchID");

            ////}

            //using (ObjectDb db = new ObjectDb("TestNestedClss"))
            //{

            //    IEnumerable<EventsX> temp1 = db.Query<EventsX, Matches, EventsX>((c, t) => { c.Match = t; return c; }, "MatchID");

            //}
            //#endregion

            //#region Entlib Method for dynamic param
            ////Stopwatch sw = new Stopwatch();
            ////sw.Start();
            ////#region Execute dynamic parameter

            //////for execute scalar
            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    db.AddInParameter("UserId", DbType.Int32, 1);
            ////    db.AddInParameter("ParentId", DbType.Int32, 1);
            ////    var funtions = db.ExecuteScalar();
            ////}

            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    var funtions = db.ExecuteScalar(new { UserId = 1, ParentId = 1 });
            ////}

            //////for execute dataset
            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    db.AddInParameter("UserId", DbType.Int32, 1);
            ////    db.AddInParameter("ParentId", DbType.Int32, 1);
            ////    var funtions = db.ExecuteDataSet();
            ////}

            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    var funtions = db.ExecuteDataSet(new { UserId = 1, ParentId = 1 });

            ////}

            //////for execute reader
            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    db.AddInParameter("UserId", DbType.Int32, 1);
            ////    db.AddInParameter("ParentId", DbType.Int32, 1);
            ////    var funtions = db.ExecuteReader();
            ////    while (funtions.Read())
            ////    {
            ////        int count = funtions.FieldCount;
            ////    }
            ////}

            ////using (ObjectDb db = new ObjectDb("OA_GetFunctionByUserAndParentID"))
            ////{
            ////    var funtions = db.ExecuteReader(new { UserId = 1, ParentId = 1 });
            ////    while (funtions.Read())
            ////    {
            ////        int count = funtions.FieldCount;
            ////    }
            ////}
            ////#endregion

            ////sw.Stop();
            ////Console.WriteLine(sw.Elapsed.TotalMilliseconds.ToString());
            //#endregion

            //#region Check performance for Mapauto vs Map by DataReader
            //////double avage = 0;

            //////XDocument documment = XDocument.Load(@"D:\Working\Fanex_BrachCode\QueryMap\Library_Document\Statistics.xml");
            ////XDocument documment = XDocument.Load(@"D:\Working\Fanex_BrachCode\QueryMap\Fanex.DataConsole\Statistics.xml");
            ////var root = documment.Descendants("Statistics");
            ////foreach (var item in root)
            ////{
            ////    XElement element = new XElement("Statistic");
            ////    for (int i = 0; i <= 49000; i += 10)
            ////    {
            ////        //Console.WriteLine("\nNumber record " + i.ToString());
            ////        element.SetAttributeValue("NumRecord", i);
            ////        element.SetAttributeValue("AutoMap", AutoMap(i));
            ////        element.SetAttributeValue("EntlibReader", EntlibReader(i));
            ////        element.SetAttributeValue("ADOReader", AdoReader(i));
            ////        item.Add(element);
            ////    }

            ////}            
            ////documment.Save(@"D:\Working\Fanex_BrachCode\QueryMap\Fanex.DataConsole\Statistics.xml");
            //////Console.WriteLine(avage / 100);
            //////int check = int.Parse(Console.ReadLine());

            //////CheckPerformance(check);


            //#endregion
            //Console.ReadLine();
        }

        private static void TestingDBResult()
        {
            Hashtable h = new Hashtable();
            h.Add("a", "value of a");

            DataSet ds = new DataSet();
            ds.DataSetName = "ds1";

            DbResult testResult = new DbResult(h, ds);

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            f.Serialize(stream, testResult);


            Console.WriteLine("Serialize DbResult is ok");
            stream.Position = 0;
            DbResult testResult2 = f.Deserialize(stream) as DbResult;
            if (testResult2.Outputs["a"].ToString() == "value of a")
            {
                Console.WriteLine("Deserialize DbResult is ok");
            }
            else
            {
                Console.WriteLine("Deserialize DbResult is failed");
            }
        }

        private static void CheckPerformance(int numRecord)
        {
            Console.WriteLine("================Number record {0} ===========\n", numRecord.ToString());

            AutoMap(numRecord);

            EntlibReader(numRecord);

            AdoReader(numRecord);

            // Console.WriteLine("================================================");
        }

        private static double AutoMap(int numRecord)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (ObjectDb objDb = new ObjectDb("CheckPerformanceQueryMap"))
            {
                //IEnumerable<EventPerformances> events = objDb.Query<EventPerformances>(new { numrecord = numRecord });
                //events.ToList<EventPerformances>();
            }
            sw.Stop();
            Console.WriteLine("Auto map : {0}", sw.Elapsed.TotalMilliseconds);
            return sw.Elapsed.TotalMilliseconds;
        }

        private static double EntlibReader(int numRecord)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (ObjectDb objDb = new ObjectDb("CheckPerformanceQueryMap"))
            {
                IDataReader reader = objDb.ExecuteReader(new { numrecord = numRecord });
                List<EventPerformances> events = new List<EventPerformances>();
                while (reader.Read())
                {
                    var temp = new EventPerformances()
                    {
                        EventID = reader.ToInt("EventID"),
                        Name = reader.ToString("Name"),
                        Player = reader.ToString("Player"),
                        ServerTime = reader.ToString("ServerTime"),
                        MatchID = reader.ToInt("MatchID"),
                        CreateDate = reader.ToDateTime("CreateDate")
                    };
                    events.Add(temp);
                }
            }
            sw.Stop();
            Console.WriteLine("Entlib reader : {0}", sw.Elapsed.TotalMilliseconds);
            return sw.Elapsed.TotalMilliseconds;
        }

        private static double AdoReader(int numRecord)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ExecuteData objData = new ExecuteData();
            objData.GetEvents(numRecord);
            sw.Stop();
            Console.WriteLine("ADO reader : {0}", sw.Elapsed.TotalMilliseconds);
            return sw.Elapsed.TotalMilliseconds;
        }

        private static void PrintFunctionList(IEnumerable<dynamic> functionList)
        {
            foreach (var function in functionList)
            {
                Console.WriteLine(string.Format("FunctionID: {0} - FunctionName: {1}"
                    , function.FunctionID, function.URLxxx));
            }

            Console.ReadKey();
        }

        //private static void PrintResellerList(IEnumerable<Reseller> resellers)
        //{
        //    foreach (var reseller in resellers)
        //    {
        //        Console.WriteLine(string.Format("ResellerId: {0} - ResellerName: {1}"
        //            , reseller.ResellerIDxx, reseller.Testxx));
        //    }

        //    Console.ReadKey();
        //}


    }

    public class Function
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }

    public class Reseller
    {
        public int ResellerId { get; set; }
        public string ResellerName { get; set; }

    }

}
