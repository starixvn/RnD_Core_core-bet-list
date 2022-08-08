namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using System.Collections.Generic;

    public class Choice3911 : Choice4
    {

        private readonly Dictionary<string, string> betTeamPatterns
                = new Dictionary<string, string>
                {
                    { "h2", CoreBetList.h_wm_2 },
                    { "h3-4", CoreBetList.h_wm_3_4 },
                    { "h5-7", CoreBetList.h_wm_5_7 },
                    { "h8-11", CoreBetList.h_wm_8_11 },
                    { "h12+", CoreBetList.h_wm_over_12 },
                    { "a2", CoreBetList.a_wm_2 },
                    { "a3-4", CoreBetList.a_wm_3_4 },
                    { "a5-7", CoreBetList.a_wm_5_7 },
                    { "a8-11", CoreBetList.a_wm_8_11 },
                    { "a12+", CoreBetList.a_wm_over_12 }
                };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int minimumBetTeamLengthRequired = 2;

            var standardizedBetTeam = ticket.BetTeam.Trim().ToLowerInvariant();

            Template.betTeam = string.Empty;

            if (standardizedBetTeam.Length >= minimumBetTeamLengthRequired)
            {
                Template.betTeam = betTeamPatterns.ContainsKey(standardizedBetTeam) ? betTeamPatterns[standardizedBetTeam] : string.Empty;
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
    }
}