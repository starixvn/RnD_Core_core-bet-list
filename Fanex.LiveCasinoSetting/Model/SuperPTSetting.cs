// <copyright file="PTSettingBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  PT Setting general information, it used by Super level
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.11">Created</change>
//	<change who="anders.le" date="2012.2.2">Add NotifyPropertyChanged</change>
//	<change who="anders.le" date="2012.2.3">Implement IsDirty property</change>
//	<change who="anders.le" date="2012.2.10">Add validator result</change>
// </history>

namespace Fanex.LiveCasinoSetting.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NotifyPropertyChanged;
    using Fanex.LiveCasinoSetting.Validator;
using FluentValidation.Results;

    [NotifyPropertyChanged]
    public class SuperPTSetting : IPTSetting
    {
        public List<string> PropertiesChangedList;

        public SuperPTSetting()
        {
            PropertiesChangedList = new List<string>();
        }

        public decimal MinPT { get; set; }
        public decimal MaxPT { get; set; }
        public decimal Comm { get; set; }
        public decimal PresetPT { get; set; }
        public decimal AutoPresetPT { get; set; }
        public bool IsAutoPresetPT { get; set; }
        public IPTSetting Upline { get; set; }
        public bool IsUpdateAllMember { get; set; }
        public decimal NewMemberPT { get; set; }
        public decimal MaxAutoPresetPT { get; set; }

        /// <summary>
        /// get valid status of pt setting
        /// </summary>
        public bool IsValid
        {
            get
            {
                return ValidationResult.IsValid;
            }
        }

        
        public virtual ValidationResult ValidationResult
        {
            get
            {
                var result = SuperPTSettingValidator.Rules.Validate(this);

                return result;
            }
        }

        /// <summary>
        /// to indicate whether object has change or not
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return IsMinPTChanged 
                    || IsMaxPTChanged 
                    || IsCommChanged 
                    || IsPresetPTChanged
                    || IsAutoPresetPTChanged
                    || IsIsAutoPresetPTChanged
                    || (this.Upline != null && this.Upline.IsDirty);
            }
        }



        #region Tracking Properties
        public bool IsMinPTChanged
        {
            get
            {
                return PropertiesChangedList.Contains("MinPT", StringComparer.OrdinalIgnoreCase);
            }
        }
        public bool IsMaxPTChanged
        {
            get
            {
                return PropertiesChangedList.Contains("MaxPT", StringComparer.OrdinalIgnoreCase);
            }
        }

        public bool IsCommChanged
        {
            get
            {
                return PropertiesChangedList.Contains("Comm", StringComparer.OrdinalIgnoreCase);
            }
        }

        public bool IsPresetPTChanged
        {
            get
            {
                return PropertiesChangedList.Contains("PresetPT", StringComparer.OrdinalIgnoreCase);
            }
        }

        public bool IsAutoPresetPTChanged
        {
            get
            {
                return PropertiesChangedList.Contains("AutoPresetPT", StringComparer.OrdinalIgnoreCase);
            }
        }

        public bool IsIsAutoPresetPTChanged
        {
            get
            {
                return PropertiesChangedList.Contains("IsAutoPresetPT", StringComparer.OrdinalIgnoreCase);
            }
        }
        #endregion


    }
}
