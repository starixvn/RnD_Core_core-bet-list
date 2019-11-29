namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Total Goal.
    /// </summary>
    public class Choice6 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            // &nbsp; because export format
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case "4-over":
                case "4&over":
                    betTeam = CoreBetList.FourAndOver;
                    break;

                case "7-over":
                case "7&over":
                    betTeam = CoreBetList.SevenAndOver;
                    break;

                default:
                    betTeam = ticket.BetTeam.ToLowerInvariant();
                    break;
            }

            Template.betTeam = string.Join(null, new[] { HtmlCharacters.NoneBreakingSpace, betTeam });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}