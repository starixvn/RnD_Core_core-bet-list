namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;

    /// <summary>
    /// Virtual Soccer - Double Chance
    /// </summary>
    public class Choice1224 : Choice24
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.AppendKickOffTime(ticket);
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            rtfHelper.RTFRenderer.AddText($"\n{VirtualSportsChoiceUtils.ParseKickOffTime(ticket)}");
        }
    }
}