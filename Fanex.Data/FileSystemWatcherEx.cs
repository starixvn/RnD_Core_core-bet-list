// <copyright file="FileSystemWatcherEx.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
//
// <history>
// <change who="anders.le" date="2012.3.21">Created</change>
// </history>

namespace Fanex.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Extend FileSystemWatcher, avoid duplicate event raising
    /// <see cref="http://www.codeproject.com/Articles/102493/Enhanced-FileSystemWatcher"/>
    /// </summary>
    public class FileSystemWatcherEx : FileSystemWatcher, IDisposable
    {
        #region Private Members

        /// <summary>
        /// This Dictionary keeps the track of when an event occured last for a particular file
        /// </summary>
        private Dictionary<string, DateTime> lastFileEvent;

        /// <summary>
        /// Interval in Millisecond
        /// </summary>
        private int interval;

        /// <summary>
        /// Timespan created when interval is set
        /// </summary>
        private TimeSpan recentTimeSpan;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemWatcherEx"/> class.
        /// </summary>
        public FileSystemWatcherEx()
            : base()
        {
            this.InitializeMembers();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemWatcherEx"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FileSystemWatcherEx(string path)
            : base(path)
        {
            this.InitializeMembers();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemWatcherEx"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="filter">The filter.</param>
        public FileSystemWatcherEx(string path, string filter)
            : base(path, filter)
        {
            this.InitializeMembers();
        }
        #endregion

        #region Events
        // These events hide the events from the base class. 
        // We want to raise these events appropriately and we do not want the 
        // users of this class subscribing to these events of the base class accidentally

        /// <summary>
        /// Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path"/> is changed.
        /// </summary>
        public new event FileSystemEventHandler Changed;

        /// <summary>
        /// Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path"/> is created.
        /// </summary>
        public new event FileSystemEventHandler Created;

        /// <summary>
        /// Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path"/> is deleted.
        /// </summary>
        public new event FileSystemEventHandler Deleted;

        /// <summary>
        /// Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path"/> is renamed.
        /// </summary>
        public new event RenamedEventHandler Renamed;
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the interval to consider recent events
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public int Interval
        {
            get
            {
                return this.interval;
            }

            set
            {
                this.interval = value;

                // Set timespan based on the value passed
                this.recentTimeSpan = new TimeSpan(0, 0, 0, 0, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [filter recent events].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [filter recent events]; otherwise, <c>false behavior the same <see cref="FileSystemWatcher"/></c>.
        /// </value>
        public bool FilterRecentEvents { get; set; }

        /// <summary>
        /// Releases all resources used by the <see cref="T:System.ComponentModel.Component"/>.
        /// </summary>
        public new void Dispose()
        {
            base.Dispose();
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="E:System.IO.FileSystemWatcher.Changed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs"/> that contains the event data.</param>
        protected new virtual void OnChanged(FileSystemEventArgs e)
        {
            if (this.Changed != null)
            {
                this.Changed(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.IO.FileSystemWatcher.Created"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs"/> that contains the event data.</param>
        protected new virtual void OnCreated(FileSystemEventArgs e)
        {
            if (this.Created != null)
            {
                this.Created(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs"/> that contains the event data.</param>
        protected new virtual void OnDeleted(FileSystemEventArgs e)
        {
            if (this.Deleted != null) 
            {
                this.Deleted(this, e); 
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.IO.RenamedEventArgs"/> that contains the event data.</param>
        protected new virtual void OnRenamed(RenamedEventArgs e)
        {
            if (this.Renamed != null)
            {
               this.Renamed(this, e);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// This Method Initializes the private members.
        /// Interval is set to its default value of 100 millisecond
        /// FilterRecentEvents is set to true, _lastFileEvent dictionary is initialized
        /// We subscribe to the base class events.
        /// </summary>
        private void InitializeMembers()
        {
            this.Interval = 100;
            this.FilterRecentEvents = true;
            this.lastFileEvent = new Dictionary<string, DateTime>();

            base.Created += new FileSystemEventHandler(this.OnCreated);
            base.Changed += new FileSystemEventHandler(this.OnChanged);
            base.Deleted += new FileSystemEventHandler(this.OnDeleted);
            base.Renamed += new RenamedEventHandler(this.OnRenamed);
        }

        /// <summary>
        /// This method searches the dictionary to find out when the last event occured 
        /// for a particular file. If that event occured within the specified timespan
        /// it returns true, else false
        /// </summary>
        /// <param name="fileName">The filename to be checked</param>
        /// <returns>True if an event has occured within the specified interval, False otherwise</returns>
        private bool HasAnotherFileEventOccuredRecently(string fileName)
        {
            bool retVal = false;

            // Check dictionary only if user wants to filter recent events otherwise return Value stays False
            if (this.FilterRecentEvents)
            {
                if (this.lastFileEvent.ContainsKey(fileName))
                {
                    // If dictionary contains the filename, check how much time has elapsed
                    // since the last event occured. If the timespan is less that the 
                    // specified interval, set return value to true 
                    // and store current datetime in dictionary for this file
                    DateTime lastEventTime = this.lastFileEvent[fileName];
                    DateTime currentTime = DateTime.Now;
                    TimeSpan timeSinceLastEvent = currentTime - lastEventTime;
                    retVal = timeSinceLastEvent < this.recentTimeSpan;
                    this.lastFileEvent[fileName] = currentTime;
                }
                else
                {
                    // If dictionary does not contain the filename, 
                    // no event has occured in past for this file, so set return value to false
                    // and annd filename alongwith current datetime to the dictionary
                    this.lastFileEvent.Add(fileName, DateTime.Now);
                    retVal = false;
                }
            }

            return retVal;
        }

        #region FileSystemWatcher EventHandlers
        // Base class Event Handlers. Check if an event has occured recently and call method
        // to raise appropriate event only if no recent event is detected

        /// <summary>
        /// Called when [changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.IO.FileSystemEventArgs"/> instance containing the event data.</param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (!this.HasAnotherFileEventOccuredRecently(e.FullPath))
            {
                this.OnChanged(e);
            }
        }

        /// <summary>
        /// Called when [created].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.IO.FileSystemEventArgs"/> instance containing the event data.</param>
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (!this.HasAnotherFileEventOccuredRecently(e.FullPath))
            {
                this.OnCreated(e);
            }
        }

        /// <summary>
        /// Called when [deleted].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.IO.FileSystemEventArgs"/> instance containing the event data.</param>
        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            if (!this.HasAnotherFileEventOccuredRecently(e.FullPath))
            {
                this.OnDeleted(e);
            }
        }

        /// <summary>
        /// Called when [renamed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.IO.RenamedEventArgs"/> instance containing the event data.</param>
        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (!this.HasAnotherFileEventOccuredRecently(e.OldFullPath))
            {
                this.OnRenamed(e);
            }
        }
        #endregion
        #endregion        
    }
}