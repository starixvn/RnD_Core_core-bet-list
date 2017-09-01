namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// PH AIO Phase 3 - Time Of 1st Goal.
    /// </summary>
    public class Choice455 : Choice1
    {
        private readonly Dictionary<string, string> BetTeamMaps = new Dictionary<string, string>
        {
            ["1-10"] = "00:01-10:00",
            ["11-20"] = "10:01-20:00",
            ["21-30"] = "20:01-30:00",
            ["31-40"] = "30:01-40:00",
            ["41-50"] = "40:01-50:00",
            ["51-60"] = "50:01-60:00",
            ["61-70"] = "60:01-70:00",
            ["71-80"] = "70:01-80:00",
            ["81-90"] = $"80:01-{CoreBetList.ft}"
        };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam?.Trim()?.ToLowerInvariant();

            if (betTeam == BetTeamValue.None)
            {
                Template.betTeam = CoreBetList.nogoal;
            }
            else if (!string.IsNullOrEmpty(betTeam) && BetTeamMaps.ContainsKey(betTeam))
            {
                Template.betTeam = BetTeamMaps[betTeam];
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}