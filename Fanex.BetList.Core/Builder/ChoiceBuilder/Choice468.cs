namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketHelperExt;

    /// <summary>
    /// PHAIO 4A: Score Box Handicap
    /// </summary>
    public class Choice468 : Choice1
    {
        public const string ScoreBoxDescriptionHtmlFormat = "<div class='bettype-description'>{0}</div>";

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetType(ticket, ticketHelper, ticketData);

            if (!string.IsNullOrEmpty(ticket.BetTeam) && !string.IsNullOrEmpty(ticket.BetCheck))
            {
                var scoreBoxOddsDescriptions = ticketHelper.GetScoreBoxOdds(ticket.BetTeam, ticket.BetCheck);

                foreach (var scoreBoxOddsDescription in scoreBoxOddsDescriptions)
                {
                    Template.BetType.Append(string.Format(ScoreBoxDescriptionHtmlFormat, scoreBoxOddsDescription));
                }
            }
        }

        protected override void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var betTypeInfoRows = Template.BetType.ToString().SplitHTMLIntoRows();

            foreach (var row in betTypeInfoRows)
            {
                rtfHelper.RTFRenderer.AddText("\n" + row);
            }
        }
    }
}