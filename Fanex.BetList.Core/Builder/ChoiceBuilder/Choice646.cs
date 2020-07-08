namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Double Chance.
    /// </summary>
    public class Choice646 : Choice1
    {
        private const string betChoices = "<span class=\"{0}\"> {1}</span>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int expectedBetTeamLength = 2;

            if (!string.IsNullOrEmpty(ticket.BetTeam) && ticket.BetTeam.Length == expectedBetTeamLength)
            {
                var betTeam = ticket.BetTeam.ToLowerInvariant();
                var firstIndicator = betTeam[0].ToString();
                var secondIndicator = betTeam[1].ToString();
                var teamName = BetTeamValue.H == firstIndicator ? ticketHelper.GetTeamNameById(ticket.HomeId) : ticketHelper.GetTeamNameById(ticket.AwayId);
                var teamColor = GetTeamColor(firstIndicator, ticket.Handicap2);
                var handicap2 = GetHandicap2(firstIndicator, ticket.Handicap2);
                var overUnderValue = BetTeamValue.O == secondIndicator ? CoreBetList.over : CoreBetList.under;
                var overUnderColor = BetTeamValue.O == secondIndicator ? Favorite : Underdog;

                var betChoicesSection = new StringBuilder();
                betChoicesSection.AppendFormat(betChoices, teamColor, teamName);
                betChoicesSection.AppendFormat(betChoices, "handicap", ConvertByBetType.Hdp(handicap2));
                betChoicesSection.AppendFormat(betChoices, Underdog, "&");
                betChoicesSection.AppendFormat(betChoices, overUnderColor, overUnderValue);
                betChoicesSection.AppendFormat(betChoices, "handicap", ConvertByBetType.Hdp(ticket.Handicap1));

                Template.betTeam = betChoicesSection.ToString();
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }

        protected override void AdjustBetTeamToRTF()
        {
            base.AdjustBetTeamToRTF();

            Template.betTeam = Template.betTeam.Replace("<span class=\"handicap\">", string.Empty);
        }

        private static string GetTeamColor(string firstIndicator, decimal handicap2)
        {
            var color = Underdog;
            if ((BetTeamValue.H == firstIndicator && handicap2 < 0) ||
                (BetTeamValue.A == firstIndicator && handicap2 > 0))
            {
                color = Favorite;
            }

            return color;
        }

        private static decimal GetHandicap2(string firstIndicator, decimal handicap2)
        {
            if (handicap2 > 0)
            {
                return BetTeamValue.A == firstIndicator ? -handicap2 : handicap2;
            }

            if (handicap2 < 0)
            {
                return BetTeamValue.H == firstIndicator ? handicap2 : -handicap2;
            }

            return handicap2;
        }

    }
}