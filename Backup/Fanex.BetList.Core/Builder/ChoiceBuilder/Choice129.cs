namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    ///  Choice for bet type 129 First Half Exact Goal.
    /// </summary>
    public class Choice129 : Choice6
    {
        /// <summary>
        /// Set resource bet team.
        /// </summary>
        /// <param name="ticket"> Ticket with bet type 129.</param>
        /// <param name="ticketHelper"> Not null ticket helper.</param>
        /// <param name="ticketData"> A valid ticket data.</param>
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam)
            {
                case "0":
                    Template.betTeam = CoreBetList.NoGoals;
                    break;

                case "1":
                    Template.betTeam = CoreBetList.OneGoal;
                    break;

                case "2":
                    Template.betTeam = CoreBetList.TwoGoals;
                    break;

                case "3":
                    Template.betTeam = CoreBetList.ThreeGoals;
                    break;

                case "4":
                    Template.betTeam = CoreBetList.FourGoals;
                    break;

                case "4&over":
                    Template.betTeam = CoreBetList.FourAndOver;
                    break;

                case "5":
                    Template.betTeam = CoreBetList.FiveGoals;
                    break;

                case "5&over":
                    Template.betTeam = CoreBetList.FiveAndOver;
                    break;

                case "6":
                    Template.betTeam = CoreBetList.SixGoals;
                    break;

                case "7&over":
                    Template.betTeam = CoreBetList.SevenAndOver;
                    break;
            }
        }
    }
}