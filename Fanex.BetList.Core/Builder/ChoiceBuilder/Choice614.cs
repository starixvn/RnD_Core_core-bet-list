namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// BAIO - Quarter X Winning Margin 7 Way
    /// </summary>
    public class Choice614 : Choice601
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1to4:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "1-4"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H5to8:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "5-8"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H9Over:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "9+"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.D:
                    betTeam = ticketHelper.GetResourceData(BasketballRefId, "D");
                    break;

                case BetTeamValue.A1to4:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "1-4"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A5to8:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "5-8"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A9Over:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "9+"), Template.Match.awayTeam);
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
        }
    }
}