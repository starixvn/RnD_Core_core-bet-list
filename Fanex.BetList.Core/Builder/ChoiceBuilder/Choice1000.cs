namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    /// <summary>
    /// P2P Games.
    /// </summary>
    public class Choice1000 : Choice1
    {
        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            var content = string.Join(null, new string[] { "<div><span class='", Favorite, "'>", CoreLang.p2pGame, "</span></div>" });
            Template.SetValue(content);

            return Template;
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