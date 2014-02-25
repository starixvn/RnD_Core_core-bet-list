// ---------------------------------------------------------------------------------
// <copyright file="XMLLogger.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.24">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.CoreVerified.Wrap.Logging
{
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Class XML Logger.
    /// </summary>
    internal class XMLLogger
    {
        private static XmlTextWriter xmlWriter;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns><c>True</c> if succeed, <c>false</c> otherwise.</returns>
        public static bool Start()
        {
            try
            {
                xmlWriter = new XmlTextWriter("CBL.xml", Encoding.UTF8);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("HTML");
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Writes the specified data to log file.
        /// </summary>
        /// <param name="dataItemIndex">Index of the data item.</param>
        /// <param name="choice">The choice markup.</param>
        /// <param name="odds">The odds markup.</param>
        /// <param name="stake">The stake markup.</param>
        /// <param name="status">The status markup.</param>
        /// <param name="trans">The transaction markup.</param>
        /// <returns><c>True</c> if succeed, <c>false</c> otherwise.</returns>
        public static bool Write(int dataItemIndex, string choice, string odds, string stake, string status, string trans)
        {
            try
            {
                // Create Parent element
                xmlWriter.WriteStartElement("Ticket");
                xmlWriter.WriteAttributeString("index", dataItemIndex.ToString());

                // Create Child elements
                xmlWriter.WriteElementString("Choice", choice);
                xmlWriter.WriteElementString("Odds", odds);
                xmlWriter.WriteElementString("Stake", stake);
                xmlWriter.WriteElementString("Status", status);
                xmlWriter.WriteElementString("Trans", trans);

                xmlWriter.WriteEndElement();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Ends this instance.
        /// </summary>
        /// <returns><c>True</c> if succeed, <c>false</c> otherwise.</returns>
        public static bool End()
        {
            try
            {
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
