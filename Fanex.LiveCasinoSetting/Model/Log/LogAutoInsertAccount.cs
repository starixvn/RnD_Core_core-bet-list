// -----------------------------------------------------------------------
// <copyright file="LogAutoInsertAccount.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.LiveCasinoSetting.Model.Log
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogAutoInsertAccount : LogManualInsertAccount, ILogData
    {
        private const string TrackingTemplate
           = "<TP_User_Customer_AutoInsertAccount CustID=\"{0}\" UserName=\"{1}\" CasinoNo=\"{2}\" ExchangeID=\"{3}\" SCustID=\"{4}\" MCustID=\"{5}\" ACustID=\"{6}\" RoleID=\"{7}\" SignupIP=\"{8}\" SignupBy=\"{9}\" AddFrom=\"{10}\" Lang=\"{11}\" ErrCode=\"{12}\" ErrMsg=\"{13}\" {14} >{15}</TP_User_Customer_AutoInsertAccount>";

        private const string GameTypeTemplate
            = "<GameType GameTypeId=\"{0}\" BetTypeId=\"{1}\" StakeLevel=\"{2}\" />";

        public DataTable dataResult { get; set; }

        public override string SpNameTag
        {
            get
            {
                return "TP_User_Customer_AutoInsertAccount";
            }
        }

        /// <summary>
        /// Generate string template <see cref="GameTypeTemplate"/>
        /// </summary>
        /// <returns></returns>
        private string GetGameTypeLogData()
        {
            StringBuilder gameTypeLog = new StringBuilder();
            if (dataResult != null && dataResult.Rows.Count>0)
            {
                DataView rows = dataResult.DefaultView; 
                dataResult.DefaultView.Sort = "IBCBetTypeID";

                int preBetTypeID = Convert.ToInt32(rows[0]["IBCBetTypeID"]);
                int preGameTypeID = Convert.ToInt32(rows[0]["GameTypeID"]);
                int currentGameTypeID = preGameTypeID;
                int currentBetTypeId = preBetTypeID;
                string stakeLevelName = rows[0]["StakeLevelName"].ToString();
                int length = rows.Count;

                for (int i = 1; i < length; i++)
                {
                    var row = rows[i];
                    currentBetTypeId = Convert.ToInt32(row["IBCBetTypeID"]);                    
                    preGameTypeID = Convert.ToInt32(row["GameTypeID"]);
                    
                    if (currentBetTypeId != preBetTypeID)
                    {
                        gameTypeLog.AppendFormat(GameTypeTemplate, preGameTypeID, preBetTypeID, stakeLevelName.TrimStart().TrimStart(','));
                        stakeLevelName = string.Empty;
                        preBetTypeID = currentBetTypeId;
                        preGameTypeID = currentGameTypeID;
                    }

                    stakeLevelName = string.Join(", ", stakeLevelName, row["StakeLevelName"].ToString());
                }

                gameTypeLog.AppendFormat(GameTypeTemplate, preGameTypeID, preBetTypeID, stakeLevelName.TrimStart().TrimStart(','));
            }

            return gameTypeLog.ToString();
        }

        private string GetTrackingValue()
        {
            string gameTypeLog = GetGameTypeLogData();
            string trackingValue = string.Format(TrackingTemplate
            , CustId
            , CustUserName
            , CasinoNo
            , ExchangeId
            , SuperId
            , MasterId
            , AgentId
            , RoleId
            , SignUpIp
            , UserId
            , SignUpForm
            , Language
            , ErrorCode
            , ErrorMessage
            , RoleId == 4 ? string.Empty : string.Format(TrackingSubAcc, SubAccId, SubAccName)
            , gameTypeLog);

            return trackingValue;
        }


        public override List<string> NewValues
        {
            get
            {
                return new List<string> { GetTrackingValue() };
            }
        }

        public override List<string> OldValues
        {
            get
            {
                return new List<string> { string.Empty };
            }
        }
    }
}
