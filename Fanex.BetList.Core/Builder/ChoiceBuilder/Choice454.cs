namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// PH AIO Phase 3 - Double Chance &amp; First Team To Score.
    /// </summary>
    public class Choice454 : Choice1
    {
        private const int ExpectedBetTeamMaxLength = 3;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = ticket.BetTeam?.ToLowerInvariant() ?? string.Empty;

            if (betTeam == BetTeamValue.NG)
            {
                Template.betTeam = CoreBetList.nogoal;

                return;
            }

            if (betTeam.Length == ExpectedBetTeamMaxLength)
            {
                string firstChanceChoice = MapDoubleChanceChoice(betTeam[0]);
                string secondChanceChoice = MapDoubleChanceChoice(betTeam[1]);
                string firstTeamToScoreChoice = MapFirstTeamToGoalChoice(betTeam[2]);

                if (!string.IsNullOrEmpty(firstChanceChoice)
                    && !string.IsNullOrEmpty(secondChanceChoice)
                    && !string.IsNullOrEmpty(firstTeamToScoreChoice))
                {
                    Template.betTeam = $"{firstChanceChoice}/{secondChanceChoice} <span class=\"{Underdog}\">&</span> {firstTeamToScoreChoice}";

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

        private static string MapFirstTeamToGoalChoice(char indicator)
        {
            switch (indicator.ToString())
            {
                case BetTeamValue.H:
                    return CoreBetList.Home;

                case BetTeamValue.A:
                    return CoreBetList.Away;

                default:
                    return string.Empty;
            }
        }
    }
}