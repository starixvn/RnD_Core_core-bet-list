namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// Sporting Solution: Set X Correct Score.
    /// </summary>
    public class Choice1317 : Choice1302
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int betTeamFixedLength = 2;

            var betTeam = ticket.BetTeam.ToLowerInvariant();

            Template.betTeam = string.Empty;

            if (betTeam == BetTeamValue.HAOS)
            {
                Template.betTeam = CoreBetList.HomeToWinAnyOtherScore;
            }

            if (betTeam == BetTeamValue.AAOS)
            {
                Template.betTeam = CoreBetList.AwayToWinAnyOtherScore;
            }

            if (betTeam.Length == betTeamFixedLength)
            {
                Template.betTeam = $"{betTeam.Substring(0, 1)}:{betTeam.Substring(1, 1)}";
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
        }
    }
}