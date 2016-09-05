namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
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
            string choice = ticket.BetTeam;
            choice = choice.ToLowerInvariant().Replace(BetTeamValue.AOS, CoreBetList.AOS).Replace(":", "-");

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

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var parentBetTypeId = ticketHelper.GetParentIdByBetTypeId(ticket.BetTypeId);

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(parentBetTypeId, ticket.RefNo);
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

            if (!string.IsNullOrEmpty(ticket.TransDesc))
            {
                string betTeam = ticket.BetTeam.ToLowerInvariant();

                if (betTeam.StartsWith(BetTeamValue.AOS) && betTeam.EndsWith(BetTeamValue.AOS))
                {
                    choice += BuildHTExcludingString(ticket.TransDesc);
                    choice += BuildFTExcludingString(ticket.TransDesc);
                }
                else if (betTeam.StartsWith(BetTeamValue.AOS) && !betTeam.EndsWith(BetTeamValue.AOS))
                {
                    choice += BuildHTExcludingString(ticket.TransDesc);
                }
                else if (!betTeam.StartsWith(BetTeamValue.AOS) && betTeam.EndsWith(BetTeamValue.AOS))
                {
                    choice += BuildFTExcludingString(ticket.TransDesc);
                }
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        private string FormatTransDesc(string transDesc, bool isFullTime)
        {
            string result = Regex.Replace(transDesc, ",\\s*", ", ").Replace(":", "-");
            int indexOfSlash = result.IndexOf("/");

            if (isFullTime)
            {
                result = result.Substring(indexOfSlash + 1, result.Length - (indexOfSlash + 1));
            }
            else
            {
                result = result.Substring(0, indexOfSlash);
            }

            return result;
        }

        private string BuildExcludingBlock(string betTeam, string transDesc)
        {
            if (string.IsNullOrEmpty(transDesc))
            {
                return string.Empty;
            }

            betTeam = betTeam.ToLowerInvariant();

            if (betTeam.StartsWith(BetTeamValue.AOS) && betTeam.EndsWith(BetTeamValue.AOS))
            {
                return BuildHTExcludingBlock(transDesc) + BuildFTExcludingBlock(transDesc);
            }
            else if (betTeam.StartsWith(BetTeamValue.AOS) && !betTeam.EndsWith(BetTeamValue.AOS))
            {
                return BuildHTExcludingBlock(transDesc);
            }
            else if (!betTeam.StartsWith(BetTeamValue.AOS) && betTeam.EndsWith(BetTeamValue.AOS))
            {
                return BuildFTExcludingBlock(transDesc);
            }

            return string.Empty;
        }

        private string BuildHTExcludingBlock(string transDesc)
        {
            string[] excludingTag = new string[]
            {
                "<div class=\"excluding-choices\">",
                CoreBetList.AOSHTExcluding,
                ": ",
                FormatTransDesc(transDesc, false),
                "</div>"
            };

            return string.Join(null, excludingTag);
        }

        private string BuildFTExcludingBlock(string transDesc)
        {
            string[] excludingTag = new string[]
           {
                "<div class=\"excluding-choices\">",
                CoreBetList.AOSFTExcluding,
                ": ",
                FormatTransDesc(transDesc, true),
                "</div>"
           };

            return string.Join(null, excludingTag);
        }

        private string BuildHTExcludingString(string transDesc)
        {
            string[] excluding = new string[]
                {
                    "\n",
                    CoreBetList.AOSHTExcluding,
                    ": ",
                    FormatTransDesc(transDesc, false)
                };

            return string.Join(null, excluding);
        }

        private string BuildFTExcludingString(string transDesc)
        {
            string[] excluding = new string[]
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