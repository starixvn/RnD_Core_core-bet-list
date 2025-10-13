namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// BAIO -  FT Winning Margin 12 Way
    /// </summary>
    public class Choice602 : Choice601
    {
        private static readonly Dictionary<string, string> BetTeamResourceKeys = new Dictionary<string, string>
        {
            { BetTeamValue.H1to5, "1-5" },
            { BetTeamValue.H6to10, "6-10" },
            { BetTeamValue.H11to15, "11-15" },
            { BetTeamValue.H16to20, "16-20" },
            { BetTeamValue.H21to25, "21-25" },
            { BetTeamValue.H26Over, "26+" },
            { BetTeamValue.A1to5, "1-5" },
            { BetTeamValue.A6to10, "6-10" },
            { BetTeamValue.A11to15, "11-15" },
            { BetTeamValue.A16to20, "16-20" },
            { BetTeamValue.A21to25, "21-25" },
            { BetTeamValue.A26Over, "26+" }
        };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = GetBetTeamValue(ticket, ticketHelper);
            Template.betTeam = betTeam;
        }

        private string GetBetTeamValue(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTeamLower = ticket.BetTeam?.ToLowerInvariant();
            
            if (string.IsNullOrEmpty(betTeamLower) || !BetTeamResourceKeys.ContainsKey(betTeamLower))
            {
                return string.Empty;
            }

            var resourceKey = BetTeamResourceKeys[betTeamLower];
            var teamType = GetTeamType(betTeamLower);
            
            return string.Format(ticketHelper.GetResourceData(BasketballRefId, resourceKey), teamType);
        }

        private string GetTeamType(string betTeam)
        {
            return betTeam.Length > 0 && betTeam[0] == 'h' ? CoreBetList.Home : CoreBetList.Away;
        }
    }
}