namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections;

    public interface IStatusSetting
    {
        /// <summary>
        /// Racing status
        /// </summary>
        bool Disable151_31 { get; set; }

        bool Disable151_41 { get; set; }

        bool Disable151_51 { get; set; }

        bool Disable152_41 { get; set; }

        bool Disable152_51 { get; set; }

        bool Disable153_51 { get; set; }

        bool DisableFinancial { get; set; }

        bool DisableCasino { get; set; }

        bool SyncCasino { get; set; }

        bool DisableBingo { get; set; }

        bool SyncBingo { get; set; }

        /// <summary>
        /// Number Game status
        /// </summary>
        bool DisableNumber { get; set; }

        bool DisableP2P { get; set; }

        bool DisableFUNP2P { get; set; }

        /// <summary>
        /// Live Casino status
        /// </summary>
        bool Disable162_1101 { get; set; }

        bool IsInternal { get; set; }

        /// <summary>
        /// Do not allow master take auto position taking on agent or member
        /// </summary>
        bool DisableAutoMPT { get; set; }

        bool Suspended { get; set; }

        bool Closed { get; set; }

        bool UplineClosed { get; set; }

        bool DisableVirtualSports { get; set; }

        bool HideVirtualSports { get; set; }

        /// <summary>
        /// update DisableVirtualSports on the target user & also on its downlines or not
        /// </summary>
        bool DisableDownlinesVirtualSports { get; set; }

        /// <summary>
        /// create a constraint: target user won't be enabled Virtual Sports if the its upline is currently disabled Virtual Sports
        /// </summary>
        bool DisableUplineVirtualSports { get; set; }

        void ReadFromDataRecord(System.Data.IDataReader reader);

        void MapToSpParameters(Hashtable parameters);
    }
}