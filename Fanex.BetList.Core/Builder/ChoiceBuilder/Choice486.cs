namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// PHAIO4B - Corners Race.
    /// </summary>
    public class Choice486 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.H:
                    Template.betTeam = Template.Match.homeTeam;
                    break;

                case BetTeamValue.A:
                    Template.betTeam = Template.Match.awayTeam;
                    break;

                case BetTeamValue.N:
                    Template.betTeam = CoreBetList.lblNeither;
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
            Template.betTeamClassName = Favorite;
        }
    }
}