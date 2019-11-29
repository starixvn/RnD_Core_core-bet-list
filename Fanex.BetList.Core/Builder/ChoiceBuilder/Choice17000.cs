namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Choice17000 : Choice1
    {
        private const string nonPeakHoursPromotionXmlTag = "p37";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var promotionMonth = GetPromotionMonth(ticket, ticketHelper);
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            Template.betTeam = string.IsNullOrEmpty(promotionMonth)
                ? betTypeName
                : string.Format(CoreBetList.SabaLuckySpinProgramPattern, promotionMonth, betTypeName);
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.LeagueName = null;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            const string betTypeCssClass = "bettype";

            Template.betTeamClassName = betTypeCssClass;

            if (!string.IsNullOrEmpty(ticket.ExtraInfo))
            {
                Template.Handicap.handicap = $"- {GetPromotionId(ticket.ExtraInfo)}";
            }
        }

        private static string GetPromotionMonth(ITicket ticket, ITicketHelper ticketHelper)
        {
            const string promotionMonthAttributeName = "d";

            var promotionStartDate = ticket.ExtraInfo.GetXmlTagAttributeValue(nonPeakHoursPromotionXmlTag, promotionMonthAttributeName);

            if (DateTime.TryParse(promotionStartDate, out DateTime parsedDateTime))
            {
                const string monthRefId = "month";
                const string expectedMonthFormat = "00";

                var month = parsedDateTime.Month.ToString(expectedMonthFormat);

                return ticketHelper.GetResourceData(monthRefId, month);
            }

            return string.Empty;
        }

        private static string GetPromotionId(string extraInfo)
        {
            const string promotionIdAttributeName = "i";

            return extraInfo.GetXmlTagAttributeValue(nonPeakHoursPromotionXmlTag, promotionIdAttributeName);
        }

        protected override void BuildEventKickoffDateTime(ITicket ticket)
            => Template.EventDate = null;
    }
}