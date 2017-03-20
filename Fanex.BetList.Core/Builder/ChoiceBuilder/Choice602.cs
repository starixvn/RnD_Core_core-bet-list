namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Constants;
    using Entities;

    /// <summary>
    /// BAIO -  FT Winning Margin 12 Way
    /// </summary>
    public class Choice602 : Choice601
    {
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H1to5:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "1-5");
                    break;

                case BetTeamValue.H6to10:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "6-10");
                    break;

                case BetTeamValue.H11to15:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "11-15");
                    break;

                case BetTeamValue.H16to20:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "16-20");
                    break;

                case BetTeamValue.H21to25:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "21-25");
                    break;

                case BetTeamValue.H26Over:
                    betTeam = Template.Match.homeTeam + ticketHelper.GetResourceData(BasketballRefId, "26+");
                    break;

                case BetTeamValue.A1to5:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "1-5");
                    break;

                case BetTeamValue.A6to10:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "6-10");
                    break;

                case BetTeamValue.A11to15:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "11-15");
                    break;

                case BetTeamValue.A16to20:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "16-20");
                    break;

                case BetTeamValue.A21to25:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "21-25");
                    break;

                case BetTeamValue.A26Over:
                    betTeam = Template.Match.awayTeam + ticketHelper.GetResourceData(BasketballRefId, "26+");
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}