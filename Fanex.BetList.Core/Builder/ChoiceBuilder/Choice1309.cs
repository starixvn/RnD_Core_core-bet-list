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

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);
            string homeName = GetHomeTeamName(ticket, ticketHelper);
            bool betHome = IsBetHome(ticket);
            bool betAway = IsBetAway(ticket);

            if (betHome)
            {
                Template.betTeam = homeName;
            }
            else if (betAway)
            {
                Template.betTeam = awayName;
            }
            else
            {
                if (ticket.Handicap2 > ticket.Handicap1)
                {
                    Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.h, "+", ConvertByBetType.Hdp(ticket.Handicap2) + ")" });
                }
                else
                {
                    Template.betTeam = string.Join(null, new string[] { CoreLang.lblDraw, "&nbsp;(", CoreBetList.a, "+", ConvertByBetType.Hdp(ticket.Handicap1), ")" });
                }
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }

        private bool IsBetHome(ITicket ticket)
        {
            var betHome = ticket.BetTeam == "1";

            return betHome;
        }

        private bool IsBetAway(ITicket ticket)
        {
            var betAway = ticket.BetTeam == "2";

            return betAway;
        }
    }
}