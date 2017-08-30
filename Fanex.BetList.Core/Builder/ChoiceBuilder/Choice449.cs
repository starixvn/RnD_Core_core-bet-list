namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// PH AIO Phase 3 - Double Chance &amp; Total Goals.
    /// </summary>
    public class Choice449 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = ticket.BetTeam?.ToLowerInvariant() ?? string.Empty;

            if (betTeam.Length == 3)
            {
                string firstChanceChoice = MapDoubleChanceChoice(betTeam[0]);
                string secondChanceChoice = MapDoubleChanceChoice(betTeam[1]);
                string overUnderChoice = MapOverUnderChoice(betTeam[2]);

                if (!string.IsNullOrEmpty(firstChanceChoice)
                    && !string.IsNullOrEmpty(secondChanceChoice)
                    && !string.IsNullOrEmpty(overUnderChoice))
                {
                    Template.betTeam = $"{firstChanceChoice}/{secondChanceChoice} <span class=\"{Underdog}\">&</span> {overUnderChoice}";

                    return;
                }
            }

            Template.betTeam = string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
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

        private static string MapOverUnderChoice(char indicator)
        {
            switch (indicator.ToString())
            {
                case BetTeamValue.O:
                    return CoreBetList.over;

                case BetTeamValue.U:
                    return CoreBetList.under;

                default:
                    return string.Empty;
            }
        }
    }
}