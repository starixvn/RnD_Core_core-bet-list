namespace Fanex.PTEngine.ViewModel
{
    using System.Collections.Generic;
    using System.Collections;

    public class BetSetting : IBetSetting
    {
        public BetSetting()
        {
            BetSettingItems = new List<BetSettingItem>();
        }

        public List<BetSettingItem> BetSettingItems { get; set; }

        private BetSettingItem CreateNewBetSettingItem(IRequestContext context, System.Data.IDataReader reader, string sportTypeId)
        {
            BetSettingItem tempItem = new BetSettingItem
            {
                SportTypeId = sportTypeId
            };
            tempItem.ReadFromDataReader(context, reader);
            return tempItem;
        }

        /// <summary>
        /// Set data reader to object BetSetting
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader, List<SportType> sportTypes)
        {
            for (int i = 0; i < sportTypes.Count; i++)
            {
                BetSettingItems.Add(CreateNewBetSettingItem(context, reader, sportTypes[i].Id.ToString()));
                if (sportTypes[i].Id == Const.SPECIAL_SPORT_TYPE_ID)
                {
                    BetSettingItems.Add(CreateNewBetSettingItem(context, reader, Const.SPECIAL_SPORT_TYPE_ID_STRING));
                    i++;
                }
            }
        }

        /// <summary>
        /// Mapping to Sp Parameters
        /// </summary>
        /// <param name="parameters"></param>
        public void MapToSpParameters(Hashtable parameters)
        {
            foreach (BetSettingItem betSettingItem in BetSettingItems)
            {
                betSettingItem.MapToSpParameters(parameters);
            }
        }
    }
}