namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Next Odd/Even.
    /// </summary>
    public class Choice86 : Choice2
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            string liveScore = string.Empty;
            string betTeam = Template.betTeam;

            if (ticket.IsLive)
            {
                string betId = ticket.BetId.ToString();
                if (betId == "0")
                {
                    betId = string.Empty;
                }

                liveScore = (!ticket.IsLive || betId == string.Empty)
                    ? " [0]"
                    : string.Join(null, new string[] { " [", betId, "]" });
            }

            Template.betTeam = betTeam + liveScore;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetType(ticket, ticketHelper, ticketData);

            string bettypeName = Template.BetType.betTypeName;
            if (!ticket.IsLive)
            {
                bettypeName = string.Join(null, new string[] { CoreBetList.ft, ". ", bettypeName });
            }
            else
            {
                bettypeName = CoreBetList.next + HtmlCharacters.NoneBreakingSpace + bettypeName;
            }

            Template.BetType.betTypeName = bettypeName;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.Match.VS = null;
            Template.Match.homeTeam = string.Join(null, new string[] { CoreBetList.numbergameno, HtmlCharacters.NoneBreakingSpace, ticket.MatchCode });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            // Bet team name is always in red for this bettype
            Template.betTeamClassName = Favorite;
        }
    }
}