// <copyright file="DbSettingProviderBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// <summary>
//
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.27">Created & Implement</change>
//  <change who="Anders" date="2011.12.28">Clone implement to XMLDbSettingProvider</change>
//  <change who="Anders" date="2011.12.30">Add updateSetting, refresh Cache</change>
//	<change who="anders.le" date="2012.2.28">Get current application domain base directory</change>
//  <change who="andy.nguyen" date="2012.4.5">Modify method GetSetting(string key) to check whether the key is existed or not in dictionary</change>
// </history>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using System.Linq;

namespace Fanex.Data.Configuration
{
    /// <summary>
    /// Define methods to interacte with datasource
    /// </summary>
    public abstract class DbSettingProviderBase : ProviderBase
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            _resourcePath = GetResourcePath(config);
        }

        /// <summary>
        /// Returns true if the given file path is a folder.
        /// </summary>
        /// <param name="Path">File path</param>
        /// <returns>True if a folder</returns>
        protected bool IsFolder(string path)
        {
            return ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory);
        }

        /// <summary>
        /// get resouce path point to extenal config datasource
        /// </summary>
        /// <param name="config">NameValueCollection config</param>
        /// <returns>return absolute resouce path</returns>
        private string GetResourcePath(NameValueCollection config)
        {
            var resourcePath = config["resourcePath"];
            if (string.IsNullOrWhiteSpace(resourcePath))
                throw new NullReferenceException(Resources.ResourcePathHasNotBeenSetYet);

            // check root path http://msdn.microsoft.com/query/dev10.query?appId=Dev10IDEF1&l=EN-US&k=k%28SYSTEM.IO.PATH.ISPATHROOTED%29;k%28SOLUTIONITEMSPROJECT%29;k%28TargetFrameworkMoniker-%22.NETFRAMEWORK%2cVERSION%3dV4.0%22%29;k%28DevLang-CSHARP%29&rd=true
            if (!Path.IsPathRooted(resourcePath))
            {
                if (resourcePath.StartsWith("~"))
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        resourcePath = System.Web.HttpContext.Current.Server.MapPath(resourcePath);
                    }
                }
                else
                {
                    // if got the problem with window service
                    // see http://haacked.com/archive/2004/06/29/706.aspx
                    resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourcePath);
                }
            }

            EnsureResourceExist(resourcePath);
            return resourcePath;
        }

        /// <summary>
        /// Ensures the resource exist.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        private void EnsureResourceExist(string resourcePath)
        {
            if (IsFolder(resourcePath))
            {
                if (!Directory.Exists(resourcePath))
                    throw new DirectoryNotFoundException(resourcePath);

                if (Directory.GetFiles(resourcePath).Length <= 0)
                    throw new IOException(Resources.FolderIsEmpty);
            }
            else
            {
                if (!File.Exists(resourcePath))
                    throw new FileNotFoundException(resourcePath);
            }
        }

        /// <summary>
        /// have to call it when application start
        /// </summary>
        public abstract void Start();

        public virtual void Restart()
        {
            Start();
        }

        /// <summary>
        /// Get setting by key
        /// </summary>
        /// <param name="key">null or whitespace is unacceptable</param>
        /// <returns>IDbSetting</returns>
        public virtual IDbSetting GetSetting(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("key");

            var dataSource = SettingDataSource;

            if (dataSource == null)
                throw new NullReferenceException(Resources.DataSourceIsNull);

            if (!dataSource.ContainsKey(key))
                throw new KeyNotFoundException(string.Join(null, key, ": ", Resources.KeyNotFound));

            return dataSource[key];
        }

        public abstract void InsertSetting(IDbSetting setting);

        public abstract void UpdateSetting(IDbSetting setting);

        public abstract void DeleteSetting(string key);

        public abstract Dictionary<string, IDbSetting> SettingDataSource { get; }

        private string _resourcePath;

        public string ResourcePath
        {
            get { return _resourcePath; }
            set { _resourcePath = value; }
        }
    }
}