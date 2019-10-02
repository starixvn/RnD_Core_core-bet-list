namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;

    public class Choice17000 : Choice1
    {
        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

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
                Template.Handicap.handicap = $" - {GetPromotionId(ticket.ExtraInfo)}";
            }
        }

        private static string GetPromotionId(string extraInfo)
        {
            const string nonPeakHoursPromotionXmlTag = "p37";
            const string requiredAttributeName = "i";

            return extraInfo.GetXmlTagAttributeValue(nonPeakHoursPromotionXmlTag, requiredAttributeName);
        }

        protected override void BuildEventKickoffDateTime(ITicket ticket)
            => Template.EventDate = null;
    }
}