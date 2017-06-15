namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// BAIO - FT Winning Margin 14 Way
    /// </summary>
    public class Choice601 : Choice4
    {
        protected const string BasketballRefId = "WMBasketball";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1to2:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "1-2"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H3to6:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "3-6"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H7to9:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "7-9"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H10to13:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "10-13"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H14to16:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "14-16"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H17to20:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "17-20"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H21Over:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "21+"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.A1to2:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "1-2"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A3to6:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "3-6"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A7to9:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "7-9"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A10to13:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "10-13"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A14to16:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "14-16"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A17to20:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "17-20"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A21Over:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "21+"), Template.Match.awayTeam);
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}