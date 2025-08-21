namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils.Parsers;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;
    using Templates;

    public class ChoiceLCNoTableCode : Choice1
    {
        private const string JackpotTransIdInfo = "jpw";
        protected virtual string RefIdForGameType => string.Empty;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = IsDetailTicket(ticket) ? ticketHelper.GetResourceData(RefIdForGameType, ticket.BetTypeId.ToString()) : null;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
            => Template.Match = null;

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (IsDetailTicket(ticket))
            {
                var _parseTransId3rd = GetTransId3rd(ticket.BetCheck);
                Template.BetType.betTypeName = $"{LiveCasinoConstants.OpenBetIdTag}{CoreBetList.BetID}: {_parseTransId3rd}{LiveCasinoConstants.CloseBetIdTag}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (IsSummaryTicket(ticket))
            {
                Template.League.Append(BuildDetailLink(ticket, ticketHelper));
            }
            else
            {
                Template.League.LeagueName.leagueName += BuildGameId(ticket);

                if(ticket.TicketHasJackpotInfo())
                {
                    Template.League.Append(BuildShowJackpotWonDetails(ticket, ticketHelper));
                }
            }
        }

        protected virtual string BuildShowJackpotWonDetails(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transId = ExtraInfoParser.ParseExtraInfo(ticket.JackpotInfo, ',')[JackpotTransIdInfo];
            var sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            var detailLinkMarkup =
                $"<div class=\"detail jackpot-detail\" onclick=\"showLCJackpotWonDetails('{transId}', '{ticket.CustId}', '{ticket.BetTypeId}', '{ticket.WinlostDate}', '{sportTypeName}');\">" +
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.JackpotWonDetails}</a>" +
                $"</div>";

            return detailLinkMarkup;
        }

        protected virtual string BuildGameId(ITicket ticket)
            => $" {LiveCasinoConstants.OpenGameIdTag}{ticket.MatchCode}{LiveCasinoConstants.CloseGameIdTag}";

        public static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return $"{LiveCasinoConstants.OpenBetTypeNameTag}{betTypeName}{LiveCasinoConstants.CloseBetTypeNameTag}";
        }

        protected override void AdjustBetTeamToRTF()
        {
            RemoveHtmlTagFromLeagueName();
            RemoveHtmlTagFromBetTypeName();
        }

        protected virtual string GetTransId3rd(string transId3rd)
        {
            const string transId3rdSeparator = "|$|";
            if (transId3rd == null || !transId3rd.Contains(transId3rdSeparator))
            {
                return transId3rd;
            }

            var transIdParts = transId3rd.Split(new[] { transId3rdSeparator }, StringSplitOptions.RemoveEmptyEntries);

            return transIdParts[transIdParts.Length - 1];
        }

        protected virtual bool IsDetailTicket(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.BetCheck);

        protected virtual bool IsSummaryTicket(ITicket ticket)
            => !IsDetailTicket(ticket);


        private static Choice_League_Block BuildDetailLink(ITicket ticket, ITicketHelper ticketHelper)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket, ticketHelper);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail live-casino-detail\" onclick='")
                .Append(detailFunction)
                .Append("'>")
                .Append("<a class=\"detail-link\" href=\"javascript:void(&quot;&quot;);\">")
                .Append(CoreBetList.details)
                .Append("</a>")
                .Append("</div>");

            var detailTemplateBlock = new Choice_League_Block();

            detailTemplateBlock.SetValue(detailLinkMarkupBuilder.ToString());

            return detailTemplateBlock;
        }

        private static string BuildDetailFunction(ITicket ticket, ITicketHelper ticketHelper)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showLiveCasinoBetDetails(&quot;")
                .Append(ticket.TransId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.WinlostDate.ToString(CultureInfo.InvariantCulture))
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.CustId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.GetTransactionType())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.SportTypeId)
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticketHelper.GetSportNameById(ticket.SportTypeId))
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }


        private void RemoveHtmlTagFromLeagueName()
        {
            var htmlTagMatching = new Regex("</?[^<>]*>");
            Template.League.LeagueName.leagueName = htmlTagMatching.Replace(Template.League.LeagueName.leagueName, string.Empty);
        }

        private void RemoveHtmlTagFromBetTypeName()
        {
            Template.BetType.betTypeName = Template.BetType.betTypeName
                .Replace(LiveCasinoConstants.OpenBetIdTag, string.Empty)
                .Replace(LiveCasinoConstants.CloseBetIdTag, string.Empty);
        }
    }
}