namespace Fanex.PTEngine.ViewModel
{
    using System;
    using System.Data;
    using Fanex.PTEngine.XmlMapping;
    using Fanex.Common.Extensions;
    using System.Collections;

    public class BetSettingItem
    {
        public decimal? MinBet { get; set; }

        public decimal? MaxBet { get; set; }

        public decimal? MaxPerMatch { get; set; }

        public decimal? MaxPerBall { get; set; }

        public string SportTypeId { get; set; }

        public decimal? MinBet_Max { get; set; }

        public decimal? MaxBet_Min { get; set; }

        public decimal? MaxPerMacht_Min { get; set; }

        public decimal? MaxPerMacht_Max { get; set; }

        public decimal? MaxPerBall_Max { get; set; }

        public decimal? MaxPerBall_Min { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="betSettingElement"></param>
        /// <param name="reader"></param>
        /// <param name="sportTypeId"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private decimal? ReadMinMaxValue(BetSettingElement betSettingElement, System.Data.IDataReader reader, string sportTypeId)
        {
            string formatedString = string.Empty;
            if (sportTypeId == betSettingElement.SportTypeBetSetting.OmitWhenDefault)
            {
                formatedString = string.Empty;
            }
            else
            {
                formatedString = string.Format(betSettingElement.SportTypeBetSetting.Format, sportTypeId);
            }

            string strPattern = betSettingElement.Pattern.Replace(Const.SPORT_ID_MAP, formatedString);

            if (reader.HasColumn(strPattern))
            {
                return (decimal?)reader.ToDecimal(strPattern);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        public void ReadFromDataReader(IRequestContext context, IDataReader reader)
        {
            BetSettingMappingSection betSettingMapping = BetSettingMappingSection.Instance;
            foreach (BetSettingElement betSettingElement in betSettingMapping.BetSettingCollection)
            {
                if (betSettingElement.Type == Enum.GetName(typeof(MappingType), MappingType.Out))
                {
                    // If super use config of super, other level use same config
                    string targetRoleId = string.Empty;
                    if (context.TargetRoleId != Const.SPECIAL_TARGET_ROLE_ID)
                    {
                        targetRoleId = "*";
                    }
                    else
                    {
                        targetRoleId = context.TargetRoleId.ToString();
                    }

                    // Map from reader
                    if (betSettingElement.TargetRoleId == targetRoleId)
                    {
                        switch (betSettingElement.MappingTarget)
                        {
                            case Const.MIN_BET:
                                this.MinBet = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_BET:
                                this.MaxBet = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_PER_MATCH:
                                this.MaxPerMatch = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_PER_BALL:
                                this.MaxPerBall = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MIN_BET_MAX:
                                this.MinBet_Max = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_BET_MIN:
                                this.MaxBet_Min = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_PER_MATCH_MIN:
                                this.MaxPerMacht_Min = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_PER_MATCH_MAX:
                                this.MaxPerMacht_Max = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_PER_BALL_MIN:
                                this.MaxPerBall_Min = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                            case Const.MAX_PER_BALL_MAX:
                                this.MaxPerBall_Max = ReadMinMaxValue(betSettingElement, reader, this.SportTypeId);
                                break;
                        }
                    }
                }
            }
        }

        private string GetParamNameToAddParameter(BetSettingElement betSettingElement, string sportTypeId)
        {
            string formatedString = string.Empty;
            if (sportTypeId == betSettingElement.SportTypeBetSetting.OmitWhenDefault)
            {
                formatedString = string.Empty;
            }
            else
            {
                formatedString = string.Format(betSettingElement.SportTypeBetSetting.Format, sportTypeId);
            }

            return betSettingElement.Pattern.Replace(Const.SPORT_ID_MAP, formatedString);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="context"></param>
        public void MapToSpParameters(Hashtable parameters)
        {
            BetSettingMappingSection betSettingMapping = BetSettingMappingSection.Instance;
            foreach (BetSettingElement betSettingElement in betSettingMapping.BetSettingCollection)
            {
                if (betSettingElement.Type == Enum.GetName(typeof(MappingType), MappingType.In))
                {
                    switch (betSettingElement.MappingTarget)
                    {
                        case Const.MIN_BET:
                            parameters.Add(GetParamNameToAddParameter(betSettingElement, this.SportTypeId), this.MinBet);
                            break;
                        case Const.MAX_BET:
                            parameters.Add(GetParamNameToAddParameter(betSettingElement, this.SportTypeId), this.MaxBet);
                            break;
                        case Const.MAX_PER_MATCH:
                            parameters.Add(GetParamNameToAddParameter(betSettingElement, this.SportTypeId), this.MaxPerMatch);
                            break;
                        case Const.MAX_PER_BALL:
                            if (this.MaxPerBall != null)
                            {
                                parameters.Add(GetParamNameToAddParameter(betSettingElement, this.SportTypeId), this.MaxPerBall);
                            }

                            break;
                    }
                }
            }
        }
    }
}