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
            var betteams = new Dictionary<string, string>
            {
                { "hb", CoreBetList.HomeBack },
                { "1b", CoreBetList.HomeBack },
                { "xb", CoreBetList.DrawBack },
                { "2b", CoreBetList.AwayBack },
                { "ab", CoreBetList.AwayBack },
                { "1l", CoreBetList.HomeLay },
                { "hl", CoreBetList.HomeLay },
                { "xl", CoreBetList.DrawLay },
                { "2l", CoreBetList.AwayLay },
                { "al", CoreBetList.AwayLay }
            };

            Template.betTeam = betteams.ContainsKey(ticket.BetTeam.ToLowerInvariant()) ?
                betteams[ticket.BetTeam.ToLowerInvariant()] :
                ticket.BetTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}