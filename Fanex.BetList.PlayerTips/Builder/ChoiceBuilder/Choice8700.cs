namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.StringExt;
    using System.Collections.Generic;

    public class Choice8700 : Choice1
    {
        private const string PlayerTipsXmlTag = "p74";
        private const string SteamingSrcAttribute = "ss";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;
            if(ticket.ExtraInfo.HasAttributeInXmlTag(PlayerTipsXmlTag, SteamingSrcAttribute))
            {
                betTeam = ticket.ExtraInfo.GetXmlTagAttributeValue(PlayerTipsXmlTag, SteamingSrcAttribute);
            }
            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildEventKickoffDateTime(ITicket ticket)
        {
            Template.EventDate.Show();
            Template.EventDate.eventDate = Formatter.FormatDateTimeEn12ShortMonthNoSec(ticket.EventDate);
        }
    }
}