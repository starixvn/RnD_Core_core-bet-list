namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Choice29 : Choice1
    {
        private const string CHOICEBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.ChoiceBuilder";
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

        private string GetCustId(ITicket ticket)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request["custid"] != null)
            {
                return HttpContext.Current.Request["custid"];
            }
            else
            {
                return ticket.CustId.ToString(CultureInfo.InvariantCulture);
            }
        }

        private string BuildShowSystemParlaySubbetsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {
            return string.IsNullOrWhiteSpace(ticketHelper.ClientSystemParlaySubbetsDetailFunction)
                        ? JoinSystemParlaySubbetsFunc(ticket, ticketHelper)
                        : ticketHelper.ClientSystemParlaySubbetsDetailFunction;
        }

        private string BuildSystemParlayDetailDiv(ITicket ticket, ITicketHelper ticketHelper)
        {
            return ticketHelper.ShowSystemParlayDetail
                            ? JoinDetailSystemParlayDiv(ticket, ticketHelper)
                            : string.Empty;
        }

        private string JoinDetailSystemParlayDiv(ITicket ticket, ITicketHelper ticketHelper)
        {
            var systemParlayDetailJsFunction = BuildSystemParlayDetailJsFunction(ticket, ticketHelper);

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";

            return $"<div class=\"detail\" onclick=\"{systemParlayDetailJsFunction}\">{detailLink}</div>";
        }

        private string BuildSystemParlayDetailJsFunction(ITicket ticket, ITicketHelper ticketHelper)
        {
            string custid = GetCustId(ticket);

            return string.IsNullOrWhiteSpace(ticketHelper.ClientDetailSystemParlayFunction)
                        ? JoinSystemParlayDetailJsFunc(
                                                            ticket.RefNo,
                                                            ticket.WinlostDate.ToString(),
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
        private string JoinSystemParlayDetailJsFunc(string refNo, string winlossDate, string transId, string betId, string custId)
        {
            return $"showCombMPDetail('{refNo}', '{winlossDate}', '{transId}', '{betId}', '{custId}');";
        }

        private string JoinSystemParlaySubbetsFunc(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transId = ticket.TransId.ToString(CultureInfo.InvariantCulture);
            var transIndex = ticketHelper.Index.ToString(CultureInfo.InvariantCulture);
            var refNo = ticket.RefNo;

            if (IsOldSystemParlay(ticket.BetCheck))
            {
                return $"javascript:showCombMP('{transId}', '{transIndex}', '{refNo}');";
            }
            else
            {
                return $"javascript:showNewCombMP('{transId}', '{transIndex}', '{refNo}', '{ticket.Handicap1}', '{ticket.Handicap2}', '{ticket.BetCheck}', '{ticket.BetTeam}');";
            }
        }

        private string BuildCombinationOfferName(ITicket ticket, ITicketHelper ticketHelper)
        {
            return IsOldSystemParlay(ticket.BetCheck)
                ? BuildCombinationOfferNameForOldSystemParlay(ticket, ticketHelper)
                : BuildCombinationOfferNameForNewSystemParlay(ticket, ticketHelper);
        }

        private bool IsOldSystemParlay(string betCheck)
        {
            return string.IsNullOrEmpty(betCheck);
        }

        private string BuildCombinationOfferNameForOldSystemParlay(ITicket ticket, ITicketHelper ticketHelper)
        {
            return $"<span class=\"offer-name\" id=\"combinationOfferName_{ticket.TransId}{ticketHelper.Index}\"> - {ticketHelper.GetCombinationMPOfferName(ticket.BetTeam)}</span>";
        }

        private string BuildCombinationOfferNameForNewSystemParlay(ITicket ticket, ITicketHelper ticketHelper)
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

            return $"<div class=\"offer-name-list\" id=\"combinationOfferName_{ticket.TransId}{ticketHelper.Index}\">{offerNameList}</div>";
        }

        /// <summary>
        /// Joins the mix parlay markups.
        /// </summary>
        /// <param name="showSystemParlaySubbetsFunction">The show system parlay sub bets function.</param>
        /// <param name="betTypeName">Name of the bet type.</param>
        /// <param name="combinationMPOfferName">Offer name of combination mix parlay.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="transIndex">Index of the trans.</param>
        /// <param name="systemParlayDetailDiv">The detail system parlay div.</param>
        /// <returns>System.String: mix parlay markups.</returns>
        private string BuildMixParlayMarkups(
                        string showSystemParlaySubbetsFunction,
                        string betTypeName,
                        string combinationOfferName,
                        string transId,
                        string transIndex,
                        string systemParlayDetailDiv)
        {
            var combinationLink = $"<a id=\"hidden0\" href=\"{showSystemParlaySubbetsFunction}\">{betTypeName}</a>";

            return $"<div class=\"combinationLink\">{combinationLink}{combinationOfferName}</div><div id=\"divEvent_{transId}{transIndex}\" style=\"display: none\" ></div>{systemParlayDetailDiv}";
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
            List<IRichTextString> lstRTFChoices = new List<IRichTextString>();
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            tmpRtfHelper.RTFRenderer.AddText(GetBetTypeName(ticket, ticketHelper), tmpRtfHelper.PosFont);

            BuildRTFCombinationMPOfferNames(tmpRtfHelper, ticket, ticketHelper);

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

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString>() { choice };
        }

        private void BuildRTFCombinationMPOfferNames(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
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

        private void BuildRTFCombinationMPOfferNamesForOldSystemParlay(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
        {
            tmpRtfHelper.RTFRenderer.AddText($" - {ticketHelper.GetCombinationMPOfferName(ticket.BetTeam)}");
        }

        private void BuildRTFCombinationMPOfferNamesForNewSystemParlay(RTFHelper tmpRtfHelper, ITicket ticket, ITicketHelper ticketHelper)
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

        /// <summary>
        /// Collects the RTF sub choice (detail choices of combination mix parlay).
        /// </summary>
        /// <param name="rtfChoiceTexts">The RTF choice texts.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <param name="posFont">The position font.</param>
        /// <param name="normalFont">The normal font.</param>
        private void CollectRTFSubChoice(List<IRichTextString> rtfChoiceTexts, ref RTFHelper rtfHelper, IFont posFont, IFont normalFont)
        {
            foreach (HSSFRichTextString rtfString in rtfChoiceTexts)
            {
                // Remove extra line-breaks
                while (rtfString.String.StartsWith("\n"))
                {
                    rtfString.String.Remove(0, 1);
                }

                int lineBreakIndex = rtfString.String.IndexOf("\n");
                string eventName = lineBreakIndex >= 0
                                    ? rtfString.String.Substring(0, lineBreakIndex)
                                    : string.Empty;
                string theRest = rtfString.String.Remove(0, eventName.Length);

                rtfHelper.RTFRenderer.AddText("\n" + eventName, posFont);
                rtfHelper.RTFRenderer.AddText(theRest, normalFont);
            }
        }

        #endregion Build RTF Choice
    }
}