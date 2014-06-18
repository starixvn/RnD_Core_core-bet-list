namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using NPOI.SS.UserModel;

    /// <summary>
    /// P2P Games.
    /// </summary>
    public class Choice1000 : Choice1
    {
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
            string content = string.Join(null, new string[] { "<div><span class='", BLFAVORITE, "'>", CoreLang.p2pGame, "</span></div>" });
            this.Template.SetValue(content);

            return this.Template;
        }

        protected override IRichTextString BuildRTFChoice(ITicket ticket, RTFHelper rtfHelper, ITicketHelper ticketHelper)
        {
            rtfHelper.RTFRenderer.AddText(CoreLang.p2pGame, rtfHelper.PosFont);

            var choiceRtf = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return choiceRtf;
        }
    }
}