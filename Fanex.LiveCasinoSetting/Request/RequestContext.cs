// <copyright file="ISettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
// <history>
//  <change who="Anders" date="2012.01.11">Create</change>
//  <change who="Anders" date="2012.01.12">Add IsEditSuper</change>
//  <change who="Andy" date="2012.01.16">Add SubAccId</change>
// </history>
namespace Fanex.LiveCasinoSetting.Request
{
    using Fanex.Common.Extensions;

    /// <summary>
    /// this class containt all information of a request to update live casino setting
    /// </summary>
    public class RequestContext : IRequestContext
    {
        /// <summary>
        /// user perform action update
        /// </summary>        
        public int UserId { get; set; }

        /// <summary>
        /// ip of userid
        /// </summary>        
        public string SignUpIp { get; set; }

        /// <summary>
        /// current language of system that the user has already authenticated
        /// </summary>        
        public string Language { get; set; }

        /// <summary>
        /// indentity of customer who is updated
        /// it can be memberid, agentid, masterid, or superid
        /// note: dont use it when perform action update multiple
        /// </summary>        
        public int CustId { get; set; }

        /// <summary>
        /// Sub-account Id is > 0 if sub-account updates settings
        /// </summary>
        public int SubAccId { get; set; }
        /// <summary>
        /// uplineid is custid of <see cref="custid"/>
        /// </summary>        
        public int UplineId { get; set; }

        public bool IsValid
        {
            get
            {
                return UserId > 0
                    && SignUpIp.IsIpAddress()
                    && !string.IsNullOrWhiteSpace(Language)
                    && CustId > 0
                    && (UplineId > 0 || (UplineId <= 0 && IsEditSuper));
            }
        }


        /// <summary>
        /// set it true if and only if admin edit super
        /// default value is false
        /// </summary>
        public bool IsEditSuper
        {
            get;
            set;
        }


        public bool IsSyncLiveCasino { get; set; }
    }
}
