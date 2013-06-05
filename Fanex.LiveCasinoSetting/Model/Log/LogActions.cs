//<copyright file="LogActions.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.9">Created</change>
//</history>


namespace Fanex.LiveCasinoSetting.Model.Log
{
    /// <summary>
    /// naming convention actionid is level int value
    /// UpdateStakeLevel_Super = 356,
    /// UpdateStakeLevel_Master = 357,
    /// UpdateStakeLevel_Agent = 358,
    /// UpdateStakeLevel_Member = 359,
    /// AutoInsert_Master = 364,
    /// AutoInsert_Agent = 365,
    /// AutoInsert_Member = 366,
    ///  ManualInsert_Super = 360,
    ///  ManualInsert_Master = 361,
    ///  ManualInsert_Agent = 362,
    ///  ManualInsert_Member = 363
    /// </summary>
    public enum LogActions : int
    {
        UpdateStakeLevel_Super = 356,
        UpdateStakeLevel_Master = 357,
        UpdateStakeLevel_Agent = 358,
        UpdateStakeLevel_Member = 359,

        AutoInsert_Master = 364,
        AutoInsert_Agent = 365,
        AutoInsert_Member = 366,

        ManualInsert_Super = 360,
        ManualInsert_Master = 361,
        ManualInsert_Agent = 362,
        ManualInsert_Member = 363,

        AutoInsertExisting_Master = 367,
        AutoInsertExisting_Agent = 368,
        AutoInsertExisting_Member = 369
    }
}
