namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    /// 1X2 (Back & Lay)
    /// </summary>
    public class Choice9538 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.ToLowerInvariant();
            var betteams = new Dictionary<string, string>
            {
                { "hb", CoreBetList.HomeBack },
                { "1b", CoreBetList.HomeBack },
                { "1l", CoreBetList.HomeLay },
                { "hl", CoreBetList.HomeLay },
                { "xb", CoreBetList.DrawBack },
                { "xl", CoreBetList.DrawLay },
                { "2b", CoreBetList.AwayBack },
                { "ab", CoreBetList.AwayBack },
                { "2l", CoreBetList.AwayLay },
                { "al", CoreBetList.AwayLay }
            };
            var betTeamFormat = betteams.ContainsKey(betTeam) ? betteams[betTeam] : betTeam;
            var teamName = GetTeamName(ticket, ticketHelper);

            Template.betTeam = string.Format(betTeamFormat, teamName);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        private string GetTeamName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTeam = ticket.BetTeam.ToLowerInvariant();
            if (betTeam.StartsWith("h") || betTeam.StartsWith("1"))
            {
                return ticketHelper.GetTeamNameById(ticket.HomeId);
            }

            if (betTeam.StartsWith("a") || betTeam.StartsWith("2"))
            {
                return ticketHelper.GetTeamNameById(ticket.AwayId);
            }

            return string.Empty;
        }
    }
}