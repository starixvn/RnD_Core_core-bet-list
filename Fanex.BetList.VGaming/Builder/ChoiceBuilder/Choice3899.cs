using System.Collections.Generic;
using Fanex.BetList.Core.App_GlobalResources;
using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    /// <summary>
    /// Jackpot Prize
    /// </summary>
    public class Choice3899 : Choice3802
    {
        private const string OpenTagJackpotRefNo = "<div class=\"jackpotRefNo\">";
        private const string CloseTagJackpotRefNo = "</div>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            var isDetailTicket = !string.IsNullOrEmpty(ticket.BetCheck);
            if (isDetailTicket)
            {
                Template.betTeam += $"{OpenTagJackpotRefNo}{CoreBetList.forRefNo}: {ticket.BetdaqId}{CloseTagJackpotRefNo}";
            }
        }
        
        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam
                .Replace(OpenTagJackpotRefNo, "\n")
                .Replace(CloseTagJackpotRefNo, string.Empty);

            base.AdjustBetTeamToRTF();
        }
    }
}
