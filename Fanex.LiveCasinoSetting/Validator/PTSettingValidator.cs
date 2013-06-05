//<copyright file="PTSettingValidator.cs" company="Nexcel Solutions Vietnam" >
//Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.1.30">Created</change>
//	<change who="anders.le" date="2012.2.2">Add rule</change>
//</history>


namespace Fanex.LiveCasinoSetting.Validator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using FluentValidation;
    using Fanex.LiveCasinoSetting.Model;

    /// <summary>
    /// TODO: Update summary.
    /// Should create singleton for this sealed class
    /// </summary>
    public sealed class PTSettingValidator : PTSettingBaseValidator<PTSetting>
    {
        private static volatile PTSettingValidator instance;
        private static object syncRoot = new object();

        public static PTSettingValidator Rules
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new PTSettingValidator();
                        }
                    }
                }

                return instance;
            }
        }

        public PTSettingValidator()
        {
            RuleFor(x => x.PresetPT)
                .LessThanOrEqualTo(x => x.Upline.MaxPT);

            //RuleFor(x => x.MaxPT + x.PresetPT)
            //    .LessThanOrEqualTo(x => x.Upline.MaxPT);

            RuleFor(x => x.AutoPresetPT)
                .LessThanOrEqualTo(x => x.Upline.MaxPT);

            //RuleFor(x => x.MaxPT + x.AutoPresetPT)
            //    .LessThanOrEqualTo(x => x.Upline.MaxPT);
        }
    }
}
