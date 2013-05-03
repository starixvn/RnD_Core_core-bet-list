// ---------------------------------------------------------------------------------
// <copyright file="Choice1235.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.02.28">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Virtual Tennis - ScoreBet
    /// </summary>
    public class Choice1235 : Choice1220
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);
            string choice = string.Empty;

            switch (ticket.BetTeam.Trim())
            {
                case "1":
                    choice = CoreBetList.virtualtennis_scorebet_win_0;
                    break;
                case "2":
                    choice = CoreBetList.virtualtennis_scorebet_win_15;
                    break;
                case "3":
                    choice = CoreBetList.virtualtennis_scorebet_win_30;
                    break;
                case "4":
                    choice = CoreBetList.virtualtennis_scorebet_win_40;
                    break;
                case "5":
                    choice = CoreBetList.virtualtennis_scorebet_0_win;
                    break;
                case "6":
                    choice = CoreBetList.virtualtennis_scorebet_15_win;
                    break;
                case "7":
                    choice = CoreBetList.virtualtennis_scorebet_30_win;
                    break;
                case "8":
                    choice = CoreBetList.virtualtennis_scorebet_40_win;
                    break;
            }

            this.Template.betTeam = choice;
        }
    }
}