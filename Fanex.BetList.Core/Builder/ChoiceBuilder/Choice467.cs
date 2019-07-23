namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// PHAIO 4A: Half Time/Full Time Exact Total Goals
    /// </summary>
    public class Choice467 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var standardizedBetTeam = ticket.BetTeam?.ToUpperInvariant();

            Template.betTeam = string.Empty;

            if (!string.IsNullOrEmpty(standardizedBetTeam))
            {
                const int expectedNumberOfIndicators = 2;
                const int minimumIdicatorLength = 2;

                var totalGoalsIndicators = standardizedBetTeam.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (totalGoalsIndicators.Length == expectedNumberOfIndicators)
                {
                    var halfTimeTotalGoalsIndicator = totalGoalsIndicators[0].Trim();
                    var fullTimeTotalGoalsIndicator = totalGoalsIndicators[1].Trim();
                    var halfTimeTotalGoals = halfTimeTotalGoalsIndicator.Length >= minimumIdicatorLength
                        ? halfTimeTotalGoalsIndicator.Substring(1)
                        : string.Empty;
                    var fullTimeTotalGoals = fullTimeTotalGoalsIndicator.Length >= minimumIdicatorLength
                        ? fullTimeTotalGoalsIndicator.Substring(1)
                        : string.Empty;

                    if (!string.IsNullOrEmpty(halfTimeTotalGoals) && !string.IsNullOrEmpty(fullTimeTotalGoals))
                    {
                        Template.betTeam = $"{CoreBetList.HalfTime} {halfTimeTotalGoals} / {CoreBetList.FullTime} {fullTimeTotalGoals}";
                    }
                }
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}