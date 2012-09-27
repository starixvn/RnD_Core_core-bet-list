namespace Fanex.NexHR.Models
{
    using System;

    public class Team : EntityBase<int>, Fanex.DataConsole.ITeam
    {         
        
        public Department Department { get; set; }

        public Employee Leader { get; set; }

        public Employee AssistantLeader { get; set; }        

        public DateTime CreatedDate { get; set; }

        public Employee Creator { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Employee Modifer { get; set; }
    }
}
