// <copyright file="Result.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
//
// <summary>
// 
// </summary>
//
// <history>
//  <change who="Anders" date="2012.01.10">Create</change>
//  <change who="Anders" date="2012.01.16">check constructor param</change>
//	<change who="anders.le" date="2012.1.30">Change errorCode dataType to string</change>
// </history>

using Fanex.Data;
using System;
namespace Fanex.LiveCasinoSetting.Model
{
    /// <summary>
    /// Use to describe result from command methods
    /// </summary>
    [Serializable]
    public class Result
    {
        private string _errorCode;
        private string _errorMessage;

        public Result() : this("CS0000", string.Empty) { }

        internal Result(string errorCode, string errorMessage)
        {
            _errorCode = errorCode;
            _errorMessage = errorMessage;
        }

        public string ErrorCode
        {
            get { return _errorCode; }
        }

        public string ErrorMessage 
        { 
            get { return _errorMessage; }
        }
        
        //public bool IsSuccess
        //{
        //    get
        //    {
        //        return string.IsNullOrWhiteSpace(ErrorCode)
        //            && string.IsNullOrWhiteSpace(ErrorMessage);
        //    }
        //}
    }
}
