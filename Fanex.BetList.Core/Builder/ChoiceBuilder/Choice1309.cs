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
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            bool betHome = ticket.BetTeam == "1";
            bool betAway = ticket.BetTeam == "2";
            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;

            if (betHome)
            {
                Template.betTeam = Template.Match.homeTeam;
                Template.betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? Underdog : Favorite;
                Template.Handicap.handicap = ConvertByBetType.Hdp((Template.betTeamClassName == Favorite) ? -handicap : handicap);
            }
            else if (betAway)
            {
                Template.betTeam = Template.Match.awayTeam;
                Template.betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? Underdog : Favorite;
                Template.Handicap.handicap = ConvertByBetType.Hdp((Template.betTeamClassName == Favorite) ? -handicap : handicap);
            }
            else
            {
                Template.betTeamClassName = Underdog;

                if (ticket.Handicap2 > ticket.Handicap1)
                {
                    Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.h, "+", ConvertByBetType.Hdp(ticket.Handicap2) + ")" });
                }
                else
                {
                    Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.a, "+", ConvertByBetType.Hdp(ticket.Handicap1), ")" });
                }

                Template.Handicap.handicap = null;
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}