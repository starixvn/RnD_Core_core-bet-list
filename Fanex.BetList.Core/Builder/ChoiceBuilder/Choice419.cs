namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Bet type name: Which Half First Goal
    /// </summary>
    public class Choice419 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.OneH:
                    betTeam = CoreBetList.FirstHalfShort;
                    break;

                case BetTeamValue.TwoH:
                    betTeam = CoreBetList.SecondHalfShort;
                    break;

                case BetTeamValue.N:
                    betTeam = CoreBetList.lblNeither;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}