namespace Fanex.PTEngine.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    public interface IBetSetting
    {
        List<BetSettingItem> BetSettingItems { get; set; }

        void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader, List<SportType> sportStypes);

        void MapToSpParameters(Hashtable parameters);
    }
}
