namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using NPOI.SS.UserModel;

    public class Choice901 : IChoice
    {
        public Choice_Template Template { get; set; }

        public Choice901()
        {
            Template = new Choice_Template();
        }

        public Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            var choice = CreateChoice((int)ticket.BetId);

            return choice.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        public List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, RTFHelper rtfHelper)
        {
            var choice = CreateChoice((int)ticket.BetId);

            return choice.RenderRTF(ticket, ticketHelper, ticketData, isShowScoreMap, rtfHelper);
        }

        private IChoice CreateChoice(int betType)
        {
            var builder = new BetListBaseBuilder();

            return builder.CreateChoiceBuilder(betType);
        }
    }
}
