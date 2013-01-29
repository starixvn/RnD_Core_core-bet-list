// --------------------------------------------------------------------------
// <copyright file="ConnectionString.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.11">Created</change>
// </history>

namespace Fanex.ConnectionString.Domain
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.ConnectionString.Constanst;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ConnectionString : IEntity<int, string>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DatabaseServer Server { get; set; }

        public Alias Alias { get; set; }

        public string InitialCatalog { get; set; }

        public bool IntegratedSecurity { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }

        public string FreeText { get; set; }

        public string Connection
        {
            get
            {
                if (!ReferenceEquals(Server, null) && Server.ServerIP != string.Empty)
                {
                    string tempConnection = string.Format(ConectionStringConstanst.CONNECTIONFORMAT,
                                                                Server.ServerIP,
                                                                InitialCatalog,
                                                                IntegratedSecurity,
                                                                UserID,
                                                                Password,
                                                                FreeText);
                    return tempConnection;
                }

                return string.Empty;
            }
        }

        public bool IsDefault { get; set; }

        public ConnectionString PrimaryConnection { get; set; }        

        public Users Creator { get; set; }

        public DateTime CreatedDate { get; set; }

        public Users Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
