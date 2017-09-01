namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// PH AIO Phase 3 - Both Teams to Score / Double Chance.
    /// </summary>
    public class Choice451 : Choice1
    {
        private const int ExpectedBetTeamLength = 3;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = ticket.BetTeam?.ToLowerInvariant() ?? string.Empty;

            if (betTeam.Length == ExpectedBetTeamLength)
            {
                string yesNoChoice = MapYesNoChoice(betTeam[0]);
                string firstChanceChoice = MapDoubleChanceChoice(betTeam[1]);
                string secondChanceChoice = MapDoubleChanceChoice(betTeam[2]);

                if (!string.IsNullOrEmpty(yesNoChoice)
                    && !string.IsNullOrEmpty(firstChanceChoice)
                    && !string.IsNullOrEmpty(secondChanceChoice))
                {
                    Template.betTeam = $"{yesNoChoice} <span class=\"{Underdog}\">&</span> {firstChanceChoice}/{secondChanceChoice}";

                    return;
                }
            }

            Template.betTeam = string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }

        private static string MapYesNoChoice(char indicator)
        {
            switch (indicator.ToString())
            {
                case BetTeamValue.Y:
                    return CoreBetList.lblYes;

                case BetTeamValue.N:
                    return CoreBetList.lblNo;

                default:
                    return string.Empty;
            }
        }

        private static string MapDoubleChanceChoice(char indicator)
        {
            switch (indicator.ToString())
            {
                case BetTeamValue.NumberOne:
                    return CoreBetList.Home;

                case BetTeamValue.NumberTwo:
                    return CoreBetList.Away;

                case BetTeamValue.X:
                    return CoreBetList.draw;

                default:
                    return string.Empty;
            }
        }
    }
}