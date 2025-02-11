namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Choice11012 : Choice11011
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            var actionName = string.Empty;
            if (ticket.ExtraInfo.HasAttributeInXmlTag("p81", "a"))
            {
                var actionId = ticket.ExtraInfo.GetXmlTagAttributeValue("p81", "a");
                actionName = ticketHelper.GetResourceData("SBC_Deduction", actionId);
            }
            Template.Match.homeTeam = actionName;
        }
    }
}