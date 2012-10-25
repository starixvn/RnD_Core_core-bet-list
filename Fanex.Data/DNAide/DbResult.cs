// <copyright file="DbResult.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// This class includes a Dataset and a hashtable which contain output params
// </summary>
//
// <history>
//  <change who="Anders" date="2011.12.24">Clone form DNAide Lib</change>
//  <change who="Qui" date="2012.03.06">Add serializable</change>
// </history>

namespace Fanex.Data.DNAide
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Collections;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// 
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [Serializable]
    public class DbResult : ISerializable
    {
        [NonSerialized]
        private Hashtable _output;

        /// <summary>
        /// DataSet to store all the result sets from database.
        /// </summary>
        private DataSet _dataSet;

        #region Ctor

        public DbResult(Hashtable output, DataSet dataSet)
        {
            _output = output;
            _dataSet = dataSet;
        }

        //public DbResult() : this (new Hashtable(), new DataSet()) {}

        #endregion

        /// <summary>
        /// store output database parameters values 
        /// </summary>
        public Hashtable Outputs
        {
            get
            {
                return _output;
            }
        }

        /// <summary>
        /// store result set returned by store procedure
        /// </summary>
        public DataSet ResultSet
        {
            get
            {
                return _dataSet;
            }
        }

        #region ISerializable Members

        public DbResult(SerializationInfo info, StreamingContext context)
        {
            // Deserialize hashtable

            List<string> outputKeys = info.GetValue("outputKeys", typeof(List<string>)) as List<string>;
            List<object> outputValues = info.GetValue("outputValues", typeof(List<object>)) as List<object>;

            int length = outputKeys.Count;
            _output = new Hashtable(length);
            for (int i = 0; i < length; i++)
            {
                _output[outputKeys[i]] = outputValues[i];
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize hashtable

            List<string> outputKeys = new List<string>();
            List<object> outputValues = new List<object>();

            foreach (object key in _output.Keys)
            {
                outputKeys.Add(key as string);

                object objValue = _output[key];
                // Resolve DBNull case
                if (objValue is DBNull) 
                {
                    objValue = string.Empty;
                }

                outputValues.Add(objValue);
            }

            info.AddValue("outputKeys", outputKeys);
            info.AddValue("outputValues", outputValues);
        }

        #endregion
    }
}
