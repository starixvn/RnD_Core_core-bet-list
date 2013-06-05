// <copyright file="PTSetting.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  PT Setting information, used by Master, Agent, and Member level
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.10">Created</change>
//  <change who="Anders" date="2012.01.11">Inherit from PTSettingBase</change>
//	<change who="anders.le" date="2012.2.2">Add NotifyPropertyChanged</change>
//	<change who="anders.le" date="2012.2.3">Implement IsDirty property</change>
//	<change who="anders.le" date="2012.2.3">Add IsDisabledAutoPresetPT, refactor tracking property change</change>
//	<change who="anders.le" date="2012.2.10">Add validator result</change>
// </history>

namespace Fanex.LiveCasinoSetting.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NotifyPropertyChanged;
    using Fanex.LiveCasinoSetting.Validator;
using FluentValidation.Results;

    /// <summary>
    /// PT setting information for Master, Agent, and Member level
    /// </summary>
    [NotifyPropertyChanged]
    public class PTSetting : SuperPTSetting
    {
        public PTSetting() { }


        public PTSetting(bool IsDisabledAutoPresetPT)
        {
            _IsDisabledAutoPresetPT = IsDisabledAutoPresetPT;
        }        

        private bool _IsDisabledAutoPresetPT;
        public bool IsDisabledAutoPresetPT
        {
            get
            {
                return _IsDisabledAutoPresetPT;
            }
        }
      

        public override ValidationResult ValidationResult
        {
            get
            {
                var result = PTSettingValidator.Rules.Validate(this);

                return result;
            }
        }
      
       
    }
}
