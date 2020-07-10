namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    ///  Progressive Jackpot
    /// </summary>
    public class Choice4298 : Choice4101
    {
        private const string OpenTagJackpotRefNo = "<div class=\"jackpotRefNo\">";
        private const string CloseTagJackpotRefNo = "</div>";
        private const string NewLine = "\n";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            if (!string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.betTeam += $"{OpenTagJackpotRefNo}{CoreBetList.forRefNo}: {ticket.BetdaqId}{CloseTagJackpotRefNo}";
            }
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam
                .Replace(OpenTagJackpotRefNo, NewLine)
                .Replace(CloseTagJackpotRefNo, string.Empty);

            base.AdjustBetTeamToRTF();
        }
    }
}