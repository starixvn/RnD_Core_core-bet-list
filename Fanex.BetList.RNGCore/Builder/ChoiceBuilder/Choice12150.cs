using System.Collections.Generic;
using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    public class Choice12150 : ChoiceRNGCore
    {
        protected override string RefIdForGameGroup => "SS_RNG_GG";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam = $"{ticket.BetTeam}</br>";
            }

            Template.betTeam += $"<span class=\"handicap\">{ticketHelper.GetBetTypeNameById(ticket.BetTypeId)}</span>";

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam += $" {GetTransId3rd(ticket.BetCheck)}";
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam.Replace("</br>", "\n");
            base.AdjustBetTeamToRTF();
        }
    }
}
