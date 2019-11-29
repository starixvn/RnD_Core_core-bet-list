namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Pool House All In One Phase 5: Penalty Shootout Exact Total Goals (First 10).
    /// </summary>
    public class Choice391 : Choice1
    {
        private const string GoalIndicator = "g";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.Trim().ToLowerInvariant();

            switch (betTeam)
            {
                case BetTeamValue.G4:
                    Template.betTeam = CoreBetList.ZeroToFour;
                    break;

                case BetTeamValue.G9Over:
                    Template.betTeam = CoreBetList.NineAndOver;
                    break;

                default:
                    Template.betTeam = IsValidBetTeam(betTeam) ? betTeam.Substring(GoalIndicator.Length) : string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        private static bool IsValidBetTeam(string betTeam)
        {
            const int minLengthRequired = 2;

            return betTeam.Length >= minLengthRequired && betTeam.StartsWith(GoalIndicator, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}