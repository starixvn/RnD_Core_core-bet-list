namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// Bet Type: Quarter 3/Quarter 4 Result.
    /// </summary>
    public class Choice633 : Choice1
    {
        private readonly Dictionary<string, string> betTeamPatterns
                = new Dictionary<string, string>
                {
                    { BetTeamValue.H, CoreBetList.Home },
                    { BetTeamValue.D, CoreBetList.draw },
                    { BetTeamValue.A, CoreBetList.Away }
                };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betteams = string.IsNullOrWhiteSpace(ticket.BetTeam) ? new char[0] : ticket.BetTeam.ToLowerInvariant().ToCharArray();
            var betTeamsText = betteams
                .Where(item => betTeamPatterns.ContainsKey(item.ToString()))
                .Select(item => betTeamPatterns[item.ToString()])
                .ToList();

            Template.betTeam = string.Join("/", betTeamsText);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}