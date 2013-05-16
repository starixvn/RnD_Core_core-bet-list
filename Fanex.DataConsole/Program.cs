using Fanex.Data;
using Fanex.Data.Configuration;
using System;
using System.Collections;
using Fanex.Common;
namespace Fanex.DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            DbSettingProviderManager.Current.Start();

            using (ObjectDb db = new ObjectDb("GetEmployees"))
            {
                //var param = new Hashtable();
                //param.Add("query", string.Empty);
                //param.Add("groupId",0);
                //param.Add("enable", string.Empty);
                //param.Add("", string.Empty);
                //param.Add("", string.Empty);
                //param.Add("", string.Empty);
                //param.Add("", string.Empty);

                //var result = db.Query<dynamic>(new { pageNumber = 1, pageSize = 100 });

                var s = "123";
                var abc = Converter.To<long>(s);
            }


        }

    }
}
