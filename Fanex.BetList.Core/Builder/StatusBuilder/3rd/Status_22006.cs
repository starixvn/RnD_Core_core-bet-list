// ---------------------------------------------------------------------------------
// <copyright file="Status_22006.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.04.11">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.StatusBuilder._3rd
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    /// <summary>
    /// Build Status for PlayTech Casino.
    /// </summary>
    public class Status_22006 : Status1
    {
        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowVNIP">If set to <c>true</c> [VNIP is shown].</param>
        /// <returns>Status_Template: the whole Status block, built on input data.</returns>
        public override Status_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<Entities.ITicketData> ticketData, bool isShowVNIP)
        {
            string ticketBetStatus = ticket.Status.ToLower();
            Template.result = ticketBetStatus;
            string result = this.GetResultString(ticket.Status);

            if (string.Empty != result)
            {
                Template.result = result;
            }

            Template.StatusResult = null;
            Template.ShowIP = null;

            return Template;
        }

        /// <summary>
        /// Gets the result string.
        /// </summary>
        /// <param name="ticketStatus">The ticket status.</param>
        /// <returns>System.String: result.</returns>
        private string GetResultString(string ticketStatus)
        {
            string result = string.Empty;

            switch (ticketStatus.ToLower())
            {
                case BetStatus.Running:
                    result = CoreBetList.running;
                    break;
                case BetStatus.Reject:
                    result = CoreBetList.reject;
                    break;
                case BetStatus.Won:
                    result = CoreBetList.won;
                    break;
                case BetStatus.Lose:
                    result = CoreBetList.lose;
                    break;
                case BetStatus.Draw:
                    result = CoreBetList.draw;
                    break;
                case BetStatus.Waiting:
                    result = CoreBetList.waiting;
                    break;
                case BetStatus.Void:
                    result = CoreBetList.voided;
                    break;
                case BetStatus.Refund:
                    result = CoreBetList.refund;
                    break;
            }

            return result;
        }
    }
}