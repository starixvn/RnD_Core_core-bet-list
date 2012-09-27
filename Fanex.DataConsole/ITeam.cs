using System;
namespace Fanex.DataConsole
{
    public interface ITeam
    {
        Fanex.NexHR.Models.Employee AssistantLeader { get; set; }
        DateTime CreatedDate { get; set; }
        Fanex.NexHR.Models.Employee Creator { get; set; }
        Fanex.NexHR.Models.Department Department { get; set; }
        
        Fanex.NexHR.Models.Employee Leader { get; set; }
        Fanex.NexHR.Models.Employee Modifer { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
