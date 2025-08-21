namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;
    using System.Collections.Generic;

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
                { "hb", CoreBetList.Back },
                { "1b", CoreBetList.Back },
                { "1l", CoreBetList.Lay },
                { "hl", CoreBetList.Lay },
                { "xb", CoreBetList.Back },
                { "xl", CoreBetList.Lay },
                { "2b", CoreBetList.Back },
                { "ab", CoreBetList.Back },
                { "2l", CoreBetList.Lay },
                { "al", CoreBetList.Lay }
            };
            var betTeamAction = betteams.ContainsKey(betTeam) ? betteams[betTeam] : betTeam;
            var teamName = GetTeamName(ticket, ticketHelper);
            var teamNameFormat = $"<span class='underdog'>{teamName}</span>";
            Template.betTeam = string.Format(betTeamAction, teamNameFormat);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            Template.betTeam = Template.betTeam.Replace("<span class='underdog'>", string.Empty);
            Template.betTeam = Template.betTeam.Replace("</span>", string.Empty);

            base.BuildBetFigure(ticket, ref rtfHelper, isMixParlay);
        }

        private static string GetTeamName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTeam = ticket.BetTeam.ToLowerInvariant();
            if (betTeam.Length > 0 && (betTeam[0] == 'h' || betTeam[0] == '1'))
            {
                return ticketHelper.GetTeamNameById(ticket.HomeId);
            }

            if (betTeam.Length > 0 && (betTeam[0] == 'a' || betTeam[0] == '2'))
            {
                return ticketHelper.GetTeamNameById(ticket.AwayId);
            }

            return CoreBetList.draw;
        }
    }
}