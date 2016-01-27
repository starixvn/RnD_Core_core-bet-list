namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    public class Choice413 : Choice4
    {
        private const string Space = " ";

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

            base.BuildScore(ticket);

            if (BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant() && !string.IsNullOrEmpty(ticket.TransDesc))
            {
                if (Template.Score.Visible)
                {
                    Template.Score.Append(BuildExcludingBlock(ticket.TransDesc));
                }
                else
                {
                    Template.Score.Visible = true;
                    Template.Score.SetValue(BuildExcludingBlock(ticket.TransDesc));
                }
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId);
        }

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            string choice = Template.betTeam;

            if (Template.Handicap != null && Template.Handicap.Visible && Template.Handicap.handicap != null)
            {
                choice += string.Join(null, new string[] { Space, Template.Handicap.handicap });
            }

            if (Template.Score != null && Template.Score.Visible && ticket.IsLive)
            {
                choice += string.Join(null, new string[] { " [", Template.Score.homeScore, "-", Template.Score.awayScore, "]" });
            }

            if (isMixParlay)
            {
                choice += string.Join(null, new string[] { " @ ", Formatter.FormatNumber3(ticket.Odds, null) });
            }

            if (BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant() && !string.IsNullOrEmpty(ticket.TransDesc))
            {
                string[] excluding = new string[]
                {
                    "\n",
                    CoreBetList.Excluding,
                    ": ",
                    FormatTransDesc(ticket.TransDesc)
                };

                choice += string.Join(null, excluding);
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
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