//<copyright file="SuperPTSettingValidator.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.4">Created</change>
//</history>


namespace Fanex.LiveCasinoSetting.Validator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.LiveCasinoSetting.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class SuperPTSettingValidator : PTSettingBaseValidator<SuperPTSetting>
    {
        private static volatile SuperPTSettingValidator instance;
        private static object syncRoot = new Object();

        public static SuperPTSettingValidator Rules
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SuperPTSettingValidator();
                    }
                }

                return instance;
            }
        }
    }
}
