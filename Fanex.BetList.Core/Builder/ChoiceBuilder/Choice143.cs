namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// Bet Type: First Half Result/Total Goal - 143.
    /// </summary>
    public class Choice143 : Choice1
    {
        /// <summary>
        /// Set bet team to template by ticket bet team pattern with following rules:
        /// <para>ho: Home and Over, hu: Home and Under</para>
        /// <para>ao: Away and Over, au: Away and Under</para>
        /// <para>do: Draw and Over, du: Draw and Over</para>
        /// </summary>
        /// <param name="ticket"> Ticket with bet type id is 143.</param>
        /// <param name="ticketHelper"> Not null ticket helper.</param>
        /// <param name="ticketData"> Not need this.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.ToLowerInvariant();

            // Bet team pattern length is always 2
            if (betTeam.Length == 2)
            {
                var totalGoal = betTeam[1].ToString() == BetTeamValue.O ? CoreBetList.over : CoreBetList.under;
                var teamName = string.Empty;

                switch (betTeam[0])
                {
                    case 'h':
                        teamName = ticketHelper.GetTeamNameById(ticket.HomeId);
                        break;

                    case 'a':
                        teamName = ticketHelper.GetTeamNameById(ticket.AwayId);
                        break;

                    case 'd':
                        teamName = CoreBetList.draw;
                        break;

                    default:
                        teamName = string.Empty;
                        break;
                }

                Template.betTeam = $"{teamName} <span class=\"{Underdog}\">&</span> {totalGoal}";
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }
    }
}