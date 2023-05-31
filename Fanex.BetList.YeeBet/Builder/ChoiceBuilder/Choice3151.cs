namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.Parsers;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using App_GlobalResources;
    using Entities;

    public class Choice3151 : Choice1
    {

        private const string BetTypeNameTag = "<span class='bet-type-name'>{0}</span>";

        private const string BetIdTag = "<span class='bet-id'>{0}</span>";

        private const string GameIdTag = "<span class='game-id'>{0}</span>";

        private const string TableIdFormat = "<span class=\"gamegroupname\">{0}</span>";

        private const string TableCodeKey = "tableCode";

        private const string ChoiceKey = "choice";

        public const string RefResourceId = "YB_BT";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData){
            Template.betTeam = string.Empty;
            if(IsDetailTicket(ticket.TransDesc)){
                var transDescOptions = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
                if(transDescOptions != null && transDescOptions.ContainsKey(ChoiceKey)){
                    Template.betTeam = ticketHelper.GetResourceData(RefResourceId, transDescOptions[ChoiceKey]);
                }
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = BuildBetTypeName(ticket, ticketHelper);

            if (string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.League.Append(BuildDetailLink(ticket));
            }
            else
            {
                Template.League.LeagueName.leagueName += " " + GetTableCode(ticket) + " " + BuildGameId(ticket);
            }
        }

        protected static string BuildBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

            return string.Format(BetTypeNameTag, betTypeName);
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (IsDetailTicket(ticket.TransDesc))
            {
                Template.BetType.betTypeName = string.Format(BetIdTag, $"{CoreBetList.BetID}: {ticket.BetCheck}");
            }
        }

        protected static Choice_League_Block BuildDetailLink(ITicket ticket)
        {
            var detailLinkMarkupBuilder = new StringBuilder();
            var detailFunction = BuildDetailFunction(ticket);

            detailLinkMarkupBuilder
                .Append("<div class=\"detail yeebet-casino-detail\" onclick='")
                .Append(detailFunction)
                .Append("'>")
                .Append("<a class=\"detail-link\" href=\"javascript:void(&quot;&quot;);\">")
                .Append(CoreBetList.details)
                .Append("</a>")
                .Append("</div>");

            var detailTemplateBlock = new Choice_League_Block();

            detailTemplateBlock.SetValue(detailLinkMarkupBuilder.ToString());

            return detailTemplateBlock;
        }

        private static string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showYeeBetBetDetails(&quot;")
                .Append(ticket.TransId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.WinlostDate.ToString(CultureInfo.InvariantCulture))
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.CustId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.GetTransactionType())
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }


        private static string GetTableCode(ITicket ticket)
        {
            var transDescOptions = ExtraInfoParser.ParseExtraInfo(ticket.TransDesc);
            if (transDescOptions != null && transDescOptions.ContainsKey(TableCodeKey))
            {
                return string.Format(TableIdFormat, transDescOptions[TableCodeKey]);
            }

            return string.Empty;
        }

        private static string BuildGameId(ITicket ticket)
            => string.Format(GameIdTag, ticket.MatchCode);

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());
            Template.League.LeagueName.leagueName = string.Join(string.Empty, Template.League.LeagueName.leagueName.StripHTML());
            Template.BetType.betTypeName = string.Join(string.Empty, Template.BetType.betTypeName.StripHTML());
        }

        private static bool IsDetailTicket(string transDesc)
                => !string.IsNullOrEmpty(transDesc);
    }
}