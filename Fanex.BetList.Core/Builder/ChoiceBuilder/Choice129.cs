namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Choice for bet type 129 First Half Exact Goal.
    /// </summary>
    public class Choice129 : Choice6
    {
        private static readonly Dictionary<string, string> BetTeamMapping = new Dictionary<string, string>
        {
            { "0", CoreBetList.NoGoals },
            { "1", CoreBetList.OneGoal },
            { "2", CoreBetList.TwoGoals },
            { "3", CoreBetList.ThreeGoals },
            { "4", CoreBetList.FourGoals },
            { "4&over", CoreBetList.FourAndOver },
            { "5", CoreBetList.FiveGoals },
            { "5&over", CoreBetList.FiveAndOver },
            { "6", CoreBetList.SixGoals },
            { "7&over", CoreBetList.SevenAndOver }
        };

        /// <summary>
        /// Set resource bet team.
        /// </summary>
        /// <param name="ticket"> Ticket with bet type 129.</param>
        /// <param name="ticketHelper"> Not null ticket helper.</param>
        /// <param name="ticketData"> A valid ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = BetTeamMapping.TryGetValue(ticket.BetTeam, out string betTeam) ? betTeam : string.Empty;
        }
    }
}