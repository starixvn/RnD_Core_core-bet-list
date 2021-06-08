namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Core.Entities;

    /// <summary>
    /// Soccer 5
    /// </summary>
    public class Choice3604 : Choice3601
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                var betChoices = GetBetChoices(ticket, ticketHelper);

                Template.betTeam = betChoices;
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        private static string GetBetChoices(ITicket ticket, ITicketHelper ticketHelper)
        {
            var contentTransDesc = Split(ticket.TransDesc, '=');
            if (contentTransDesc.Length == numberOfExpressionsOfEachProperty)
            {
                var betChoiceId = contentTransDesc[1].Trim();

                return ticketHelper.GetResourceData(BetChoiceRefId, betChoiceId);
            }

            return string.Empty;
        }

        private static string[] Split(string value, char separator)
            => value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}