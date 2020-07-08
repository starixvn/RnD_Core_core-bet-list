namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
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
            var betTeamsText = new List<string>();
            foreach (var item in betteams)
            {
                if (betTeamPatterns.ContainsKey(item.ToString()))
                {
                    betTeamsText.Add(betTeamPatterns[item.ToString()]);
                }
            }

            Template.betTeam = string.Join("/", betTeamsText);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}