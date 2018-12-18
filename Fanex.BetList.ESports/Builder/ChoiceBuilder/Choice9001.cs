namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Resources;
    using Utils.StringExt;

    /// <summary>
    /// E-Sports 2.0: Map X Moneyline.
    /// </summary>
    public class Choice9001 : Choice20
    {
        private const string ESportXmlTag = "p16";
        private const string InPlayXmlAttribute = "in";

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
        }

        protected override void BuildScore(ITicket ticket)
        {
            base.BuildScore(ticket);

            Template.Score.SetValue($"<span class=\"{Favorite}\"> [{GetScoreText(ticket.ExtraInfo)}]</span>");
        }

        internal static string GetScoreText(string extraInfo)
        {
            var scoreText = CoreBetList.Live;

            if (extraInfo.HasAttributeInXmlTag(ESportXmlTag, InPlayXmlAttribute))
            {
                var isInPlay = extraInfo.GetXmlTagAttributeValue(ESportXmlTag, InPlayXmlAttribute) == "1";

                if (isInPlay)
                {
                    scoreText = ESportsResources.InPlay;
                }
            }

            return scoreText;
        }
    }
}