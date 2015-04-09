namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Utils;

    /// <summary>
    ///    Match Handicap Games 3-way.
    /// </summary>
    public class Choice1309 : Choice1
    {
        /// <summary>
        /// Builds the bet team class name and handicap.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            bool betHome = IsBetHome(ticket);
            bool betAway = IsBetAway(ticket);
            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;

            if (betHome)
            {
                Template.betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? Underdog : Favorite;
                Template.Handicap.handicap = ConvertByBetType.Hdp((Template.betTeamClassName == Favorite) ? -handicap : handicap);
            }
            else if (betAway)
            {
                Template.betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? Underdog : Favorite;
                Template.Handicap.handicap = ConvertByBetType.Hdp((Template.betTeamClassName == Favorite) ? -handicap : handicap);
            }
            else
            {
                Template.betTeamClassName = Underdog;
                Template.Handicap.handicap = null;
            }
        }

        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            bool betHome = IsBetHome(ticket);
            bool betAway = IsBetAway(ticket);

            if (betHome)
            {
                Template.betTeam = Template.Match.homeTeam;
            }
            else if (betAway)
            {
                Template.betTeam = Template.Match.awayTeam;
            }
            else
            {
                if (ticket.Handicap2 > ticket.Handicap1)
                {
                    Template.betTeam = string.Join(null, new string[] { CoreBetList.lblDraw, "&nbsp;(", CoreBetList.h, "+", ConvertByBetType.Hdp(ticket.Handicap2) + ")" });
                }
                else
                {
                    Template.betTeam = string.Join(null, new string[] { CoreBetList.lblDraw, "&nbsp;(", CoreBetList.a, "+", ConvertByBetType.Hdp(ticket.Handicap1), ")" });
                }
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }

        private static bool IsBetHome(ITicket ticket)
        {
            return ticket.BetTeam == "1";
        }

        private static bool IsBetAway(ITicket ticket)
        {
            return ticket.BetTeam == "2";
        }
    }
}