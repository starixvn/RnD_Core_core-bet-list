namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;
    using App_GlobalResources;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Combination Mix Parlay.
    /// </summary>
    public class Choice29 : Choice1
    {
        private const string CHOICEBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.ChoiceBuilder";

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
            tmpRtfHelper.RTFRenderer.AddText(CoreBetList.lblSystemParlay, tmpRtfHelper.PosFont);
            tmpRtfHelper.RTFRenderer.AddText(" - " + ticketHelper.GetCombinationMPOfferName(ticket.BetTeam), tmpRtfHelper.NormalFont);

            if (ticketHelper.GetParlayDetailById != null)
            {
                IList<ITicket> parlayData = ticketHelper.GetParlayDetailById(ticket.TransId);
                bool showOdds = ticketHelper.IsShowOddsWithChoice;
                ticketHelper.IsShowOddsWithChoice = true;
                IFont pos, normal;

                foreach (ITicket tkt in parlayData)
                {
                    IChoice choiceBuilder = CreateChoiceBuilder(tkt.BetTypeId);
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

                    if ((tkt.StatusId != null && tkt.StatusId.ToString().Trim() == BetStatusId.Void) || (tkt.Status != null && tkt.Status.Trim().ToLower() == BetStatus.Void))
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

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();
            return new List<IRichTextString>() { choice };
        }

        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            string mixParlay = string.Empty, custid = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request["custid"] != null)
            {
                custid = HttpContext.Current.Request["custid"];
            }
            else
            {
                custid = ticket.CustId.ToString(CultureInfo.InvariantCulture);
            }

            // Get user-customized js for winloss summary. The default is used if no customization is available
            string showSystemParlayDetailJsFunction = ticketHelper.ClientDetailSystemParlayFunction;
            if (string.IsNullOrWhiteSpace(showSystemParlayDetailJsFunction))
            {
                showSystemParlayDetailJsFunction = JoinSystemParlayDetailJsFunction(
                                                            ticket.RefNo,
                                                            ticket.WinlostDate.ToString(),
                                                            ticket.TransId.ToString(CultureInfo.InvariantCulture),
                                                            ticket.BetId.ToString(),
                                                            custid);
            }

            // Get user-customized js for sub-bets view. The default is used if no customization is available
            string showSystemParlaySubbetsFunction = ticketHelper.ClientSystemParlaySubbetsDetailFunction;
            if (string.IsNullOrWhiteSpace(showSystemParlaySubbetsFunction))
            {
                showSystemParlaySubbetsFunction =
                    JoinSystemParlaySubbetsFunction(ticket.TransId.ToString(CultureInfo.InvariantCulture), ticketHelper.Index.ToString(CultureInfo.InvariantCulture), ticket.RefNo);
            }

            string detailSystemParlayDiv = string.Empty;
            if (ticketHelper.ShowSystemParlayDetail)
            {
                detailSystemParlayDiv = JoinDetailSystemParlayDiv(showSystemParlayDetailJsFunction);
            }

            string combinationMPOfferName = ticketHelper.GetCombinationMPOfferName(ticket.BetTeam);

            mixParlay = JoinMixParlayMarkups(
                                        showSystemParlaySubbetsFunction,
                                        betTypeName,
                                        combinationMPOfferName,
                                        ticket.TransId.ToString(CultureInfo.InvariantCulture),
                                        ticketHelper.Index.ToString(CultureInfo.InvariantCulture),
                                        detailSystemParlayDiv);

            Template.SetValue(mixParlay);
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
        private static string JoinSystemParlayDetailJsFunction(string refNo, string winlossDate, string transId, string betId, string custId)
        {
            string[] markupCompositions = new string[]
                                            {
                                                "showCombMPDetail(&quot;",
                                                refNo,
                                                "&quot;",
                                                ",&quot;",
                                                winlossDate,
                                                "&quot;",
                                                ",&quot;",
                                                transId,
                                                "&quot;",
                                                ",&quot;",
                                                betId,
                                                "&quot;",
                                                ",&quot;",
                                                custId,
                                                "&quot;);"
                                            };

            string markups = string.Join(null, markupCompositions);

            return markups;
        }

        private static string JoinSystemParlaySubbetsFunction(string transId, string transIndex, string refNo)
        {
            string markups = string.Join(null, new string[] { "javascript:showCombMP(&quot;", transId, "&quot;,&quot;", transIndex, "&quot;,&quot;", refNo, "&quot;);" });

            return markups;
        }

        /// <summary>
        /// Creates the choice builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IChoice: concrete Choice builder basing on input bet type id.</returns>
        private IChoice CreateChoiceBuilder(int bettype)
        {
            string typeName = string.Join(null, new string[] { CHOICEBUILDERNAMESPACE, ".Choice", bettype.ToString() });

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                return new Choice1();
            }
            else
            {
                return Activator.CreateInstance(type) as IChoice;
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

                try
                {
                    int lineBreakIndex = rtfString.String.IndexOf("\n");
                    string eventName = rtfString.String.Substring(0, lineBreakIndex);
                    string theRest = rtfString.String.Remove(0, eventName.Length);

                    rtfHelper.RTFRenderer.AddText("\n" + eventName, posFont);
                    rtfHelper.RTFRenderer.AddText(theRest, normalFont);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Joins the mix parlay markups.
        /// </summary>
        /// <param name="showSystemParlaySubbetsFunction">The show system parlay sub bets function.</param>
        /// <param name="betTypeName">Name of the bet type.</param>
        /// <param name="combinationMPOfferName">Offer name of combination mix parlay.</param>
        /// <param name="transId">The trans identifier.</param>
        /// <param name="transIndex">Index of the trans.</param>
        /// <param name="detailSystemParlayDiv">The detail system parlay div.</param>
        /// <returns>System.String: mix parlay markups.</returns>
        private string JoinMixParlayMarkups(
                        string showSystemParlaySubbetsFunction,
                        string betTypeName,
                        string combinationMPOfferName,
                        string transId,
                        string transIndex,
                        string detailSystemParlayDiv)
        {
            string combinationOpenDiv = "<div class=\"combinationLink\"><a id=\"hidden0\" href=\"";
            string[] mixParlayCompositions = new string[]
                                                {
                                                    combinationOpenDiv, showSystemParlaySubbetsFunction,
                                                    "\">",
                                                    betTypeName,
                                                    "</a>",
                                                    " - ",
                                                    "<span style=\"color:#3a3a3a\">",
                                                    combinationMPOfferName,
                                                    "</span>",
                                                    "</div>",
                                                    "<div id=\"divEvent_",
                                                    transId,
                                                    transIndex,
                                                    "\" style=\"display: none\" ></div>",
                                                    detailSystemParlayDiv
                                                };

            string mixParlay = string.Join(null, mixParlayCompositions);

            return mixParlay;
        }

        /// <summary>
        /// Joins the detail system parlay div tag.
        /// </summary>
        /// <param name="systemParlayDetailJsFunction">The system parlay detail JavaScripts function.</param>
        /// <returns>System.String: the markups.</returns>
        private string JoinDetailSystemParlayDiv(string systemParlayDetailJsFunction)
        {
            string[] markupCompositions = new string[]
                                            {
                                                "<div class=\"detail\" onclick='",
                                                systemParlayDetailJsFunction,
                                                "'>",
                                                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">",
                                                CoreBetList.details,
                                                "</a>",
                                                "</div>"
                                            };

            string markups = string.Join(null, markupCompositions);

            return markups;
        }
    }
}