namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.StringExt;
    using NPOI.SS.UserModel;

    public class Choice5301 : Choice1
    {
        private const string ResourceId = "LG_SEL";
        private const string LeapGamingXmlTag = "p49";
        private const string Row1Format = "<div class='favorite'>{0}</div>";
        private const string Row2Format = "<div class='bettype'>{0}</div>";
        private const string Row3Format = "<div class='match'><span>{0}</span><span>&nbsp;-&nbsp;vs&nbsp;-&nbsp;</span><span>{1}</span></div>";
        private const string Row4Format = "<div class='leagueName'><span>{0}</span></div>";
        private const string Row5Format = "<div><span class='sport'>{0}</span>&nbsp;<span class='leagueName'>{1}</span></div>";
        private const string Row6Format = "<div class='underdog'>{0}</div>";
        private List<MatchInfo> ticketsInfo;
        private LeapGamingResource Resource;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            ticketsInfo = GetMatchInfoList(ticket.TransDesc);
            Resource = new LeapGamingResource(ticketHelper);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (IsSingleBet(ticket))
            {
                Template.League.LeagueName = null;
            }
            else
            {
                var linkShowDetail = string.Format("<a id='hidden{0}' href='javascript:showDetail(\"leapGaming_{1}\");'>{2}</a>", ticket.BetTypeId, ticket.RefNo, Resource.MixParlay);
                Template.League.LeagueName.leagueName = linkShowDetail;
                Template.League.LeagueName.leagueName += GetBetModeSection(ticket, ticketHelper);
            }
        }

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);
            var cssTicketSection = IsMultipleBet(ticket) ? "style='display:none;'" : string.Empty;
            var ticketSection = new StringBuilder($"<div id='leapGaming_{ticket.RefNo}' {cssTicketSection}>");
            foreach (var ticketInfo in ticketsInfo)
            {
                var betTeam = GetBetTeam(ticketInfo.BetTeam, ticketHelper);

                if (IsMultipleBet(ticket))
                {
                    ticketSection.Append("<div class='line'></div>");
                    betTeam = $"{betTeam} @{ticketInfo.Odds}";
                }

                var row1 = string.Format(Row1Format, betTeam);
                var row2 = string.Format(Row2Format, ticketHelper.GetResourceData(ResourceId, ticketInfo.BetChoice));
                var row3 = string.Format(Row3Format, ticketInfo.HomeTeam, ticketInfo.AwayTeam);
                var row4 = string.Format(Row4Format, $"{Resource.MatchDate} {ticketInfo.MatchDate}, {Resource.MatchId} {ticketInfo.MatchId}, {Resource.Season} {ticketInfo.Season}");
                var row5 = string.Format(Row5Format, ticketHelper.GetSportNameById(ticket.SportTypeId), ticketHelper.GetBetTypeNameById(ticket.BetTypeId));
                var row6 = string.Format(Row6Format, GetTicketStatus(ticketInfo.Status, ticket, ticketHelper));

                ticketSection.Append($"<div class='ticketList'>{row1}{row2}{row3}{row4}{row5}{row6}</div>");
            }

            ticketSection.Append("</div>");
            Template.League.SetValue(Template.League + ticketSection.ToString());
        }

        public override List<IRichTextString> RenderRTF(
            ITicket ticket,
            ITicketHelper ticketHelper,
            List<ITicketData> ticketData,
            bool isShowScoreMap,
            RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowScoreMap);
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            if (IsMultipleBet(ticket))
            {
                var htmlTagMatching = new Regex("</?[^<>]*>");
                var betMode = GetBetModeSection(ticket, ticketHelper);
                tmpRtfHelper.RTFRenderer.AddText(Resource.MixParlay, rtfHelper.PosFont);
                tmpRtfHelper.RTFRenderer.AddText($"\n{htmlTagMatching.Replace(betMode, string.Empty)}");
            }
            else
            {
                foreach (var ticketInfo in ticketsInfo)
                {
                    var betTeam = GetBetTeam(ticketInfo.BetTeam, ticketHelper);

                    var row1 = $"{betTeam}\n";
                    var row2 = ticketHelper.GetResourceData(ResourceId, ticketInfo.BetChoice) + "\n";
                    var row3 = $"{ticketInfo.HomeTeam} - vs - {ticketInfo.AwayTeam}\n";
                    var row4 = $"{Resource.MatchDate} {ticketInfo.MatchDate}, {Resource.MatchId} {ticketInfo.MatchId}, {Resource.Season} {ticketInfo.Season}\n";
                    var row5 = $"{ticketHelper.GetSportNameById(ticket.SportTypeId)} {ticketHelper.GetBetTypeNameById(ticket.BetTypeId)}";

                    tmpRtfHelper.RTFRenderer.AddText(row1, rtfHelper.PosFont);
                    tmpRtfHelper.RTFRenderer.AddText(row2);
                    tmpRtfHelper.RTFRenderer.AddText(row3);
                    tmpRtfHelper.RTFRenderer.AddText(row4);
                    tmpRtfHelper.RTFRenderer.AddText(row5);
                }
            }

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString> { choice };
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = string.Empty;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.Hide();
            Template.betTeamClassName = string.Empty;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildScore(ITicket ticket)
            => Template.Score.Hide();

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.sportTypeName = string.Empty;

        private static List<MatchInfo> GetMatchInfoList(string transdesc)
        {
            var matchs = new List<MatchInfo>();
            if (!string.IsNullOrEmpty(transdesc))
            {
                foreach (var matchInfo in transdesc.Split('|'))
                {
                    matchs.Add(new MatchInfo(matchInfo));
                }
            }

            return matchs;
        }

        private static string GetBetModeSection(ITicket ticket, ITicketHelper ticketHelper)
        {
            string[] items = { string.Empty, string.Empty };
            if (string.Equals(ticket.Status, BetStatus.Running, System.StringComparison.InvariantCultureIgnoreCase))
            {
                items = ticket.BetCheck.Split(',');
            }
            else
            {
                if (ticket.ExtraInfo.HasAttributeInXmlTag(LeapGamingXmlTag, "mt"))
                {
                    items = ticket.ExtraInfo.GetXmlTagAttributeValue(LeapGamingXmlTag, "mt").Split(',');
                }
            }

            var betMode = items.Length > 0 ? items[0].Trim() : string.Empty;
            var numberTicket = items.Length > 1 ? items[1].Trim() : string.Empty;
            var betmode = $"{ticketHelper.GetResourceData("LG_GT", betMode)} ({numberTicket})";

            return string.Format(Row6Format, betmode);
        }

        private static string GetTicketStatus(string status, ITicket ticket, ITicketHelper ticketHelper)
        {
            if (IsMultipleBet(ticket) && !IsRunningTicket(ticket))
            {
                return ticketHelper.GetResourceData("LG_STMT", status);
            }

            return string.Empty;
        }

        public static bool IsSingleBet(ITicket ticket)
            => (!string.IsNullOrEmpty(ticket.BetCheck) && ticket.BetCheck.Contains("GT14")) ||
            (!string.IsNullOrEmpty(ticket.ExtraInfo) && ticket.ExtraInfo.Contains("GT14"));

        private static bool IsMultipleBet(ITicket ticket)
            => !IsSingleBet(ticket);

        private static bool IsRunningTicket(ITicket ticket)
            => string.Equals(ticket.Status, BetStatus.Running, System.StringComparison.InvariantCultureIgnoreCase);

        public static string GetBetTeam(string betTeamKey, ITicketHelper ticketHelper)
        {
            var betTeam = ticketHelper.GetResourceData(ResourceId, betTeamKey);

            return string.IsNullOrEmpty(betTeam) ? betTeamKey : betTeam;
        }
    }
}
