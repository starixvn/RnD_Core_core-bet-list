namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Both/One/Neither team to score.
    /// </summary>
    public class Choice26 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.B:
                    Template.betTeam = CoreBetList.lblBoth;
                    break;

                case BetTeamValue.O:
                    Template.betTeam = CoreBetList.lblOne;
                    break;

                case BetTeamValue.N:
                    Template.betTeam = CoreBetList.nogoal;
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}