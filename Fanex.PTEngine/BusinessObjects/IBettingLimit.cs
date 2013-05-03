namespace Fanex.PTEngine.BusinessObjects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IBettingLimit<T> where T : IBettingLimitItem
    {
        IList<T> BettingLimitItems { get; set; }

        void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader);

        void MapToSpParameters(IRequestContext context, Hashtable parameters);

        void MapToSpUpdateMultipleParameters(ref Hashtable parameters, Hashtable updateParameters);

        void Populate();
    }
}