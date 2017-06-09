namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Choice29 : Choice1
    {
        private const string RefId = "ComboParlay";
        private const string ResourceId = "LuckyBet";

        #region Build Choice

        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
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
                return ticketHelper.GetBetTypeNameById(9);
            }

            var bettypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            if (ticket.BetCheck == ticket.BetTeam && ticket.Handicap1 == 0)
            {
                bettypeName += " " + ticketHelper.GetResourceData(RefId, ResourceId);
            }

            return bettypeName;
        }

        private static string GetCustId(ITicket ticket)
        {
            return (HttpContext.Current != null
                && HttpContext.Current.Request != null
                && HttpContext.Current.Request["custid"] != null)
                    ? HttpContext.Current.Request["custid"]
                    : ticket.CustId.ToString(CultureInfo.InvariantCulture);
        }

        private static string BuildShowSystemParlaySubbetsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {
            return string.IsNullOrWhiteSpace(ticketHelper.ClientSystemParlaySubbetsDetailFunction)
                        ? JoinSystemParlaySubbetsFunc(ticket, ticketHelper)
                        : ticketHelper.ClientSystemParlaySubbetsDetailFunction;
        }

        private static string BuildSystemParlayDetailDiv(ITicket ticket, ITicketHelper ticketHelper)
        {
            return ticketHelper.ShowSystemParlayDetail
                        && ticket.BetTeam != "1"
                        && ticket.Status.ToLowerInvariant() != BetStatus.Void
                    ? JoinDetailSystemParlayDiv(ticket, ticketHelper)
                    : string.Empty;
        }

        private static string JoinDetailSystemParlayDiv(ITicket ticket, ITicketHelper ticketHelper)
        {
            var systemParlayDetailJsFunction = BuildSystemParlayDetailJsFunction(ticket, ticketHelper);

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";

            return $"<div class=\"detail\" onclick=\"{systemParlayDetailJsFunction}\">{detailLink}</div>";
        }

        private static string BuildSystemParlayDetailJsFunction(ITicket ticket, ITicketHelper ticketHelper)
        {
            string custid = GetCustId(ticket);

            return string.IsNullOrWhiteSpace(ticketHelper.ClientDetailSystemParlayFunction)
                        ? JoinSystemParlayDetailJsFunc(
                            ticket.RefNo,
                            ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                            ticket.TransId.ToString(CultureInfo.InvariantCulture),
                            ticket.BetId.ToString(),
                            custid)
                        : ticketHelper.ClientDetailSystemParlayFunction;
        }

        /// <summary>
        /// Joins the system parlay detail JavaScripts function.
        /// </summary>
        /// <param name="refNo">The reference no.</param>
        /// <param name="winlossDate">The win loss date.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="betId">The bet identifier.</param>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>System.String: the markups.</returns>
        private static string JoinSystemParlayDetailJsFunc(string refNo, string winlossDate, string transId, string betId, string custId)
            => $"showCombMPDetail('{refNo}', '{winlossDate}', '{transId}', '{betId}', '{custId}');";

        private static string JoinSystemParlaySubbetsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transId = ticket.TransId.ToString(CultureInfo.InvariantCulture);
            var transIndex = ticketHelper.Index.ToString(CultureInfo.InvariantCulture);
            var refNo = ticket.RefNo;

            return IsOldSystemParlay(ticket.BetCheck)
                ? $"javascript:showCombMP('{transId}', '{transIndex}', '{refNo}');"
                : string.Format(
                    "javascript:showNewCombMP('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');",
                    transId,
                    transIndex,
                    refNo,
                    ticket.Handicap1,
                    ticket.Handicap2,
                    ticket.BetCheck,
                    ticket.BetTeam);
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

        /// <summary>
        /// Joins the mix parlay markups.
        /// </summary>
        /// <param name="showSystemParlaySubbetsFunction">The show system parlay sub bets function.</param>
        /// <param name="betTypeName">Name of the bet type.</param>
        /// <param name="combinationOfferName">Offer name of combination mix parlay.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="transIndex">Index of the trans.</param>
        /// <param name="systemParlayDetailDiv">The detail system parlay div.</param>
        /// <returns>System.String: mix parlay markups.</returns>
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

        /// <summary>
        /// Renders the RTF.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>List of IRichTextString for input data.</returns>
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

        /***************************Export excel for subtickets*****************************
           if (ticketHelper.GetParlayDetailById != null)
           {
               IList<ITicket> parlayData = ticketHelper.GetParlayDetailById(ticket.TransId);

               bool showOdds = ticketHelper.IsShowOddsWithChoice;
               ticketHelper.IsShowOddsWithChoice = true;
               IFont pos, normal;
               var betListBaseBuilder = new BetListBaseBuilder();

               foreach (ITicket tkt in parlayData)
               {
                   IChoice choiceBuilder = betListBaseBuilder.CreateChoiceBuilder(tkt.BetTypeId);
                   Template = choiceBuilder.Render(tkt, ticketHelper, null, isShowScoreMap);
                   var choiceData = BuildRTFChoice(tkt, rtfHelper, ticketHelper);
                   List<IRichTextString> tmpRtfChoice;

                   if (choiceData is List<IRichTextString>)
                   {
                       tmpRtfChoice = (List<IRichTextString>)choiceData;
                   }
                   else
                   {
                       tmpRtfChoice = new List<IRichTextString>() { (IRichTextString)choiceData };
                   }

                   if ((tkt.StatusId?.ToString().Trim() == BetStatusId.Void) || (tkt.Status?.Trim().ToLower() == BetStatus.Void))
                   {
                       pos = tmpRtfHelper.PosFontCrossed;
                       normal = tmpRtfHelper.NormalFontCrossed;
                   }
                   else
                   {
                       pos = tmpRtfHelper.PosFont;
                       normal = tmpRtfHelper.NormalFont;
                   }

                   CollectRTFSubChoice(tmpRtfChoice, ref tmpRtfHelper, pos, normal);
               }

               ticketHelper.IsShowOddsWithChoice = showOdds;
           }
           *******************************************************************************/

        #endregion Build RTF Choice
    }
}