namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
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

        private static Choice_League_Block BuildShowMicroGamingRnGBetsDetails(ITicket ticket)
        {
            string[] detailsFunctionCompositions =
                {
                    "showMicroGamingRnGBetsDetails(&quot;",
                    ticket.TransId.ToString(),
                    "&quot;",
                    ",&quot;",
                        ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                    "&quot;",
                    ",&quot;",
                    ticket.CustId.ToString(),
                        "&quot;",
                    ",&quot;",
                    GetTransactionType(ticket),
                    "&quot;);"
                };

            string detailsFunction = string.Join(null, detailsFunctionCompositions);

            string[] detailsFunctionDivCompositions =
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

        private static string BuildGameName(ITicket ticket, ITicketHelper ticketHelper)
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

        private static string GetTransactionType(ITicket ticket)
        {
            ////@TransType
            ////+ 0 : All (DRAW/WON/LOSE/Reject/Void/Refund/Running/Waiting)
            ////+ 1 : DRAW/WON/LOSE/Refund
            ////+ 2 : Reject/Void
            ////+ 3 : Running/Waiting
            //// Use this because web server vs db is same GMT
            if (ticket.WinlostDate.Date >= DateTime.Today.AddDays(-1))
            {
                switch (ticket.Status.ToLowerInvariant())
                {
                    case BetStatus.Won:
                    case BetStatus.Lose:
                    case BetStatus.Draw:
                    case BetStatus.Refund:
                        return "1";

                    case BetStatus.Reject:
                    case BetStatus.Void:
                        return "2";

                    case BetStatus.Running:
                    case BetStatus.Waiting:
                        return "3";

                    default:
                        return "0";
                }
            }

            return "0"; ////Yesterday Ticket
        }
    }
}