namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Choice29 : Choice1
    {
        private const string RefId = "ComboParlay";
        private const string ResourceId = "LuckyBet";

        #region Build Choice

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTypeName = GetBetTypeName(ticket, ticketHelper);

            var showSystemParlaySubbetsFunc = BuildShowSystemParlaySubbetsFunc(ticket, ticketHelper);

            var detailSystemParlayDiv = BuildSystemParlayDetailDiv(ticket, ticketHelper);

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

        private static string BuildSystemParlayDetailDiv(ITicket ticket, ITicketHelper ticketHelper)
        {
            return ticketHelper.ShowSystemParlayDetail && IsAcceptedTicket(ticket)
                    ? JoinDetailSystemParlayDiv(ticket, ticketHelper)
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

        private static string JoinDetailSystemParlayDiv(ITicket ticket, ITicketHelper ticketHelper)
        {
            var systemParlayDetailJsFunction = BuildSystemParlayDetailJsFunction(ticket, ticketHelper);

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";

            return $"<div class=\"detail\" onclick=\"{systemParlayDetailJsFunction}\">{detailLink}</div>";
        }

        private static string BuildSystemParlayDetailJsFunction(ITicket ticket, ITicketHelper ticketHelper)
        {
            return string.IsNullOrWhiteSpace(ticketHelper.ClientDetailSystemParlayFunction)
                        ? JoinSystemParlayDetailJsFunc(
                            ticket.RefNo,
                            ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                            ticket.TransId.ToString(CultureInfo.InvariantCulture),
                            ticket.BetId.ToString(),
                            ticket.CustId.ToString(),
                            ticket.BetTeam)
                        : ticketHelper.ClientDetailSystemParlayFunction;
        }

        private static string JoinSystemParlayDetailJsFunc(string refNo, string winlossDate, string transId, string betId, string custId, string betTeam)
            => $"showCombMPDetail('{refNo}', '{winlossDate}', '{transId}', '{betId}', {custId}, '{betTeam}');";

        private static string JoinSystemParlaySubbetsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {
            return IsOldSystemParlay(ticket.BetCheck)
                ? $"javascript:showCombMP('{ticket.TransId}', {ticketHelper.Index}, '{ticket.RefNo}', {ticket.custId});"
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

        private static string BuildCombinationOfferName(ITicket ticket, ITicketHelper ticketHelper)
            => IsOldSystemParlay(ticket.BetCheck)
                ? BuildCombinationOfferNameForOldSystemParlay(ticket, ticketHelper)
                : BuildCombinationOfferNameForNewSystemParlay(ticket, ticketHelper);

        private static bool IsOldSystemParlay(string betCheck) => string.IsNullOrEmpty(betCheck);

        private static string BuildCombinationOfferNameForOldSystemParlay(ITicket ticket, ITicketHelper ticketHelper)
            => string.Format(
                "<span class=\"offer-name\" id=\"combinationOfferName_{0}{1}\"> - {2}</span>",
                ticket.TransId,
                ticketHelper.Index,
                ticketHelper.GetCombinationMPOfferName(ticket.BetTeam));

        private static string BuildCombinationOfferNameForNewSystemParlay(ITicket ticket, ITicketHelper ticketHelper)
        {
            var combinationMPOfferNames = ticketHelper.GetCombinationMPOfferNameList(ticket.BetTeam, ticket.BetCheck, ticket.Handicap1, ticket.Handicap2);
            var offerNameList = string.Empty;

            if (combinationMPOfferNames != null)
            {
                foreach (var name in combinationMPOfferNames)
                {
                    offerNameList += $"<div>{name}</div>";
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

            tmpRtfHelper.RTFRenderer.AddText(GetBetTypeName(ticket, ticketHelper), tmpRtfHelper.PosFont);

            BuildRTFCombinationMPOfferNames(tmpRtfHelper, ticket, ticketHelper);

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString> { choice };
        }

        private static void BuildRTFCombinationMPOfferNames(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
        {
            if (IsOldSystemParlay(ticket.BetCheck))
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

        #endregion Build RTF Choice
    }
}