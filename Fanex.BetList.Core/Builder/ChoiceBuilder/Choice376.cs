namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketHelperExt;

    public class Choice376 : Choice1
    {
        public const string OptionHtmlFormat = "<div class='bettype-description'>{0}</div>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = "bettype";
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;
            var options = PenaltyShootoutComboExtensions.GetRoundInfo(ticket, ticketHelper).OrderBy(i => i.Round);
            foreach (var option in options)
            {
                Template.BetType.Append(string.Format(OptionHtmlFormat, option.Text));
            }
        }

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var betTypeInfoRows = Template.BetType.ToString().SplitHTMLIntoRows();
            rtfHelper.RTFRenderer.AddText(string.Join("\n", betTypeInfoRows));
        }        
    }
}