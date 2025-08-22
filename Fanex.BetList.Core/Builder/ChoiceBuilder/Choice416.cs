namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    public class Choice416 : Choice413
    {
        private const string Space = " ";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = HtmlCharacters.NoneBreakingSpace + BuildBetChoiceString(ticket.BetTeam);
        }

        private static string BuildBetChoiceString(string betTeam)
        {
            if (string.IsNullOrEmpty(betTeam))
            {
                return string.Empty;
            }

            var betChoice = betTeam.ToLowerInvariant().Replace(BetTeamValue.AOS, CoreBetList.AOS).Replace(":", "-");

            var halfTimeChoice = betChoice.Substring(0, betChoice.IndexOf("/", StringComparison.InvariantCulture)).Trim();
            var fullTimeChoice = betChoice.Remove(0, betChoice.IndexOf("/", StringComparison.InvariantCulture) + 1).Trim();

            betChoice = $"{CoreBetList.HalfTime} {halfTimeChoice} / {CoreBetList.FullTime} {fullTimeChoice}";

            return betChoice;
        }

        protected override void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            base.BuildScore(ticket);

            if (Template.Score.Visible)
            {
                Template.Score.Append(BuildExcludingBlock(ticket.BetTeam, ticket.TransDesc));
            }
            else
            {
                Template.Score.Visible = true;
                Template.Score.SetValue(BuildExcludingBlock(ticket.BetTeam, ticket.TransDesc));
            }
        }

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            string choice = Template.betTeam;

            if (Template.Handicap != null && Template.Handicap.Visible && Template.Handicap.handicap != null)
            {
                choice += string.Join(null, Space, Template.Handicap.handicap);
            }

            if (Template.Score != null && Template.Score.Visible && ticket.IsLive)
            {
                choice += string.Join(null, " [", Template.Score.homeScore, "-", Template.Score.awayScore, "]");
            }

            if (isMixParlay)
            {
                choice += string.Join(null, " @ ", Formatter.FormatNumber3(ticket.Odds, null));
            }

            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                choice += BuildTransDescChoice(ticket.BetTeam, ticket.TransDesc);
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        private string BuildTransDescChoice(string betTeam, string transDesc)
        {
            string betTeamValue = betTeam.ToLowerInvariant();
            string result;
            if (betTeamValue.StartsWith(BetTeamValue.AOS, StringComparison.InvariantCulture)
                && betTeamValue.EndsWith(BetTeamValue.AOS, StringComparison.InvariantCulture))
            {
                result = BuildHtExcludingString(transDesc) + BuildFtExcludingString(transDesc);
            }
            else if (betTeamValue.StartsWith(BetTeamValue.AOS, StringComparison.InvariantCulture)
                && !betTeamValue.EndsWith(BetTeamValue.AOS, StringComparison.InvariantCulture))
            {
                result = BuildHtExcludingString(transDesc);
            }
            else if (!betTeamValue.StartsWith(BetTeamValue.AOS, StringComparison.InvariantCulture)
                && betTeamValue.EndsWith(BetTeamValue.AOS, StringComparison.InvariantCulture))
            {
                result = BuildFtExcludingString(transDesc);
            }
            else
            {
                result = string.Empty;
            }

            return result;
        }

        private static string FormatTransDesc(string transDesc, bool isFullTime)
        {
            var result = Regex.Replace(transDesc, ",\\s*", ", ").Replace(":", "-");
            var indexOfSlash = result.IndexOf("/", StringComparison.InvariantCulture);

            result = isFullTime
                ? result.Substring(indexOfSlash + 1, result.Length - (indexOfSlash + 1))
                : result.Substring(0, indexOfSlash);

            return result;
        }

        private static string BuildExcludingBlock(string betTeam, string transDesc)
        {
            if (string.IsNullOrEmpty(transDesc))
            {
                return string.Empty;
            }

            var betTeamValue = betTeam.ToLowerInvariant();

            if (betTeamValue.StartsWith(BetTeamValue.AOS, StringComparison.InvariantCulture)
                && betTeamValue.EndsWith(BetTeamValue.AOS, StringComparison.InvariantCulture))
            {
                return BuildHtExcludingBlock(transDesc) + BuildFtExcludingBlock(transDesc);
            }

            if (betTeamValue.StartsWith(BetTeamValue.AOS, StringComparison.InvariantCulture)
                && !betTeamValue.EndsWith(BetTeamValue.AOS, StringComparison.InvariantCulture))
            {
                return BuildHtExcludingBlock(transDesc);
            }

            if (!betTeamValue.StartsWith(BetTeamValue.AOS, StringComparison.InvariantCulture)
                && betTeamValue.EndsWith(BetTeamValue.AOS, StringComparison.InvariantCulture))
            {
                return BuildFtExcludingBlock(transDesc);
            }

            return string.Empty;
        }

        private static string BuildHtExcludingBlock(string transDesc)
        {
            string[] excludingTag =
            {
                "<div class=\"excluding-choices\">",
                CoreBetList.AOSHTExcluding,
                ": ",
                FormatTransDesc(transDesc, false),
                "</div>"
            };

            return string.Join(null, excludingTag);
        }

        private static string BuildFtExcludingBlock(string transDesc)
        {
            string[] excludingTag =
           {
                "<div class=\"excluding-choices\">",
                CoreBetList.AOSFTExcluding,
                ": ",
                FormatTransDesc(transDesc, true),
                "</div>"
           };

            return string.Join(null, excludingTag);
        }

        private static string BuildHtExcludingString(string transDesc)
        {
            string[] excluding =
                {
                    "\n",
                    CoreBetList.AOSHTExcluding,
                    ": ",
                    FormatTransDesc(transDesc, false)
                };

            return string.Join(null, excluding);
        }

        private static string BuildFtExcludingString(string transDesc)
        {
            string[] excluding =
                {
                    "\n",
                    CoreBetList.AOSFTExcluding,
                    ": ",
                    FormatTransDesc(transDesc, true)
                };

            return string.Join(null, excluding);
        }
    }
}