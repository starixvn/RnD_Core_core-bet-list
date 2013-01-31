// --------------------------------------------------------------------------
// <copyright file="ConnectionStringDataAccess.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.ConnectionWatcher
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    internal class ConnectionStringDataAccess : IDisposable
    {
        #region Memeber

        private string _connectionString;
        private SqlConnection _connection;
        private SqlCommand _command;
        private IDataReader _result;
        private SqlWatcherNotificationType _notificationType;

        #endregion

        #region Ctor

        public ConnectionStringDataAccess(string connectionString, SqlCommand command, SqlWatcherNotificationType notificationType)
        {
            this._notificationType = notificationType;
            this._connectionString = connectionString;
            SqlDependency.Start(this._connectionString);
            this._connection = new SqlConnection(this._connectionString);
            this._command = command;
            this._command.Connection = this._connection;
        }

        #endregion

        /// <summary>
        /// Start
        /// </summary>
        public void Start()
        {
            RegisterForChanges();
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop()
        {
            SqlDependency.Stop(this._connectionString);
        }

        public delegate void SqlWatcherEventHandler(IDataReader Result);

        public event SqlWatcherEventHandler OnChange;

        public IDataReader DataSet
        {
            get { return _result; }
        }

        private void RegisterForChanges()
        {
            try
            {
                if (this._connection.State == ConnectionState.Closed)
                {
                    this._connection.Open();
                }

                //Remove old dependency object
                this._command.Notification = null;
                //Create new dependency object
                SqlDependency dep = new SqlDependency(this._command);
                dep.OnChange += new OnChangeEventHandler(Handle_OnChange);
                //Save data            
                _result = this._command.ExecuteReader();
                //Notify client of change to DataSet
                switch (_notificationType)
                {
                    case SqlWatcherNotificationType.Blocking:
                        OnChange(_result);
                        break;
                    case SqlWatcherNotificationType.Threaded:
                        ThreadPool.QueueUserWorkItem(ChangeEventWrapper, _result);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this._connection.Close();
            }
        }

        public void ChangeEventWrapper(object state)
        {
            IDataReader Result = (IDataReader)state;
            OnChange(Result);
        }

        private void Handle_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type != SqlNotificationType.Change)
                throw new ApplicationException("Failed to create queue notification subscription!");

            //Clean up the old notification
            SqlDependency dep = (SqlDependency)sender;
            dep.OnChange -= Handle_OnChange;

            //Register for the new notification
            RegisterForChanges();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
