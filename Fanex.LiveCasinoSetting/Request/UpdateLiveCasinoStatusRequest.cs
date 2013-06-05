//<copyright file="UpdateLiveCasinoStatusRequest.cs" company="Nexcel Solutions Vietnam" >
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
//</copyright>
//
//<history>
//	<change who="anders.le" date="2012.2.2">Created</change>
//</history>


namespace Fanex.LiveCasinoSetting.Request
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UpdateLiveCasinoStatusRequest
    {
        public int CustId { get; set; }
        public int UserId { get; set; }
        public int SubAccId { get; set; }
        public string SignUpIp { get; set; }
        public string Language { get; set; }
        public bool IsDisabledLiveCasino { get; set; }
        public bool IsEnableLiveCasinoForDownline { get; set; }

        public UpdateLiveCasinoStatusRequest(InsertAccountRequest request)
        {          
            this.CustId = request.CustId;
            this.IsDisabledLiveCasino = request.IsDisabledLiveCasino;
            this.IsEnableLiveCasinoForDownline = request.IsEnableDownline;
            this.SignUpIp = request.SignUpIp;
            this.Language = request.Language;
            this.UserId = request.UserId;
            this.SubAccId = request.SubAccId;

        }        
    }
}
