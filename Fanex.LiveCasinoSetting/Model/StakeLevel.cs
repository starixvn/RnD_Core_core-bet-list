// <copyright file="StakeLevel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.8">Created</change>
// </history>

namespace Fanex.LiveCasinoSetting.Model
{
    using System;
    using NotifyPropertyChanged;

    /// <summary>
    /// 
    /// </summary>
    [NotifyPropertyChanged]
    public class StakeLevel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }

        public bool IsDirty { get; set; }
    }
}
