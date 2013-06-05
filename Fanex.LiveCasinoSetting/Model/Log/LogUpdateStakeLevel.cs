//<copyright file="LogUpdateStakeLevel.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.13">Created</change>
//	<change who="anders.le" date="2012.2.13">Reformat tracking template</change>
//</history>



namespace Fanex.LiveCasinoSetting.Model.Log
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Log entity class for action  UpdateStakeLevel_Super = 356,
    /// UpdateStakeLevel_Master = 357,
    /// UpdateStakeLevel_Agent = 358,
    /// UpdateStakeLevel_Member = 359,
    /// </summary>
    public class LogUpdateStakeLevel : LogDataBase, ILogData
    {
        protected const string TrackingTemplate
            = "<TP_User_CustGameType_UpdStakeLevel CustID=\"{0}\" {1} GameTypeId=\"{2}\" BetTypeId=\"{3}\" StakeLevel=\"{4}\" Ip=\"{5}\" Lang=\"{6}\" RoleID=\"{7}\" ErrCode=\"{8}\" ErrMsg=\"{9}\" />";
        private List<GameType> _oldGameTypeValues;
        private List<GameType> _newGameTypeValues;
        private Dictionary<int, Result> _resultDictionary;
        //private dynamic _resultDictionary;
       
        public LogUpdateStakeLevel(List<GameType> oldGameTypeValues, List<GameType> newGameTypeValues
            , Dictionary<int, Result> resultDictionary)
        {
            _oldGameTypeValues = oldGameTypeValues;
            _newGameTypeValues = newGameTypeValues;
            _resultDictionary = resultDictionary;

            ProccessSuccessfulResult();
        }

        #region ILogEntity Members
        protected void ProccessSuccessfulResult()
        {
            var resultList = ((Dictionary<int, Result>)_resultDictionary).ToList();
            resultList.RemoveAll(r => string.IsNullOrWhiteSpace(r.Value.ErrorCode));

            //remove all unsuccessfull results
            foreach (var result in resultList)
            {
                _oldGameTypeValues.RemoveAll(g => g.Id == result.Key);
                _newGameTypeValues.RemoveAll(g => g.Id == result.Key);
            }
        }

        protected string CreateLogData(GameType gameType)
        {
            return string.Format(TrackingTemplate
                , CustId                
                , RoleId == 4 ? string.Empty : string.Format(TrackingSubAcc, SubAccId, SubAccName)
                , gameType.Id
                , gameType.BetTypeId
                , GetSelectedStakeLevelText(gameType.BettingLimit)
                , SignUpIp
                , Language
                , RoleId
                , ErrorCode
                , ErrorMessage);
        }
      

        protected List<string> CreateLogData(List<GameType> gameTypeList)
        {
            List<string> trackingValues = new List<string>();
            gameTypeList.ForEach(gameType => trackingValues.Add(
                    CreateLogData(gameType)
                ));

            return trackingValues;
        }

        public override string SpNameTag
        {
            get
            {
                return "TP_User_CustGameType_UpdStakeLevel";
            }
        }

        public override List<string> NewValues
        {
            get
            {
                return CreateLogData(_newGameTypeValues);
            }
        }

        public override List<string> OldValues
        {
            get
            {
                return CreateLogData(_oldGameTypeValues);
            }
        }

        /// <summary>
        /// Get text of selected stakelevel item
        /// sepereated by commas
        /// </summary>
        /// <param name="stakeLevelList"></param>
        /// <returns></returns>
        private static string GetSelectedStakeLevelText(List<StakeLevel> stakeLevelList)
        {
            string text = string.Empty;
            stakeLevelList
                .Where(s => s.IsSelected)
                .Select(s => s)
                .ToList()
                .ForEach(stakeLevel =>
                {
                    text += stakeLevel.Text + ", ";

                });

            return text.Trim().TrimEnd(',');
        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid;
            }
        }

        public GameType GameType { get; set; }

        #endregion
    }
}
