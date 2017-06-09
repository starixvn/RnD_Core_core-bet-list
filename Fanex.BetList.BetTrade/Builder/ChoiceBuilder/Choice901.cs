namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Builder;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    public class Choice901 : IChoice
    {
        private const string RefNoDefault = "100000000000";

        public Choice901()
        {
            Template = new Choice_Template();
        }

        public Choice_Template Template { get; set; }

        public string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData = null)
        {
            var bettypeId = ticket.BetTypeId;
            ticket.BetTypeId = (int)ticket.BetId;

            var refNo = ticket.RefNo;
            ticket.RefNo = RefNoDefault;

            var choice = CreateChoice(ticket.BetTypeId);
            var betTypeName = choice.GetBetTypeName(ticket, ticketHelper, ticketData);

            ticket.BetTypeId = bettypeId;
            ticket.RefNo = refNo;

            return betTypeName;
        }

        public Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            var bettypeId = ticket.BetTypeId;
            ticket.BetTypeId = (int)ticket.BetId;

            var refNo = ticket.RefNo;
            ticket.RefNo = RefNoDefault;

            var choice = CreateChoice(ticket.BetTypeId);
            var template = choice.Render(ticket, ticketHelper, ticketData, isShowScoreMap);

            ticket.BetTypeId = bettypeId;
            ticket.RefNo = refNo;

            return template;
        }

        public List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, RTFHelper rtfHelper)
        {
            var bettypeId = ticket.BetTypeId;
            ticket.BetTypeId = (int)ticket.BetId;

            var refNo = ticket.RefNo;
            ticket.RefNo = RefNoDefault;

            var choice = CreateChoice(ticket.BetTypeId);
            var template = choice.RenderRTF(ticket, ticketHelper, ticketData, isShowScoreMap, rtfHelper);

            ticket.BetTypeId = bettypeId;
            ticket.RefNo = refNo;

            return template;
        }

        private static IChoice CreateChoice(int betType)
            => new BetListBaseBuilder().CreateChoiceBuilder(betType);
    }
}