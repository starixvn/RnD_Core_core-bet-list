// <copyright file="LoggerConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
//  run fluently config information 
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.14">Created</change>
// </history>


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Fanex.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Fanex.Data.Configuration;

namespace Fanex.Logging.Configuration
{
    internal static class DataConfiguration
    {
        private static volatile DataSettings DataSetting;
        private static volatile bool _runConfig = false;

        static DataConfiguration()
        {
            if (DataSetting == null)
            {
                DataSetting = DataSettings.GetSettings(ConfigurationSourceFactory.Create());
            }
        }

        public static void Run()
        {
            //if (_runConfig) return;

            //var builder = new ConfigurationSourceBuilder();

            //builder.ConfigureLogging()
            //    .SpecialSources.UnprocessedCategory
            //    .SpecialSources.LoggingErrorsAndWarningsCategory
            //    .SpecialSources.AllEventsCategory
            //    .SendTo
            //    .Msmq("MsmqListener").UseQueue(Setting.MsmqPath)
            //    .WithTraceOptions(TraceOptions.Callstack)
            //    .FormatWith(new FormatterBuilder()
            //                    .BinaryFormatterNamed("TextFormatter"));


            //var configSource = new DictionaryConfigurationSource();
            //builder.UpdateConfigurationWithReplace(configSource);
            //EnterpriseLibraryContainer.Current
            //    = EnterpriseLibraryContainer.CreateDefaultContainer(configSource);

            //_runConfig = true;
        }

        public static DataSettings Setting
        {
            get { return DataSetting; }
        }
    }
}
