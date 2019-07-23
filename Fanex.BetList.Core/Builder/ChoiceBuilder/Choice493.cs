namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// PHAIO 4B: HT/FT Total Corners.
    /// </summary>
    public class Choice493 : Choice413
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = ticket.BetTeam.ToLowerInvariant();
            var memberChoice = string.Empty;

            if (BetTeamValue.AOS == betTeam)
            {
                memberChoice = CoreBetList.AOS;
            }
            else
            {
                const int expectedNumberOfTotalCorners = 2;

                var totalCornersByHalves = betTeam.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (totalCornersByHalves.Length == expectedNumberOfTotalCorners)
                {
                    var halfTimeTotalCorners = totalCornersByHalves[0].Trim();
                    var fullTimeTotalCorners = totalCornersByHalves[1].Trim();

                    memberChoice = $"{CoreBetList.HalfTime} {halfTimeTotalCorners} / {CoreBetList.FullTime} {fullTimeTotalCorners}";
                }
            }

            Template.betTeam = memberChoice;
        }
    }
}