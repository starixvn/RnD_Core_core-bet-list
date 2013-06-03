using Fanex.Data;
using Fanex.Data.Configuration;
using System;
using System.Collections;
using Fanex.Common;
using System.Data;
namespace Fanex.DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            DbSettingProviderManager.Current.Start();

            using (ObjectDb db = new ObjectDb("GetEmployees"))
            {
                var param = new Hashtable();
                param.Add("query", string.Empty);
                param.Add("groupId", null);
                param.Add("enable", null);
                param.Add("isdelete", null);
                param.Add("pagenumber", 1);
                param.Add("pagesize", 100);
                param.Add("totalrow", 0);
                param.Add("titleid", null);
                param.Add("abc", 100);
                var result = db.Query<dynamic>(param);
                //db.ExecuteDataSet(param);

                //var result = db.ExecuteDataSet(new
                //{
                //    Query = string.Empty,
                //    GroupId = 0,
                //    Enable = true,
                //    IsDelete = false,
                //    PageNumber = 1,
                //    PageSize = 100,
                //    TotalRow = 0,
                //    TitleId = 0,
                //    ABC = string.Empty
                //});


                //db.AddInParameter("@query", DbType.String, string.Empty);
                //db.AddInParameter("@groupId", DbType.Int32, null);
                //db.AddInParameter("@enable", DbType.Boolean, null);
                //db.AddInParameter("@isdelete", DbType.Boolean, null);
                //db.AddInParameter("@pagenumber", DbType.Int32, 1);
                //db.AddInParameter("@pagesize", DbType.Int32, 100);
                //db.AddInParameter("@totalrow", DbType.Int32, 0);
                //db.AddInParameter("@titleid", DbType.Int32, null);
                //db.AddInParameter("abc", DbType.Int32, 100);

                //db.ExecuteDataSet();
            }


        }

    }
}
