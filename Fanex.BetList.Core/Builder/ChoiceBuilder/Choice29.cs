namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using BetListNPOI.SS.UserModel;
    using Constants;
    using Entities;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Net;
    using Fanex.BetList.Core.Utils.StringExt;
    using System.Linq;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Choice29 : Choice1
    {
        private const string RefId = "ComboParlay";
        private const string ResourceId = "LuckyBet";
        private const string BetBuilderPlusRefId = "BetBuilderPlus";
        private const int OddsSpreadForBetBuilderPlus = 3;
        private const string BetBuilderPlusBettypeNameSuffix = "<sup>+</sup>";

        #region Build Choice

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTypeName = GetBetTypeName(ticket, ticketHelper);

            var showSystemParlaySubbetsFunc = BuildShowSystemParlaySubbetsFunc(ticket, ticketHelper);

            var detailSystemParlayDiv = BuildSystemParlayDetailDiv(ticket, ticketHelper, betTypeName);

            var combinationOfferName = BuildCombinationOfferName(ticket, ticketHelper);

            var mixParlayMarkups = BuildMixParlayMarkups(
                                        showSystemParlaySubbetsFunc,
                                        betTypeName,
                                        combinationOfferName,
                                        ticket.TransId.ToString(CultureInfo.InvariantCulture),
                                        ticketHelper.Index.ToString(CultureInfo.InvariantCulture),
                                        detailSystemParlayDiv);

            Template.SetValue(mixParlayMarkups);
        }

        public override string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData = null)
        {
            if(IsBetBuilderPlus(ticket))
            {
                return ticketHelper.GetResourceData(BetBuilderPlusRefId, OddsSpreadForBetBuilderPlus.ToString())
                            .Replace("+", BetBuilderPlusBettypeNameSuffix);
            }

            if (ticket.BetTeam == "1")
            {
                return ticketHelper.GetBetTypeNameById(BetTypes.MixParlay);
            }

            var bettypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            if (ticket.BetCheck == ticket.BetTeam && ticket.Handicap1 == 0)
            {
                bettypeName += " " + ticketHelper.GetResourceData(RefId, ResourceId);
            }

            return bettypeName;
        }

        private static string BuildShowSystemParlaySubbetsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {
            return string.IsNullOrWhiteSpace(ticketHelper.ClientSystemParlaySubbetsDetailFunction)
                        ? JoinSystemParlaySubbetsFunc(ticket, ticketHelper)
                        : ticketHelper.ClientSystemParlaySubbetsDetailFunction;
        }

        private static string BuildSystemParlayDetailDiv(ITicket ticket, ITicketHelper ticketHelper, string betTypeName)
        {
            return ticketHelper.ShowSystemParlayDetail && IsAcceptedTicket(ticket) && !IsBetBuilderPlus(ticket)
                    ? JoinDetailSystemParlayDiv(ticket, ticketHelper, betTypeName)
                    : string.Empty;
        }

        private static bool IsAcceptedTicket(ITicket ticket)
        {
            string status = ticket.Status.ToLowerInvariant();

            return status == BetStatus.Running
                || status == BetStatus.Won
                || status == BetStatus.Lose
                || status == BetStatus.Draw;
        }

        private static string JoinDetailSystemParlayDiv(ITicket ticket, ITicketHelper ticketHelper, string betTypeName)
        {
            var systemParlayDetailJsFunction = BuildSystemParlayDetailJsFunction(ticket, ticketHelper, betTypeName);

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";

            return $"<div class=\"detail\" onclick=\"{systemParlayDetailJsFunction}\">{detailLink}</div>";
        }

        private static string BuildSystemParlayDetailJsFunction(ITicket ticket, ITicketHelper ticketHelper, string betTypeName)
        {
            return string.IsNullOrWhiteSpace(ticketHelper.ClientDetailSystemParlayFunction)
                        ? JoinSystemParlayDetailJsFunc(
                            ticket.RefNo,
                            ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),                           
                            ticket.CustId.ToString(),
                            ticket.BetTeam,
                            betTypeName)
                        : ticketHelper.ClientDetailSystemParlayFunction;
        }

        private static string JoinSystemParlayDetailJsFunc(string refNo, string winlossDate, string custId, string betTeam, string betTypeName)
            => $"showCombMPDetail('{refNo}', '{winlossDate}', {custId}, '{betTeam}', '{betTypeName}');";

        private static string JoinSystemParlaySubbetsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {   
            if(IsBetBuilderPlus(ticket))
            {
                return JoinBetBuilderPlusJsFunc(ticket, ticketHelper);
            }

            return IsOldSystemParlay(ticket.BetCheck)
                ? $"javascript:showCombMP('{ticket.TransId}', {ticketHelper.Index}, '{ticket.RefNo}', {ticket.CustId});"
                : string.Format(
                    "javascript:showNewCombMP('{0}', {1}, '{2}', {3}, {4}, '{5}', '{6}', {7});",
                    ticket.TransId,
                    ticketHelper.Index,
                    ticket.RefNo,
                    ticket.Handicap1,
                    ticket.Handicap2,
                    ticket.BetCheck,
                    ticket.BetTeam,
                    ticket.CustId);
        }

        private static string JoinBetBuilderPlusJsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transDesc = string.IsNullOrEmpty(ticket.TransDesc) ? "{}" : WebUtility.HtmlEncode(ticket.TransDesc);

            return $"javascript:showBetBuilderPlus('{ticket.TransId}', {ticketHelper.Index}, '{ticket.RefNo}', {ticket.CustId}, {transDesc}, {(int)ticket.Handicap1}, {(int)ticket.Handicap2}, '{ticket.BetCheck}', '{ticket.BetTeam}', '{ticket.Currency}', {ticket.Stake});";
        }

        private static string BuildCombinationOfferName(ITicket ticket, ITicketHelper ticketHelper)
        {
            if(IsBetBuilderPlus(ticket))
            {
                return BuildCombinationOfferNameForBetBuilderPlus(ticket, ticketHelper);
            }

            return IsOldSystemParlay(ticket.BetCheck)
                ? BuildCombinationOfferNameForOldSystemParlay(ticket, ticketHelper)
                : BuildCombinationOfferNameForNewSystemParlay(ticket, ticketHelper);
        }

        private static bool IsOldSystemParlay(string betCheck) => string.IsNullOrEmpty(betCheck);

        private static string GetBetBuilderPlusCombinationOfferName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var combinationBetBuilderPlusOfferNames = ticketHelper.GetCombinationMPOfferNameList(ticket.BetTeam, ticket.BetCheck, ticket.Handicap1, ticket.Handicap2);
            
            if (combinationBetBuilderPlusOfferNames != null && combinationBetBuilderPlusOfferNames.Count > 0)
            {
                return combinationBetBuilderPlusOfferNames[0];
            }

            return string.Empty;
        }

        private static string BuildCombinationOfferNameForBetBuilderPlus(ITicket ticket, ITicketHelper ticketHelper)
        {
            var combinationOfferName = GetBetBuilderPlusCombinationOfferName(ticket, ticketHelper);
            var extraVoidTicketClass = ticket.Status?.ToLowerInvariant() == BetStatus.Void ? "void" : string.Empty;

            return $"<div class=\"offer-name-list {extraVoidTicketClass}\" id=\"combinationOfferName_{ticket.TransId}{ticketHelper.Index}\"><div>{combinationOfferName}</div></div>";
        }

        private static string BuildCombinationOfferNameForOldSystemParlay(ITicket ticket, ITicketHelper ticketHelper)
            => string.Format(
                "<span class=\"offer-name\" id=\"combinationOfferName_{0}{1}\"> - {2}</span>",
                ticket.TransId,
                ticketHelper.Index,
                ticketHelper.GetCombinationMPOfferName(ticket.BetTeam));

        private static string BuildCombinationOfferNameForNewSystemParlay(ITicket ticket, ITicketHelper ticketHelper)
        {
            var combinationMPOfferNames = ticketHelper.GetCombinationMPOfferNameList(ticket.BetTeam, ticket.BetCheck, ticket.Handicap1, ticket.Handicap2);
            var offerNameList = new StringBuilder();

            if (combinationMPOfferNames != null && combinationMPOfferNames.Any())
            {
                foreach (var name in combinationMPOfferNames)
                {
                    offerNameList.Append($"<div>{name}</div>");
                }
            }

            return string.Format(
                "<div class=\"offer-name-list\" id=\"combinationOfferName_{0}{1}\">{2}</div>",
                ticket.TransId,
                ticketHelper.Index,
                offerNameList);
        }

        private static string BuildMixParlayMarkups(
                        string showSystemParlaySubbetsFunction,
                        string betTypeName,
                        string combinationOfferName,
                        string transId,
                        string transIndex,
                        string systemParlayDetailDiv)
        {
            var combinationLink = $"<a id=\"hidden0\" href=\"{showSystemParlaySubbetsFunction}\">{betTypeName}</a>";

            return string.Format(
                "<div class=\"combinationLink\">{0}{1}</div><div id=\"divEvent_{2}{3}\" style=\"display: none\" ></div>{4}",
                combinationLink,
                combinationOfferName,
                transId,
                transIndex,
                systemParlayDetailDiv);
        }

        private static bool IsBetBuilderPlus(ITicket ticket) => ticket.OddsSpread == OddsSpreadForBetBuilderPlus;

        #endregion Build Choice

        #region Build RTF Choice

        public override List<IRichTextString> RenderRTF(
                                ITicket ticket,
                                ITicketHelper ticketHelper,
                                List<ITicketData> ticketData,
                                bool isShowScoreMap,
                                RTFHelper rtfHelper)
        {
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            tmpRtfHelper.RTFRenderer.AddText(GetBetTypeName(ticket, ticketHelper).Replace(BetBuilderPlusBettypeNameSuffix, " +"), tmpRtfHelper.PosFont);

            BuildRTFCombinationMPOfferNames(tmpRtfHelper, ticket, ticketHelper);

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString> { choice };
        }

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var betTypeName = Template.BetType.betTypeName
                .Replace(BetBuilderPlusBettypeNameSuffix, "+")
                .StripHTML();

            rtfHelper.RTFRenderer.AddText(betTypeName);
        }

        private static void BuildRTFCombinationMPOfferNames(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
        {
            if(IsBetBuilderPlus(ticket))
            {
                BuildRTFCombinationMPOfferNamesForBetBuilderPlus(tmpRtfHelper, ticket, ticketHelper);
            }
            else if (IsOldSystemParlay(ticket.BetCheck))
            {
                BuildRTFCombinationMPOfferNamesForOldSystemParlay(tmpRtfHelper, ticket, ticketHelper);
            }
            else
            {
                BuildRTFCombinationMPOfferNamesForNewSystemParlay(tmpRtfHelper, ticket, ticketHelper);
            }
        }

        private static void BuildRTFCombinationMPOfferNamesForOldSystemParlay(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
        {
            tmpRtfHelper.RTFRenderer.AddText($" - {ticketHelper.GetCombinationMPOfferName(ticket.BetTeam)}");
        }

        private static void BuildRTFCombinationMPOfferNamesForNewSystemParlay(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
        {
            var combinationMPOfferNames = ticketHelper.GetCombinationMPOfferNameList(ticket.BetTeam, ticket.BetCheck, ticket.Handicap1, ticket.Handicap2);

            if (combinationMPOfferNames != null)
            {
                foreach (var offerName in combinationMPOfferNames)
                {
                    tmpRtfHelper.RTFRenderer.AddText($"\n{offerName}", tmpRtfHelper.NormalFont);
                }
            }
        }

        private static void BuildRTFCombinationMPOfferNamesForBetBuilderPlus(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
        {
            var combinationOfferName = GetBetBuilderPlusCombinationOfferName(ticket, ticketHelper);
            if(ticket.Status?.ToLowerInvariant() == BetStatus.Void)
            {
                tmpRtfHelper.RTFRenderer.AddText($"\n{combinationOfferName}", tmpRtfHelper.NormalFontCrossed);
            }
            else
            {
                tmpRtfHelper.RTFRenderer.AddText($"\n{combinationOfferName}", tmpRtfHelper.NormalFont);
            }
        }
        #endregion Build RTF Choice
    }
}