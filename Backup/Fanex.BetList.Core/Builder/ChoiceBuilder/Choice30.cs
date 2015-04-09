namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Entities;

    /// <summary>
    /// 1st Correct score.
    /// </summary>
    public class Choice30 : Choice4
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string choice = ticket.BetTeam;
            choice = (choice == "0:4" || choice == "4:0") ? choice + " UP" : choice;

            // "&nbsp" because export format
            Template.betTeam = HtmlCharacters.NoneBreakingSpace + choice;
        }
    }
}