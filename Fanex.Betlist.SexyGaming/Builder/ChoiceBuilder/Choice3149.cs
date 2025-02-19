namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System;
    using System.Collections.Generic;

    public class Choice3149 : Choice3101
    {
        private const string TipChoiceKey = "gift";
        private const string DealerKey = "dealer";
        private const string OpenGameIdTag = "<span class='game-id'>";
        private const string CloseGameIdTag = "</span>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = GetTransDescData(TipChoiceKey);
        }

        protected override string BuildGameId(ITicket ticket)
            => $" {OpenGameIdTag}{GetTransDescData(DealerKey)}{CloseGameIdTag}";
    }
}