namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// 1st ball O/U.
    /// </summary>
    public class Choice81 : Choice1
    {
        protected const string NumberGameValue = " <span style='color:#555555'>37.5</span> ";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ticket.BetTeam.ToLowerInvariant() == BetTeamValue.H ? CoreBetList.over : CoreBetList.under;
            Template.betTeam += NumberGameValue;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.Match.VS = null;
            Template.Match.homeTeam = $"{CoreBetList.numbergameno}{HtmlCharacters.NoneBreakingSpace}{ticket.MatchCode}";
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}