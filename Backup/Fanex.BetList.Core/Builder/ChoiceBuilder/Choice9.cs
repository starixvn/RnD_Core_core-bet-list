namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Templates;

    /// <summary>
    /// Mix Parlay.
    /// </summary>
    public class Choice9 : Choice1
    {
        private const string CHOICEBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.ChoiceBuilder";

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        /// <returns>Choice_Template object.</returns>
        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            string betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            string clientMixParlayDetailFunction;
            string[] components;

            if (string.IsNullOrWhiteSpace(ticketHelper.ClientMixParlaySubbetsDetailFunction))
            {
                components = new string[] { "javascript:showMP(&quot;", ticket.TransId.ToString(), "&quot;,&quot;", ticket.RefNo, "&quot;);" };
                clientMixParlayDetailFunction = string.Join(null, components);
            }
            else
            {
                clientMixParlayDetailFunction = ticketHelper.ClientMixParlaySubbetsDetailFunction;
            }

            components = new string[]
                            {
                                "<a id=\"hidden0\" href=\"",
                                clientMixParlayDetailFunction,
                                "\">",
                                betTypeName,
                                "</a>",
                                "<br/><br/>",
                                "<div id=\"divEvent_",
                                ticket.TransId.ToString(),
                                "\" style=\"display: none\" ></div>"
                            };

            string mixParlay = string.Join(null, components);

            Template.SetValue(mixParlay);

            return Template;
        }

        /// <summary>
        /// Renders the RTF.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        /// <param name="rtfHelper">The rich text rendering helper.</param>
        /// <returns>
        /// Choice_Template object.
        /// </returns>
        public override List<IRichTextString> RenderRTF(
                                                ITicket ticket,
                                                ITicketHelper ticketHelper,
                                                List<ITicketData> ticketData,
                                                bool isShowScoreMap,
                                                RTFHelper rtfHelper)
        {
            List<IRichTextString> lstRTFChoices = new List<IRichTextString>();
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            tmpRtfHelper.RTFRenderer.AddText(CoreBetList.lblMixParlay, tmpRtfHelper.PosFont);

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

                    CombineSubRichTextString(tmpRtfChoice, ref tmpRtfHelper, pos, normal);
                }

                ticketHelper.IsShowOddsWithChoice = showOdds;
            }

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();
            return new List<IRichTextString>() { choice };
        }

        /// <summary>
        /// Combines the sub rich text string.
        /// </summary>
        /// <param name="tmpRtfChoice">The temporary RTF choice.</param>
        /// <param name="tmpRtfHelper">The temporary RTF helper.</param>
        /// <param name="posFont">The position font.</param>
        /// <param name="normalFont">The normal font.</param>
        private static void CombineSubRichTextString(List<IRichTextString> tmpRtfChoice, ref RTFHelper tmpRtfHelper, IFont posFont, IFont normalFont)
        {
            foreach (HSSFRichTextString rtfString in tmpRtfChoice)
            {
                // Remove extra line-breaks
                while (rtfString.String.StartsWith("\n", StringComparison.OrdinalIgnoreCase))
                {
                    rtfString.String.Remove(0, 1);
                }

                try
                {
                    int lineBreakIndex = rtfString.String.IndexOf("\n");
                    string eventName = rtfString.String.Substring(0, lineBreakIndex);
                    string theRest = rtfString.String.Remove(0, eventName.Length);
                    tmpRtfHelper.RTFRenderer.AddText("\n" + eventName, posFont);
                    tmpRtfHelper.RTFRenderer.AddText(theRest, normalFont);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Creates the choice builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IChoice object.</returns>
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
    }
}