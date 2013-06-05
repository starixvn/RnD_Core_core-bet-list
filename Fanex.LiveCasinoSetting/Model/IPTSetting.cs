//<copyright file="IPTSetting.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.4">Move property of PTSetting to IPSetting</change>
//</history>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.LiveCasinoSetting.Model
{
    public interface IPTSetting
    {
        decimal MinPT { get; set; }
        decimal MaxPT { get; set; }
        decimal Comm { get; set; }
        decimal PresetPT { get; set; }
        decimal AutoPresetPT { get; set; }
        bool IsAutoPresetPT { get; set; }
        IPTSetting Upline { get; set; }
        bool IsValid { get; }
        bool IsDirty { get; }

        bool IsMinPTChanged { get; }
        bool IsMaxPTChanged { get; }
        bool IsCommChanged { get; }
        bool IsPresetPTChanged { get; }
        bool IsAutoPresetPTChanged { get; }
        bool IsIsAutoPresetPTChanged { get; }
        bool IsUpdateAllMember { get; set; }
        decimal NewMemberPT { get; set; }
        decimal MaxAutoPresetPT { get; set; }
    }
}
