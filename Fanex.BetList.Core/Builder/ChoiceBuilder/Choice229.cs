namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using Entities;
    using Utils;

    public class Choice229 : Choice1
    {
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var timeNumber = Convert.ToInt32(ticket.Handicap1) + 1;
            var timeString = Formatter.FormatTwoDigit(timeNumber);
            var timeMachine = $"<span class='favorite'> (00:00 - {timeString}:00)</span>";

            var teamNameColor = GetTeamNameColor(ticket);
            var handicapValue = teamNameColor == Favorite ? -Math.Abs(ticket.OddsSpread) : Math.Abs(ticket.OddsSpread);
            Template.Handicap.handicap = ConvertByBetType.Hdp(handicapValue) + timeMachine;
            Template.betTeamClassName = teamNameColor;
        }

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            Template.Handicap.handicap = Template.Handicap.handicap.Replace("<span class='favorite'>", string.Empty);
            Template.Handicap.handicap = Template.Handicap.handicap.Replace("</span>", string.Empty);

            base.BuildBetFigure(ticket, ref rtfHelper, isMixParlay);
        }

        private string GetTeamNameColor(ITicket ticket)
        {
            var isBetHomeTeam = IsBetHomeTeam(ticket);
            var betTeamClassName = string.Empty;

            if (isBetHomeTeam)
            {
                betTeamClassName = ticket.OddsSpread < 0 ? Favorite : Underdog;
            }
            else
            {
                betTeamClassName = ticket.OddsSpread > 0 ? Favorite : Underdog;
            }

            return betTeamClassName;
        }

    }
}