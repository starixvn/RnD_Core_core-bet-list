namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Bet Type: First Half Total Goal - 143.
    /// </summary>
    public class Choice143 : Choice1
    {
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "ao is ok here.")]
#pragma warning disable 1587
        /// <summary>
        /// Set bet team to template by ticket bet team pattern with following rules:
        /// <para>ho: Home and Over, hu: Home and Under</para>
        /// <para>ao: Away and Over, au: Away and Under</para>
        /// <para>do: Draw and Over, du: Draw and Over</para>
        /// </summary>
        /// <param name="ticket"> Ticket with bet type id is 143.</param>
        /// <param name="ticketHelper"> Not null ticket helper.</param>
        /// <param name="ticketData"> Not need this.</param>
#pragma warning restore 1587
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var teamName = string.Empty;

            // Bet team pattern length is always 2
            if (ticket.BetTeam.Length == 2)
            {
                switch (ticket.BetTeam[0])
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
                }

                var resultOverUnder = (ticket.BetTeam[1].ToString(CultureInfo.InvariantCulture) == BetTeamValue.O)
                    ? CoreBetList.over
                    : CoreBetList.under;

                Template.betTeam = string.Join(" & ", new string[] { teamName, resultOverUnder });
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
        }
    }
}