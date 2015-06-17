namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    public class Choice1703 : Choice1701
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;@&nbsp;<span class='handicap custom'>{1}</span></div>";
        private const string LeagueFormat = "<div class='league'><span class='sport'>{0}</span><span class='leagueName'>&nbsp;{1}&nbsp;{2}</span></div>";

        public override List<IRichTextString> RenderRTF(
            ITicket ticket,
            ITicketHelper ticketHelper,
            List<ITicketData> ticketData,
            bool isShowScoreMap,
            RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowScoreMap);
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            var betTypeName = GetBetTypeName(ticket.BetTypeId, ticketHelper);
            tmpRtfHelper.RTFRenderer.AddText(string.Format("{0} {1}", Template.League.sportTypeName, betTypeName));

            var ticketDetails = GetTicketReference(ticket, ticketData);
            foreach (var ticketDetail in ticketDetails)
            {
                var odds = ConvertByBetType.Odds(ticketDetail.Odds, ticket.BetTypeId, ticket.OddsType);
                var betTeam = ticketHelper.GetResourceData(GetResourceId(), ticketDetail.BetTeam);
                var league = string.Format("{0} {1} {2}", ticketHelper.GetSportNameById(ticket.SportTypeId), betTypeName, GetBetChoice(ticketDetail.TransDesc));

                tmpRtfHelper.RTFRenderer.AddText("\n");
                tmpRtfHelper.RTFRenderer.AddText(string.Format("{0} @ {1}", betTeam, odds), rtfHelper.PosFont);
                tmpRtfHelper.RTFRenderer.AddText("\n");
                tmpRtfHelper.RTFRenderer.AddText(league, rtfHelper.NormalFont);
            }

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();
            return new List<IRichTextString>() { choice };
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = GetBetTypeName(ticket.BetTypeId, ticketHelper);

            if (IsNotRunningTicket(ticket))
            {
                betTypeName = string.Format("<a id='hidden{0}' href='javascript:showDetail(\"parlay_{1}\");'>{2}</a>", ticket.BetTypeId, ticket.RefNo, betTypeName);
            }

            Template.League.LeagueName.leagueName = betTypeName;
        }

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);
            var betTypeName = GetBetTypeName(ticket.BetTypeId, ticketHelper);

            var ticketDetails = GetTicketReference(ticket, ticketData);
            var choiceDetails = string.Format("<div id='parlay_{0}' style='display:none;'>", ticket.RefNo);

            if (ticketDetails.Count > 0 && IsNotRunningTicket(ticket))
            {
                int count = 1;
                foreach (var ticketDetail in ticketDetails)
                {
                    var odds = ConvertByBetType.Odds(ticketDetail.Odds, ticket.BetTypeId, ticket.OddsType);
                    var betTeam = string.Format(BetTeamFormat, ticketHelper.GetResourceData(GetResourceId(), ticketDetail.BetTeam), odds);
                    var league = string.Format(LeagueFormat, ticketHelper.GetSportNameById(ticket.SportTypeId), betTypeName, GetBetChoice(ticketDetail.TransDesc));

                    choiceDetails += string.Format("<div class='ticketList'>{0}{1}</div>", betTeam, league);
                    if (count++ < ticketDetails.Count)
                    {
                        choiceDetails += "<div class='line'></div>";
                    }
                }
            }
            else
            {
                choiceDetails += string.Format("<div class='no-details'>{0}</div>", CoreBetList.ThereIsNoTicketDetailAvailable);
            }

            choiceDetails += "</div>";
            Template.League.SetValue(Template.League.ToString() + choiceDetails);
        }

        private bool IsNotRunningTicket(ITicket ticket)
        {
            return !string.Equals(ticket.Status, BetStatus.Running, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetBetChoice(string transDesc)
        {
            if (string.IsNullOrWhiteSpace(transDesc))
            {
                return transDesc;
            }

            var array = transDesc.Split(';');

            return array[0].ToLowerInvariant().Replace("betchoice=", string.Empty).ToUpperInvariant();
        }
    }
}