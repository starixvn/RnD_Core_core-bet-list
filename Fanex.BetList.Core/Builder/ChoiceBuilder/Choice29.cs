// ---------------------------------------------------------------------------------
// <copyright file="Choice29.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2013.03.11>Add ShowSystemParlayDetailJsFunction
//                                             for easy customization</change>
//     <change who="Marc.Bui" date="2013.03.19>Add showSystemParlaySubbetsFunction
//                                             for easy customization</change>
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
            if (HttpContext.Current.Request["custid"] != null)
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

        public override List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, Entities.RTFHelper rtfHelper)
        {
            List<IRichTextString> lstRTFChoices = new List<IRichTextString>(1);
            IRichTextString rtfChoice = new HSSFRichTextString(App_GlobalResources.CoreLang.lblSystemParlay);
            lstRTFChoices.Add(rtfChoice);
            return lstRTFChoices;
        }
    }
}