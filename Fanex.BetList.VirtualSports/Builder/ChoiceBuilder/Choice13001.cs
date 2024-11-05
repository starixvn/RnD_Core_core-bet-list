namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Betby - Double Chance
    /// </summary>
    public class Choice13001 : Choice13000
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int expectedBetTeamLength = 2;

            if (!string.IsNullOrEmpty(ticket.BetTeam) && ticket.BetTeam.Length == expectedBetTeamLength)
            {
                switch (ticket.BetTeam.ToLowerInvariant())
                {
                    case BetTeamValue.HD:
                        Template.betTeam = $"{CoreBetList.Home} {CoreBetList.lblOr} {CoreBetList.draw}";
                        break;
                    case BetTeamValue.HA:
                        Template.betTeam = $"{CoreBetList.Home} {CoreBetList.lblOr} {CoreBetList.Away}";
                        break;
                    case BetTeamValue.DA:
                        Template.betTeam = $"{CoreBetList.Away} {CoreBetList.lblOr} {CoreBetList.draw}";
                        break;
                    default:
                        Template.betTeam = string.Empty;
                        break;
                }
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }
    }
}