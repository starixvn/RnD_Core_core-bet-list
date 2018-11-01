namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Entities;
    using Core.Resources;

    /// <summary>
    /// Happy 5: Big/Small
    /// </summary>
    public class Choice8101 : Choice1
    {
        public const string BetChoiceRefId = "H5_BetChoice";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (!string.IsNullOrEmpty(ticket.BetTeam))
            {
                var resourceId = ticket.BetTeam.ToLowerInvariant();

                Template.betTeam = ticketHelper.GetResourceData(BetChoiceRefId, resourceId);
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            var matchCode = string.Format(NumberGameLang.Number, ticket.MatchCode);

            Template.Match.SetValue($"<div class=\"match\">{matchCode}</div>");
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap = null;
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            var matchCode = string.Format(NumberGameLang.Number, ticket.MatchCode);

            rtfHelper.RTFRenderer.AddText("\n" + matchCode);
        }
    }
}