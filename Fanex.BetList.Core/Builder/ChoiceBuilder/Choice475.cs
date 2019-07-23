namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// PHAIO4B - Alternative Corners.
    /// </summary>
    public class Choice475 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.O:
                    Template.betTeam = CoreBetList.over;
                    break;

                case BetTeamValue.U:
                    Template.betTeam = CoreBetList.under;
                    break;

                case BetTeamValue.E:
                    Template.betTeam = CoreBetList.Exact;
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