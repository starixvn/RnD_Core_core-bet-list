namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Builder;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Templates;

    public class Choice911 : IChoice
    {
        private const string RefNoDefault = "100000000000";
        private const string CashOutLabelHtmlFormat = "<div class=\"underdog\">{0}</div>";

        public Choice911()
        {
            Template = new Choice_Template();
        }

        public Choice_Template Template { get; set; }

        public string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var getBetTypeName = new Func<IChoice, string>((IChoice choice) =>
            {
                return choice.GetBetTypeName(ticket, ticketHelper, ticketData);
            });

            var betTypeName = GetResult(getBetTypeName, ticket);

            return betTypeName;
        }

        public Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            var render = new Func<IChoice, Choice_Template>((IChoice choice) =>
            {
                return choice.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
            });

            var template = GetResult(render, ticket);

            var cashOutLabelHtml = string.Format(CashOutLabelHtmlFormat, BuildCashOutLabel(ticket.RefNo));
            template.SetValue(cashOutLabelHtml + template);

            return template;
        }

        public List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, RTFHelper rtfHelper)
        {
            var renderRTF = new Func<IChoice, List<IRichTextString>>((IChoice choice) =>
            {
                return choice.RenderRTF(ticket, ticketHelper, ticketData, isShowScoreMap, rtfHelper);
            });

            var template = GetResult(renderRTF, ticket);

            return new List<IRichTextString> { AddCashOutLabel(template[0], ticket.RefNo, rtfHelper) };
        }

        protected virtual string BuildCashOutLabel(string originalTicketRefNo)
            => CoreBetList.CashOutLabel + " " + originalTicketRefNo;

        private T GetResult<T>(Func<IChoice, T> render, ITicket ticket)
        {
            var bettypeId = ticket.BetTypeId;
            ticket.BetTypeId = ticket.OriginalBetType;
            var refNo = ticket.RefNo;
            ticket.RefNo = RefNoDefault;

            var choice = CreateChoice(ticket.BetTypeId);
            var result = render(choice);

            ticket.BetTypeId = bettypeId;
            ticket.RefNo = refNo;

            return result;
        }

        private IChoice CreateChoice(int betType)
        {
            var builder = new BetListBaseBuilder();

            return builder.CreateChoiceBuilder(betType);
        }

        private IRichTextString AddCashOutLabel(IRichTextString originalTicketChoice, string originalTicketRefNo, RTFHelper rtfHelper)
        {
            var cashOutLabel = $"{BuildCashOutLabel(originalTicketRefNo)}\n";
            var formattingRuns = rtfHelper.GetFormattingRuns(originalTicketChoice);
            var text = new HSSFRichTextString(cashOutLabel + originalTicketChoice.String);

            rtfHelper.ApplyFormattingRuns(text, formattingRuns, cashOutLabel.Length);

            text.ApplyFont(0, cashOutLabel.Length, rtfHelper.PosFont);

            return text;
        }
    }
}