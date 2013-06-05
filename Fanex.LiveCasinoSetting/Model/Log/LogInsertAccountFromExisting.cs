// -----------------------------------------------------------------------
// <copyright file="LogAutoInsertAccount.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.LiveCasinoSetting.Model.Log
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogInsertAccountFromExisting : LogManualInsertAccount, ILogData
    {
        private const string TrackingTemplate
           = "<TP_User_Customer_AutoInsertAccountFromExisting CustID=\"{0}\" UserName=\"{1}\" CasinoNo=\"{2}\" ExchangeID=\"{3}\" RoleID=\"{4}\" SignupIP=\"{5}\" SignupBy=\"{6}\" AddFrom=\"{7}\" Lang=\"{8}\" ErrCode=\"{9}\" ErrMsg=\"{10}\" {11} CopiedFromUsername=\"{12}\">{13}</TP_User_Customer_AutoInsertAccountFromExisting>";

        private const string GameTypeTemplate
            = "<GameType GameTypeId=\"{0}\" BetTypeId=\"{1}\" StakeLevel=\"{2}\" />";

        public DataTable dataResult { get; set; }

        public override string SpNameTag
        {
            get
            {
                return "TP_User_Customer_AutoInsertAccountFromExisting";
            }
        }

        /// <summary>
        /// Generate string template <see cref="GameTypeTemplate"/>
        /// </summary>
        /// <returns></returns>
        private string GetGameTypeLogData()
        {
            StringBuilder gameTypeLog = new StringBuilder();
            if (dataResult != null && dataResult.Rows.Count > 0)
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
            , RoleId
            , SignUpIp
            , UserId
            , SignUpForm
            , Language
            , ErrorCode
            , ErrorMessage
            , RoleId == 4 ? string.Empty : string.Format(TrackingSubAcc, SubAccId, SubAccName)
            , CopiedFrom
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