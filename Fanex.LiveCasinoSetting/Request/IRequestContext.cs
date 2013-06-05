// <copyright file="ISettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved. 
// </copyright>
// <history>
//  <change who="Anders" date="2012.01.11">Create</change>
//  <change who="Andy" date="2012.01.16">Add SubAccId</change>
// </history>
namespace Fanex.LiveCasinoSetting.Request
{
    /// <summary>
    /// Describe all information of a request to update the setting
    /// </summary>
    public interface IRequestContext
    {
        int UserId { get; set; }
        int CustId { get; set; }
        int SubAccId { get; set; }
        int UplineId { get; set; }
        string SignUpIp { get; set; }
        string Language { get; set; }
        bool IsEditSuper { get; set; }
        bool IsValid { get; }
        bool IsSyncLiveCasino { get; set; }
    }
}
