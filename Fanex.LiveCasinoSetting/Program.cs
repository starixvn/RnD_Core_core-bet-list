using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.LiveCasinoSetting.Business;
using Fanex.LiveCasinoSetting.Model;
using Fanex.LiveCasinoSetting.ViewModel;
using FluentValidation;
using Fanex.LiveCasinoSetting;
using System.ComponentModel;
using Fanex.LiveCasinoSetting.Request;
using System.Data;
using Fanex.LiveCasinoSetting.Model.Log;


namespace Fanex.LiveCasinoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //Logging.LogService.Debug("Test live casino log");

            //RequestContext requestContext = new RequestContext();
            //requestContext.UserId = 0;

 //           PTSetting ptSetting = new PTSetting();
 //           ptSetting.Comm = 0.01m;
 //           ptSetting.MaxPT = 0.9m;
 //           ptSetting.Upline = new PTSetting();
 //           ptSetting.Upline.Comm = 0.1m;
 //           ptSetting.Upline.MaxPT = 0.9m;

 //           ptSetting.PresetPT = 0.9m;

 //           PTSettingValidator validator = new PTSettingValidator();
 //           var result = validator.Validate(ptSetting);
 //Console.Read();
            //var result = PTSettingBaseValidator.Rule.Validate(ptSettingBase
            
            //var result = validor.Validate(ptSettingBase, ruleSet: ValidatorSetting.PTSettingBaseRuleSet);
            
            
            //Validator.ValidateObject(requestContext, context, true);

          //  var c = default(decimal);
           // WriteLogRequest request = new WriteLogRequest();
            
            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();
            //var custIdList = new List<int> { 3501, 3562, 3563 };
            //GameSettingService settingService = new GameSettingService();
            //var gameSetting = settingService.GetByListCustomer(custIdList);

            DataTable dt = new DataTable();
            DataColumn cGameTypeId = new DataColumn("GameTypeId");
            DataColumn cBetTypeID = new DataColumn("IBCBetTypeID");
            DataColumn cStakeLevel = new DataColumn("StakeLevelName");
            dt.Columns.Add(cGameTypeId);
            dt.Columns.Add(cBetTypeID);
            dt.Columns.Add(cStakeLevel);

            var row1 = dt.NewRow();
            row1[0] = "1";
            row1[1] = "1101";
            row1[2] = "0-200";
            dt.Rows.Add(row1);

            var row2 = dt.NewRow();
            row2[0] = "1";
            row2[1] = "1101";
            row2[2] = "0-400";
            dt.Rows.Add(row2);

            var row3 = dt.NewRow();
            row3[0] = "1";
            row3[1] = "1102";
            row3[2] = "0-600";
            dt.Rows.Add(row3);


            var row4 = dt.NewRow();
            row4[0] = "1";
            row4[1] = "1102";
            row4[2] = "0-300";
            dt.Rows.Add(row4);

            var row5 = dt.NewRow();
            row5[0] = "1";
            row5[1] = "1102";
            row5[2] = "0-500";
            dt.Rows.Add(row5);

            var row6 = dt.NewRow();
            row6[0] = "1";
            row6[1] = "1101";
            row6[2] = "0-700";
            dt.Rows.Add(row6);

            var logService = new LogWriterService();

            LogAutoInsertAccount model = new LogAutoInsertAccount();
            model.dataResult = dt;

            var x = model.NewValues;
            
          //  GameSettingService gameService = new GameSettingService();
            //var gametype = gameService.GetByExhangeId(33);
           // var gametype = gameService.GetByCustomerId(839043);
           // gametype
           //     .ForEach(g => g.BettingLimit
           //         .ForEach(b => ((INotifyPropertyChanged)b).PropertyChanged += (o, arg) =>
           //             {
           //                 if ((arg.PropertyName == "IsSelected"))
           //                 {
           //                     (o as StakeLevel).IsDirty = (arg.PropertyName == "IsSelected");
           //                     Console.WriteLine((o as StakeLevel).Id + ":" + (o as StakeLevel).Text + " " + (o as StakeLevel).IsSelected.ToString() + ":" + (o as StakeLevel).IsDirty.ToString());
           //                 }
           //             }));
           //// Console.WriteLine(gametype[0].BettingLimit[0].IsSelected);
           // //foreach (var myGame in gametype)
           // //{
           // //    foreach (var bettingLimit in myGame.BettingLimit)
           // //    {
           // //        ((INotifyPropertyChanged)bettingLimit).PropertyChanged +=new PropertyChangedEventHandler(Program_PropertyChanged);
           // //    }
           // //}

           // //((INotifyPropertyChanged)gametype[0].BettingLimit[0]).PropertyChanged += new PropertyChangedEventHandler(Program_PropertyChanged);

           // gametype[0].BettingLimit[0].IsSelected = (gametype[0].BettingLimit[0].IsSelected) ? false : true;
           // gameService.UpdateGameSetting(839043, gametype);
           // Console.Read();
           SettingService service = new SettingService();
           //SuperPTSetting superSetting = service.GetSuperPTSetting(1373389) as SuperPTSetting;

           var superSetting = service.GetAgentPTSetting(5558333, 5558331) as PTSetting;
           var b = superSetting.IsValid;
           //((INotifyPropertyChanged)superSetting).PropertyChanged += new PropertyChangedEventHandler(Program_PropertyChanged);
            
            ((INotifyPropertyChanged)superSetting).PropertyChanged
                 += (o, ar) =>
                 {                  


                     if (!superSetting.PropertiesChangedList.Contains(ar.PropertyName))
                     {
                         superSetting.PropertiesChangedList.Add(ar.PropertyName);
                     }
                 };

            superSetting.Comm = 0.05m;
            //superSetting.MaxPT = 0.7m;
            
            //var s = superSetting.PropertiesChangedList;
            

            //var memberSetting = service.MasterGetMemberPTSetting(1108009, 839043);

            //var custInfo = service.GetCustomerInfo(641918);
            //PTSetting setting = (PTSetting)service.GetAgentPTSetting(4137514, 1678160);

            //setting.Comm = 0;
            //setting.PresetPT = 0.7M;
            //setting.AutoPresetPT = 0;
            //setting.IsAutoPresetPT = false;
            //setting.MaxPT = 0.1m;
            //setting.MinPT = 0.1m;


            RequestContext request = new RequestContext();
            request.CustId = 5558287;
            request.SignUpIp = "192.168.1.1";
            request.Language = "E";
            request.UplineId = 5558284;
            request.UserId = 8;
            service.UpdateMemberPTSetting(request, superSetting);
            
            ////service.UpdateSuperPTSetting(request, superSetting);

            //var result = service.UpdateSuperPTSetting(request, superSetting);
             //service.UpdateSuperPTSetting();
            
            //var maserseting = service.GetMasterPTSeting(642679, 641918);
            //service.UpdateMasterPTSetting(request, maserseting);

            //service.UpdateMasterPTSetting(request, maserseting, new List<int> { 642679, 642843, 642891, 643829 });
        }

        static void Program_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                (sender as StakeLevel).IsDirty = true;
            }
            
            Console.WriteLine((sender as StakeLevel).Id
                + ":" + (sender as StakeLevel).Text
                + " " + (sender as StakeLevel).IsSelected.ToString() 
                + ":" + (sender as StakeLevel).IsDirty.ToString());
        }


    }
}
