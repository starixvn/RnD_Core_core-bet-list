namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// PH AIO Phase 3 - Odd/Even &amp; Total Goals.
    /// </summary>
    public class Choice450 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = ticket.BetTeam?.ToLowerInvariant() ?? string.Empty;

            if (betTeam.Length == 2)
            {
                string oddEvenChoice = MapOddEvenChoice(betTeam[0]);
                string overUnderChoice = MapOverUnderChoice(betTeam[1]);

                if (!string.IsNullOrEmpty(oddEvenChoice) && !string.IsNullOrEmpty(overUnderChoice))
                {
                    Template.betTeam = $"{oddEvenChoice} <span class=\"{Underdog}\">&</span> {overUnderChoice}";

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

        private static string MapOddEvenChoice(char indicator)
        {
            switch (indicator.ToString())
            {
                case BetTeamValue.O:
                    return CoreBetList.odd;

                case BetTeamValue.E:
                    return CoreBetList.even;

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