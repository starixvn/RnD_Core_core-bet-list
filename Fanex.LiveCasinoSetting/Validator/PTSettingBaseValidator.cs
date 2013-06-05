//<copyright file="PTSettingBaseValidator.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.4">Created</change>
//</history>

using FluentValidation;
using System;
using System.Linq;
using Fanex.LiveCasinoSetting.Model;


namespace Fanex.LiveCasinoSetting.Validator
{
    /// <summary>
    /// <see cref="http://stackoverflow.com/questions/4746390/fluent-validations-inheritos-validations-rules"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PTSettingBaseValidator<T> : AbstractValidator<T> where T : IPTSetting
    {

        protected PTSettingBaseValidator()
        {
            RuleFor(setting => setting.Upline).NotNull();

            RuleFor(setting => setting.Comm)
                .GreaterThanOrEqualTo(ValidatorSetting.MinComm_Default)
                .LessThanOrEqualTo(setting => setting.Upline.Comm);

            RuleFor(setting => setting.MinPT)
                .GreaterThanOrEqualTo(ValidatorSetting.MinPT_Default)
                .LessThanOrEqualTo(setting => setting.MaxPT);

            RuleFor(setting => setting.MaxPT)
                .LessThanOrEqualTo(setting => setting.Upline.MaxPT)
                .GreaterThanOrEqualTo(setting => setting.MinPT);

        }
    }
}
