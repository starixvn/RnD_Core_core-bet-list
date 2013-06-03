// ---------------------------------------------------------------------------------
// <copyright file="Choice9.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.05.31>Add detail export excel</change>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using System;

    /// <summary>
    /// Mix Parlay
    /// </summary>
    public class Choice9 : Choice1
    {
        private const string CHOICE_BUILDER_NAMESPACE = "Fanex.BetList.Core.Builder.ChoiceBuilder";

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">if set to <c>true</c> [is show score map].</param>
        /// <returns>Choice_Template.</returns>
        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            string betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            string clientMixParlayDetailFunction = string.IsNullOrWhiteSpace(ticketHelper.ClientMixParlaySubbetsDetailFunction) ? string.Join(
                                                            null,
                                                            new string[] { 
                                                                "javascript:showMP(&quot;",
                                                                ticket.TransId.ToString(),
                                                                "&quot;,&quot;",
                                                                ticket.RefNo,
                                                                "&quot;);"    
                                                            }
                                                          ) : ticketHelper.ClientMixParlaySubbetsDetailFunction;

            string mixParlay = string.Join(
                                                null,
                                                new string[] 
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
                                                }
                                          );

            this.Template.SetValue(mixParlay);

            return this.Template;
        }

        /// <summary>
        /// Renders the RTF.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">if set to <c>true</c> [is show score map].</param>
        /// <param name="rtfHelper"></param>
        /// <returns>
        /// Choice_Template.
        /// </returns>
        public override List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, RTFHelper rtfHelper)
        {
            List<IRichTextString> lstRTFChoices = new List<IRichTextString>();
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            tmpRtfHelper.RTFRenderer.AddText(App_GlobalResources.CoreLang.lblMixParlay, tmpRtfHelper.PosFont);

            if (ticketHelper.GetParlayDetailById != null)
            {
                IList<ITicket> parlayData = ticketHelper.GetParlayDetailById(ticket.TransId);

                bool showOdds = ticketHelper.IsShowOddsWithChoice;
                ticketHelper.IsShowOddsWithChoice = true;

                IFont pos, normal;

                foreach (ITicket tkt in parlayData)
                {
                    IChoice choiceBuilder = this.CreateChoiceBuilder(tkt.BetTypeId);
                    this.Template = choiceBuilder.Render(tkt, ticketHelper, null, isShowScoreMap);

                    var choiceData = this.BuildRTFChoice(tkt, rtfHelper, ticketHelper);

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

                    foreach (HSSFRichTextString rtfString in tmpRtfChoice)
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

                            tmpRtfHelper.RTFRenderer.AddText("\n" + eventName, pos);
                            tmpRtfHelper.RTFRenderer.AddText(theRest, normal);
                        }
                        catch (Exception ex) { }
                    }
                }

                ticketHelper.IsShowOddsWithChoice = showOdds;
            }

            var choice = tmpRtfHelper.RTFRenderer.Render();

            tmpRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString>() { choice };
        }

        /// <summary>
        /// Creates the choice builder.
        /// </summary>
        /// <param name="bettype">The bettype.</param>
        /// <returns>IChoice.</returns>
        private IChoice CreateChoiceBuilder(int bettype)
        {
            string typeName = string.Join(
                                            null,
                                            new string[] 
                                            { 
                                                CHOICE_BUILDER_NAMESPACE, 
                                                ".Choice", 
                                                bettype.ToString() 
                                            }
                                         );

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