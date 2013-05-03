namespace Fanex.PTEngine.BusinessObjects
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Reflection;
    using System.Xml.Linq;
    using Fanex.Common.Extensions;
    using Fanex.PTEngine.XmlMapping;

    public class BettingLimitItem : IBettingLimitItem
    {
        public int Id { get; set; }

        public int BetTypeId { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public string ResourceKey { get; set; }

        public decimal? MinBet { get; set; }

        public decimal? MaxBet { get; set; }

        public decimal? MaxPerMatch { get; set; }

        public decimal? MaxPerBall { get; set; }

        public decimal? MinBetMax { get; set; }

        public decimal? MaxBetMin { get; set; }

        public decimal? MaxPerMatchMin { get; set; }

        public decimal? MaxPerMatchMax { get; set; }

        public decimal? MaxPerBallMax { get; set; }

        public decimal? MaxPerBallMin { get; set; }

        public BettingLimitItem()
        {
        }

        public BettingLimitItem(XElement element)
            : this()
        {
            Map(element);
        }

        public void Map(XElement element)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                switch (attribute.Name.LocalName)
                {
                    case Constant.SPORT_TYPE_ID:
                        Id = int.Parse(attribute.Value);
                        break;
                    case Constant.SPORT_TYPE_NAME:
                        Name = attribute.Value;
                        break;
                    case Constant.SPORT_TYPE_KEY:
                        Key = attribute.Value;
                        break;
                    case Constant.SPORT_TYPE_RESOURCE_KEY:
                        ResourceKey = attribute.Value;
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Name))
                {
                    Key = Name;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        /// <param name="mappingSection"></param>
        public void ReadFromDataReader(IRequestContext context, IDataReader reader, BettingLimitMappingSection mappingSection)
        {
            BettingLimitMappingSection bettingLimitMapping = BettingLimitMappingSection.Instance;
            foreach (BettingLimitElement bettingLimitElement in bettingLimitMapping.BetSettingCollection)
            {
                if (bettingLimitElement.Type == Enum.GetName(typeof(MappingType), MappingType.Out))
                {
                    // If super use config of super, other levels use same config
                    int targetRoleId = 0;
                    if (context.TargetRoleId == Constant.SPECIAL_TARGET_ROLE_ID)
                    {
                        targetRoleId = context.TargetRoleId;
                    }

                    PropertyInfo[] propertyInfos;
                    propertyInfos = typeof(BettingLimitItem).GetProperties();

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (propertyInfo.Name == bettingLimitElement.MappingTarget)
                        {
                            // Replace pattern
                            string formattedPattern = FormatPattern(bettingLimitElement, this.Id, MappingType.Out);

                            if (reader.HasColumn(formattedPattern))
                            {
                                propertyInfo.SetValue(this, reader.ToDecimal(formattedPattern), null);
                            }
                        }
                    }
                }
            }

            // For Add  New case, set current value = max value
            if (context.FormId == FormType.Add)
            {
                MinBet = MinBetMax;
                MaxBet = MaxBetMin;
                MaxPerMatch = MaxPerMatchMax;
                MaxPerBall = MaxPerBallMax;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="parameters"></param>
        /// <param name="mappingSection"></param>
        public void MapToSpParameters(IRequestContext context, Hashtable parameters, BettingLimitMappingSection mappingSection)
        {
            BettingLimitMappingSection bettingLimitMapping = BettingLimitMappingSection.Instance;
            foreach (BettingLimitElement bettingLimitElement in bettingLimitMapping.BetSettingCollection)
            {
                if (bettingLimitElement.Type == Enum.GetName(typeof(MappingType), MappingType.In))
                {
                    PropertyInfo[] propertyInfos;
                    propertyInfos = typeof(BettingLimitItem).GetProperties();

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        if (propertyInfo.Name == bettingLimitElement.MappingTarget)
                        {
                            // Replace pattern
                            string formattedPattern = FormatPattern(bettingLimitElement, this.Id, MappingType.In);

                            // Add parameters
                            object value = propertyInfo.GetValue(this, null);
                            if (value != null)
                            {
                                parameters.Add(formattedPattern, value);
                            }
                        }
                    }
                }
            }
        }

        private string FormatPattern(BettingLimitElement mappingElement, int sportTypeId, MappingType type)
        {
            string formattedPattern = mappingElement.Pattern;
            if (this.Id == Constant.SPECIAL_SPORT_TYPE_ID && this.Key == Constant.SPECIAL_SPORT_TYPE_NAME)
            {
                formattedPattern = formattedPattern.Replace(Constant.SPORT_ID_MAP, string.Format(mappingElement.SportId.Format, sportTypeId.ToString() + Constant.SPECIAL_SPORT_TYPE_STRING));
            }
            else
            {
                // Format sport type id
                if (int.Parse(mappingElement.SportId.OmitWhenDefault) == sportTypeId)
                {
                    formattedPattern = formattedPattern.Replace(Constant.SPORT_ID_MAP, string.Empty);
                }
                else
                {
                    formattedPattern = formattedPattern.Replace(Constant.SPORT_ID_MAP, string.Format(mappingElement.SportId.Format, sportTypeId));
                }
            }

            return formattedPattern;
        }
    }
}