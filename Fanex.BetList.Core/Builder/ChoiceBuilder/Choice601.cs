namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Constants;
    using Entities;

    /// <summary>
    /// BAIO - FT Winning Margin 14 Way
    /// </summary>
    public class Choice601 : Choice4
    {
        protected const string BasketballRefId = "WMBasketball";

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1to2:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "1-2");
                    break;

                case BetTeamValue.H3to6:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "3-6");
                    break;

                case BetTeamValue.H7to9:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "7-9");
                    break;

                case BetTeamValue.H10to13:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "10-13");
                    break;

                case BetTeamValue.H14to16:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "14-16");
                    break;

                case BetTeamValue.H17to20:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "17-20");
                    break;

                case BetTeamValue.H21Over:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "21+");
                    break;

                case BetTeamValue.A1to2:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "1-2");
                    break;

                case BetTeamValue.A3to6:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "3-6");
                    break;

                case BetTeamValue.A7to9:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "7-9");
                    break;

                case BetTeamValue.A10to13:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "10-13");
                    break;

                case BetTeamValue.A14to16:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "14-16");
                    break;

                case BetTeamValue.A17to20:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "17-20");
                    break;

                case BetTeamValue.A21Over:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "21+");
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}