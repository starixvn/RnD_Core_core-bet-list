///<copyright file="CustomerInfo.cs" company="Nexcel Solutions Vietnam" >
/// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
///</copyright>
///
///<history>
///	<change who="anders.le" date="2012.2.2">Created</change>
///	<change who="anders.le" date="2012.2.9">Add IsSyncLiveCasino</change>
///</history>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.LiveCasinoSetting.Model
{
    [Serializable]
    public class CustomerInfo
    {
        public int CustomerId { get; set; }

        public string UserName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Fax { get; set; }

        public byte RoleId { get; set; }

        public bool IsSyncLiveCasino { get; set; }

        public int ExChangeId { get; set; }

        public string Site { get; set; }

        public bool DisableLiveCasino { get; set; }

        public bool UplineDisableLiveCasino { get; set; }

        public IMemberWinLimit Limit
        {
            get;
            set;
        }
    }
}