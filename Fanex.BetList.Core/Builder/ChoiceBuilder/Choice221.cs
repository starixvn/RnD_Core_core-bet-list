namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Text.RegularExpressions;
    using Entities;
    using Utils;

    /// <summary>
    /// Fast Markets: Next 1 minute.
    /// </summary>
    public class Choice221 : Choice223
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            if (ticket.OddsSpread != 0)
            {
                Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.OddsSpread);

                if (!string.IsNullOrEmpty(Template.betTeam))
                {
                    Template.betTeam += "<br/>";
                }
            }
            else
            {
                Template.Handicap = null;
            }

            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            var htmlTagMatching = new Regex("</?[^<>]*>");
            string choice = htmlTagMatching.Replace(Template.betTeam, string.Empty);

            if (Template.Handicap != null && Template.Handicap.Visible && Template.Handicap.handicap != null)
            {
                if (!string.IsNullOrEmpty(Template.betTeam))
                {
                    choice += "\n";
                }

                choice += Template.Handicap.handicap;
            }

            if (Template.Score != null && Template.Score.Visible)
            {
                choice += htmlTagMatching.Replace(Template.Score, string.Empty);
            }

            if (!string.IsNullOrEmpty(choice))
            {
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }
    }
}