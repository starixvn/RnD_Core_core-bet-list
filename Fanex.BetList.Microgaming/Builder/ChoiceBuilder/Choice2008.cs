namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Choice2008 : Choice2001
    {
        private const string OpenGameNameTag = "<span class='micro-gaming-game-name'>";
        private const string CloseGameNameTag = "</span>";

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildLeague(ticket, ticketHelper);

            if (!string.IsNullOrWhiteSpace(ticket.TransDesc))
            {
                Template.League.LeagueName.leagueName += BuildGameName(ticket, ticketHelper);
            }
            else
            {
                Template.League.Append(BuildShowMicroGamingRnGBetsDetails(ticket));
            }
        }

        private Choice_League_Block BuildShowMicroGamingRnGBetsDetails(ITicket ticket)
        {
            string[] detailsFunctionCompositions = new string[]
                                            {
                                                "showMicroGamingRnGBetsDetails(&quot;",
                                                ticket.TransId.ToString(),
                                                "&quot;",
                                                ",&quot;",
                                                ticket.WinlostDate.ToString(),
                                                "&quot;",
                                                ",&quot;",
                                                ticket.CustId.ToString(),
                                                "&quot;);"
                                            };

            string detailsFunction = string.Join(null, detailsFunctionCompositions);

            string[] detailsFunctionDivCompositions = new string[]
                                            {
                                                "<div class=\"detail\" onclick='",
                                                detailsFunction,
                                                "'>",
                                                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">",
                                                CoreBetList.details,
                                                "</a>",
                                                "</div>"
                                            };

            string markups = string.Join(null, detailsFunctionDivCompositions);

            var detailtTemplateBlock = new Choice_League_Block();

            detailtTemplateBlock.SetValue(markups);

            return detailtTemplateBlock;
        }

        private string BuildGameName(ITicket ticket, ITicketHelper ticketHelper)
        {
            string gameName = ticketHelper.GetResourceData("MG_Casino", ticket.TransDesc);
            string markups = string.Format("{0} {1}{2}", OpenGameNameTag, gameName, CloseGameNameTag);

            return markups;
        }

        protected override void AdjustBetTeamToRTF()
        {
            base.AdjustBetTeamToRTF();

            Template.League.LeagueName.leagueName = Template.League.LeagueName.leagueName
                .Replace(OpenGameNameTag, string.Empty)
                .Replace(CloseGameNameTag, string.Empty);
        }
    }
}