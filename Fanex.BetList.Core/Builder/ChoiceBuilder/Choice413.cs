namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;

    public class Choice413 : Choice4
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string choice = ticket.BetTeam;

            if (BetTeamValue.AOS == choice.ToLowerInvariant())
            {
                choice = CoreBetList.AOS;
            }

            // "&nbsp" because export format
            Template.betTeam = HtmlCharacters.NoneBreakingSpace + choice;
        }

        protected override void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            if (BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant() && !string.IsNullOrEmpty(ticket.TransDesc))
            {
                if (ticket.IsLive)
                {
                    Template.Score.homeScore = ticket.LiveHomeScore.ToString();
                    Template.Score.awayScore = ticket.LiveAwayScore.ToString();
                    Template.Score.scoreClassName = Favorite;
                    Template.Score.Append(BuildExcludingBlock(ticket.TransDesc));
                }
                else
                {
                    Template.Score.SetValue(BuildExcludingBlock(ticket.TransDesc));
                }
            }
            else
            {
                base.BuildScore(ticket);
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId);
        }

        private string FormatTransDesc(string transDesc)
        {
            return Regex.Replace(transDesc, ",\\s*", ", ");
        }

        private string BuildExcludingBlock(string transDesc)
        {
            string[] excludingTag = new string[]
            {
                "<div class=\"excluding-choices\">",
                CoreBetList.Excluding,
                ": ",
                FormatTransDesc(transDesc),
                "</div>"
            };

            return string.Join(null, excludingTag);
        }
    }
}