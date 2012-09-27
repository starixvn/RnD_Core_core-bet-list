//--------------------------------------------------------------------------
// <copyright file="Employee.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Anders.Le" date="2012.5.18">Created</change>
// </history>
namespace Fanex.NexHR.Models
{
    using System;
    
    using System.Linq;
    using Fanex.DataConsole;
    

    
    public class Employee : EntityBase<int>
    {

        public string DomainUsername { get; set; }

        
        public string FullName { get; set; }

        public string NickName { get; set; }

        
        public DateTime DOB { get; set; }

        
        public string Email { get; set; }

        public int? JobTitleId { get; set; }

        public bool Enabled { get; set; }

        public string Phone { get; set; }

        public byte? Gender { get; set; }

        public ITeam Group { get; set; }

        public short Role { get; set; }

        public Employee Manager { get; set; }

        
        public DateTime? JoinedDate { get; set; }

        
        public DateTime? ConfirmedDate { get; set; }

        
        public DateTime? ResignedDate { get; set; }

        public string Skype { get; set; }

        public string Yahoo { get; set; }

        public bool Mariage { get; set; }

        public DateTime? LastLogin { get; set; }

        public decimal AnnualLeaveBalance { get; set; }

        public DateTime CreatedDate { get; set; }

        public Employee Creator { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Employee Modifier { get; set; }

      
    }
}
