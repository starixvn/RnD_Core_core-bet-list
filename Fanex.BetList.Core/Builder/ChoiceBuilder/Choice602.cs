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
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "1-5"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H6to10:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "6-10"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H11to15:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "11-15"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H16to20:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "16-20"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H21to25:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "21-25"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.H26Over:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "26+"), Template.Match.homeTeam);
                    break;

                case BetTeamValue.A1to5:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "1-5"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A6to10:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "6-10"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A11to15:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "11-15"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A16to20:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "16-20"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A21to25:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "21-25"), Template.Match.awayTeam);
                    break;

                case BetTeamValue.A26Over:
                    betTeam = string.Format(ticketHelper.GetResourceData(BasketballRefId, "26+"), Template.Match.awayTeam);
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}