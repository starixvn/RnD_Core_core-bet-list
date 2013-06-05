// <copyright file="SettingServiceBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
// <history>
//  <change who="Anders" date="2012.01.11">Seperate game setting to another service</change>
//  <change who="Anders" date="2012.01.18">Add function process exception</change>
// </history>

using System;
using Fanex.Logging;
namespace Fanex.LiveCasinoSetting.Business
{
    /// <summary>
    /// contain default settings for service layer
    /// </summary>
    public class SettingServiceBase
    {
        public ILogger Logger { get; set; }

        public const string ExceptionPolicy = "LiveCasinoPolicy";
        private const string DefaultLogCategory = "LiveCasino";

        protected void ProcessException(Exception ex)
        {
            if (Logger != null)//user for unitest only
            {
                Logger.Error(ex.Message, DefaultLogCategory);
            }
            else
            {
                LogService.Error(ex.Message, DefaultLogCategory);
                throw ex;
            }            
        }
    }
}
