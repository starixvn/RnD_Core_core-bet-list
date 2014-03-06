// ---------------------------------------------------------------------------------
// <copyright file="Choice29.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.05.31>Add detail export excel</change>
//     <change who="Marc.Bui" date="2013.03.19>Add showSystemParlaySubbetsFunction for easy customization</change>
//     <change who="Marc.Bui" date="2013.03.11>Add ShowSystemParlayDetailJsFunction for easy customization</change>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Combination Mix Parlay
    /// </summary>
    public class Choice29 : Choice1
    {
        private const string CHOICE_BUILDER_NAMESPACE = "Fanex.BetList.Core.Builder.ChoiceBuilder";

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
                custid = ticket.CustId.ToString();
            }

            // Get user-customized js for winloss summary. The default is used if no customization is available
            string showSystemParlayDetailJsFunction = string.IsNullOrWhiteSpace(ticketHelper.ClientDetailSystemParlayFunction) ? string.Join(null, new string[] {"showCombMPDetail(&quot;",
                                            ticket.RefNo,
                                            "&quot;",
                                            ",&quot;",
                                            ticket.WinlostDate.ToString(),
                                            "&quot;",
                                            ",&quot;",
                                            ticket.TransId.ToString(),
                                            "&quot;",
                                            ",&quot;",
                                            ticket.BetId.ToString(),
                                            "&quot;",
                                            ",&quot;",
                                            custid,
                                            "&quot;);" }) : ticketHelper.ClientDetailSystemParlayFunction;

            // Get user-customized js for sub-bets view. The default is used if no customization is available
            string showSystemParlaySubbetsFunction = string.IsNullOrWhiteSpace(ticketHelper.ClientSystemParlaySubbetsDetailFunction) ? string.Join(null, new string[] {
                                                                "javascript:showCombMP(&quot;",
                                                                ticket.TransId.ToString(), 
                                                                "&quot;,&quot;", 
                                                                ticketHelper.Index.ToString(),
                                                                "&quot;,&quot;",
                                                                ticket.RefNo,
                                                                "&quot;);" 
                                                        }) : ticketHelper.ClientSystemParlaySubbetsDetailFunction;

            string detailSystemParlayDiv = string.Empty;
            if (ticketHelper.ShowSystemParlayDetail)
            {
                detailSystemParlayDiv = String.Join(
                                            null,
                                            new string[]
                                        {
                                           "<div class=\"detail\" onclick='",
                                            showSystemParlayDetailJsFunction,
                                            "'>",
                                            "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">",
                                            CoreBetList.details,
                                            "</a>",                                
                                            "</div>"                   
                                        }
                                       );
            }
            mixParlay = String.Join(
                                        null,
                                        new string[]
                                        {
                                            "<div class=\"combinationLink\"><a id=\"hidden0\" href=\"",
                                            showSystemParlaySubbetsFunction,
                                            "\">",
                                            betTypeName,
                                            "</a>",
                                            " - ",                                
                                            "<span style=\"color:#3a3a3a\">",
                                            ticketHelper.GetCombinationMPOfferName(ticket.BetTeam),
                                            "</span>",                                
                                            "</div>",                                
                                            "<div id=\"divEvent_",
                                            ticket.TransId.ToString(),
                                            ticketHelper.Index.ToString(),
                                            "\" style=\"display: none\" ></div>",                                
                                            detailSystemParlayDiv
                                        }
                                   );

            this.Template.SetValue(mixParlay);
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
        public override List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, Entities.RTFHelper rtfHelper)
        {
            List<IRichTextString> lstRTFChoices = new List<IRichTextString>();
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            tmpRtfHelper.RTFRenderer.AddText(App_GlobalResources.CoreLang.lblSystemParlay, tmpRtfHelper.PosFont);
            tmpRtfHelper.RTFRenderer.AddText(" - " + ticketHelper.GetCombinationMPOfferName(ticket.BetTeam), tmpRtfHelper.NormalFont);

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
                        catch (Exception ex)
                        {
                        }
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