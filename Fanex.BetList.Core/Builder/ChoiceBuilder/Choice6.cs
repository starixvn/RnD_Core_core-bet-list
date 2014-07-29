namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// Total Goal.
    /// </summary>
    public class Choice6 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            // &nbsp; because export format
            var betTeam = ticket.BetTeam;

            if (betTeam == "7-over")
            {
                betTeam = CoreBetList.SevenAndOver;
            }
            else if (betTeam == "4-over")
            {
                betTeam = CoreBetList.FourAndOver;
            }

            Template.betTeam = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, betTeam });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }      
    }
}