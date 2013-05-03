namespace Fanex.PTEngine.BusinessObjects
{
    using System;
    using System.Collections;

    public class StatusSetting : IStatusSetting
    {
        /// <summary>
        /// Racing status
        /// </summary>
        public bool Disable151_31 { get; set; }

        public bool Disable151_41 { get; set; }

        public bool Disable151_51 { get; set; }

        public bool Disable152_41 { get; set; }

        public bool Disable152_51 { get; set; }

        public bool Disable153_51 { get; set; }

        public bool DisableFinancial { get; set; }

        public bool DisableCasino { get; set; }

        public bool SyncCasino { get; set; }

        public bool DisableBingo { get; set; }

        public bool SyncBingo { get; set; }

        /// <summary>
        /// Number Game status
        /// </summary>
        public bool DisableNumber { get; set; }

        public bool DisableP2P { get; set; }

        public bool DisableFUNP2P { get; set; }

        /// <summary>
        /// Live Casino status
        /// </summary>
        public bool Disable162_1101 { get; set; }

        public bool IsInternal { get; set; }

        /// <summary>
        /// Do not allow master take auto position taking on agent or member
        /// </summary>
        public bool DisableAutoMPT { get; set; }

        public bool Suspended { get; set; }

        public bool Closed { get; set; }

        public bool UplineClosed { get; set; }

        public bool UplineSuspended { get; set; }

        public bool DisableVirtualSports { get; set; }

        public bool HideVirtualSports { get; set; }

        public bool DisableDownlinesVirtualSports { get; set; }

        public bool DisableUplineVirtualSports { get; set; }

        /// <summary>
        /// Set data reader to object StatusSetting
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public void ReadFromDataRecord(System.Data.IDataReader reader)
        {
            SyncCasino = reader.ToBoolean("synccasino");
            SyncBingo = reader.ToBoolean("syncbingo");
            Closed = reader.ToBoolean("closed");
            Suspended = reader.ToBoolean("suspended");
            DisableAutoMPT = reader.ToBoolean("disableautompt");
            UplineClosed = reader.ToBoolean("uplineclosed");
            UplineSuspended = reader.HasColumn("uplinesuspended") ? reader.ToBoolean("uplinesuspended") :
                                reader.HasColumn("msuspended") ? reader.ToBoolean("msuspended") :
                                reader.HasColumn("asuspended") ? reader.ToBoolean("asuspended") :  reader.ToBoolean("suspended");
        }

        /// <summary>
        /// Mapping to Sp Parameters
        /// </summary>
        /// <param name="parameters"></param>
        public void MapToSpParameters(Hashtable parameters)
        {
            parameters.Add("synccasino", SyncCasino);
            parameters.Add("syncbingo", SyncBingo);
            parameters.Add("closed", Closed);
            parameters.Add("suspended", Suspended);
            parameters.Add("disableautompt", !DisableAutoMPT);
        }
    }
}