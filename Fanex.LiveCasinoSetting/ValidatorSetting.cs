//<copyright file="ValidatorSetting.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.3">Add Error Code</change>
//</history>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.LiveCasinoSetting
{
    internal static class ValidatorSetting
    {
        internal const string RULE_PTSETTING_GENERAL = "PT Setting General";
        internal const string RUEL_PTSETTING_LEVEL = "PT Setting";
        internal const string RULE_REQUEST_CONTEXT = "ReqeuestContext";

        internal const decimal MinComm_Default = 0.0m;
        internal const decimal MaxComm_Default = 0.008m;
        internal const decimal MaxPT_Default = 0.9m;
        internal const decimal MinPT_Default = 0.0m;
    }

    public static class ErrorCode
    {
        public const string NOT_UPDATE_PT_SETTING = "CS0000";
        public const string NOT_UPDATE_BETTING_LIMIT = "GS0000";
    }
}
