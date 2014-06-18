namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

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
                this.Template.betTeam = this.Template.Match.homeTeam;
                this.Template.betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? Underdog : Favorite;
                this.Template.Handicap.handicap = ConvertByBetType.Hdp((this.Template.betTeamClassName == Favorite) ? -handicap : handicap);
            }
            else if (betAway)
            {
                this.Template.betTeam = this.Template.Match.awayTeam;
                this.Template.betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? Underdog : Favorite;
                this.Template.Handicap.handicap = ConvertByBetType.Hdp((this.Template.betTeamClassName == Favorite) ? -handicap : handicap);
            }
            else
            {
                this.Template.betTeamClassName = Underdog;

                if (ticket.Handicap2 > ticket.Handicap1)
                {
                    this.Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.h, "+", ConvertByBetType.Hdp(ticket.Handicap2) + ")" });
                }
                else
                {
                    this.Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.a, "+", ConvertByBetType.Hdp(ticket.Handicap1), ")" });
                }

                this.Template.Handicap.handicap = null;
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper)
        {
            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}