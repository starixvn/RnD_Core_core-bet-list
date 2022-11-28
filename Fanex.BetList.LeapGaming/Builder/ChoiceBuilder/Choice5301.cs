namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils.StringExt;
    using BetListNPOI.SS.UserModel;

    public class Choice5301 : Choice1
    {
        private const string LeapGamingXmlTag = "p49";
        private const string Row1Format = "<div class='favorite'>{0}</div>";
        private const string Row2Format = "<div class='bettype'>{0}</div>";
        private const string Row3Format = "<div class='match'><span>{0}</span><span>&nbsp;-&nbsp;vs&nbsp;-&nbsp;</span><span>{1}</span></div>";
        private const string Row4Format = "<div><span class='sport'>{0}</span>&nbsp;<span class='leagueName'>{1}</span></div>";
        private const string Row5Format = "<div class='underdog'>{0}</div>";
        private List<MatchInfo> ticketsInfo;
        private LeapGamingResource Resource;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            ticketsInfo = GetMatchInfoList(ticket.TransDesc, ticketHelper);
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
                var sectionName = $"{ticketHelper.GetSportNameById(ticket.SportTypeId)} - {Resource.MixParlay}";
                var linkShowDetail = string.Format("<a id='hidden{0}' href='javascript:showDetail(\"leapGaming_{1}\");'>{2}</a>", ticket.BetTypeId, ticket.RefNo, sectionName);
                Template.League.LeagueName.leagueName = linkShowDetail;
                Template.League.LeagueName.leagueName += GetBetModeSection(ticket, ticketHelper);
            }
        }

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);
            var cssTicketSection = IsMultipleBet(ticket) ? "style='display:none;'" : string.Empty;
            var ticketSection = new StringBuilder($"<div id='leapGaming_{ticket.RefNo}' {cssTicketSection}>");
            if (ticketsInfo.Any())
            {
                foreach (var ticketInfo in ticketsInfo)
                {
                    var betTeam = ticketInfo.BetTeam;

                    if (IsMultipleBet(ticket))
                    {
                        ticketSection.Append("<div class='line'></div>");
                        betTeam = $"{betTeam} @{ticketInfo.Odds}";
                    }

                    var row1 = string.Format(Row1Format, betTeam);
                    var row2 = string.Format(Row2Format, ticketInfo.BetChoice);
                    var row3 = string.Format(Row3Format, ticketInfo.HomeTeam, ticketInfo.AwayTeam);
                    var row4 = string.Format(Row4Format, ticketHelper.GetSportNameById(ticket.SportTypeId), ticketHelper.GetBetTypeNameById(ticket.BetTypeId));
                    var row5 = string.Format(Row5Format, ticketInfo.Status);

                    ticketSection.Append($"<div class='ticketList'>{row1}{row2}{row3}{row4}{row5}</div>");
                }
            }
            else
            {
                if(IsSingleBet(ticket))
                {
                    var row4 = string.Format(Row4Format, ticketHelper.GetSportNameById(ticket.SportTypeId), ticketHelper.GetBetTypeNameById(ticket.BetTypeId));
                    ticketSection = new StringBuilder($"<div><div class='ticketList'>{row4}</div>");
                }                
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
                var sectionName = $"{ticketHelper.GetSportNameById(ticket.SportTypeId)} - {Resource.MixParlay}";
                tmpRtfHelper.RTFRenderer.AddText(sectionName, rtfHelper.PosFont);
                tmpRtfHelper.RTFRenderer.AddText($"\n{htmlTagMatching.Replace(betMode, string.Empty)}");
            }
            else
            {
                foreach (var ticketInfo in ticketsInfo)
                {
                    var row1 = $"{ticketInfo.BetTeam}\n";
                    var row2 = $"{ticketInfo.BetChoice}\n";
                    var row3 = $"{ticketInfo.HomeTeam} - vs - {ticketInfo.AwayTeam}\n";
                    var row4 = $"{ticketHelper.GetSportNameById(ticket.SportTypeId)} {ticketHelper.GetBetTypeNameById(ticket.BetTypeId)}\n";
                    var row5 = ticketInfo.Status;

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

        private static List<MatchInfo> GetMatchInfoList(string transdesc, ITicketHelper ticketHelper)
        {
            var matchs = new List<MatchInfo>();
            if (!string.IsNullOrEmpty(transdesc))
            {
                foreach (var matchInfo in transdesc.Split('|'))
                {
                    matchs.Add(new MatchInfo(matchInfo, ticketHelper));
                }
            }

            return matchs;
        }

        private static string GetBetModeSection(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betMode = ticket.BetCheck;
            if (!IsRunningRejectTicket(ticket) && ticket.ExtraInfo.HasAttributeInXmlTag(LeapGamingXmlTag, "mt"))
            {
                betMode = ticket.ExtraInfo.GetXmlTagAttributeValue(LeapGamingXmlTag, "mt");
            }

            betMode = $"{ticketHelper.GetResourceData("LG_MTPL", betMode)}";
            return string.Format(Row5Format, betMode);
        }

        public static bool IsSingleBet(ITicket ticket)
            => (!string.IsNullOrEmpty(ticket.BetCheck) && ticket.BetCheck == "1") ||
            (!string.IsNullOrEmpty(ticket.ExtraInfo) && ticket.ExtraInfo.GetXmlTagAttributeValue(LeapGamingXmlTag, "mt") == "1");

        private static bool IsMultipleBet(ITicket ticket)
            => !IsSingleBet(ticket);

        private static bool IsRunningRejectTicket(ITicket ticket)
            => string.Equals(ticket.Status, BetStatus.Running, StringComparison.InvariantCultureIgnoreCase) ||
            string.Equals(ticket.Status, BetStatus.Reject, StringComparison.InvariantCultureIgnoreCase);
    }
}
