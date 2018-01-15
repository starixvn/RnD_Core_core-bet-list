namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Double Chance.
    /// </summary>
    public class Choice24 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = ticket.BetTeam;

            if (!string.IsNullOrEmpty(betTeam) && betTeam.Length == 2)
            {
                var firstChance = string.Equals(BetTeamValue.NumberOne, betTeam[0].ToString(), StringComparison.InvariantCultureIgnoreCase)
                        ? CoreBetList.Home
                        : CoreBetList.Away;
                var secondChance = string.Equals(BetTeamValue.X, betTeam[1].ToString(), StringComparison.InvariantCultureIgnoreCase)
                        ? CoreBetList.lblDraw
                        : CoreBetList.Away;

                Template.betTeam = $"{firstChance}<span class=\"{Underdog}\"> {CoreBetList.lblOr} </span>{secondChance}";
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;

            Template.Handicap.handicap = null;
        }
    }
}