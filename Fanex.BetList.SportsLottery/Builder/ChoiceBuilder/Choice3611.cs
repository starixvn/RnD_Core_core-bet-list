namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice3611 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticketHelper.GetResourceData("SportsLotteryChoice", GetSplitValue(ticket.TransDesc));

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var gameTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            Template.League.LeagueName.leagueName = $"{gameTypeName} [{GetTransId3rd(ticket.BetCheck)}]";
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetType(ticket, ticketHelper, ticketData);
            Template.BetType.Visible = false;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
            => rtfHelper.RTFRenderer.AddText(string.Empty);

        private static string GetSplitValue(string strValue)
        {
            string[] strTemp = strValue.Split('=');
            return strTemp.Length > 1 ? strTemp[1] : string.Empty;
        }

        protected virtual string GetTransId3rd(string transId3rd)
        {
            const string transId3rdSeparator = "|$|";
            if (transId3rd == null || !transId3rd.Contains(transId3rdSeparator))
            {
                return transId3rd;
            }

            var transIdParts = transId3rd.Split(new string[] { transId3rdSeparator }, StringSplitOptions.RemoveEmptyEntries);

            return transIdParts[transIdParts.Length - 1];
        }
    }
}
