namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;

    /// <summary>
    ///  Set x Games y and y+1 Winner.
    /// </summary>
    public class Choice1333 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            bool team1 = ticket.BetTeam == "1";
            bool team2 = ticket.BetTeam == "2";

            if (team1)
            {
                Template.betTeam = Template.Match.homeTeam;
            }
            else if (team2)
            {
                Template.betTeam = Template.Match.awayTeam;
            }
            else
            {
                Template.betTeam = CoreLang.lblNeither;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            base.BuildBetTeamClassNameAndHandicap(ticket);

            Template.Handicap.handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.BetId);
        }
    }
}