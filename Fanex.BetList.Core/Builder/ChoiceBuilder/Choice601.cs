namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// BAIO - FT Winning Margin 14 Way
    /// </summary>
    public class Choice601 : Choice4
    {
        protected const string BasketballRefId = "WMBasketball";

        private static readonly Dictionary<string, string> BetTeamResourceKeys = new Dictionary<string, string>
        {
            { BetTeamValue.H1to2, "1-2" },
            { BetTeamValue.H3to6, "3-6" },
            { BetTeamValue.H7to9, "7-9" },
            { BetTeamValue.H10to13, "10-13" },
            { BetTeamValue.H14to16, "14-16" },
            { BetTeamValue.H17to20, "17-20" },
            { BetTeamValue.H21Over, "21+" },
            { BetTeamValue.A1to2, "1-2" },
            { BetTeamValue.A3to6, "3-6" },
            { BetTeamValue.A7to9, "7-9" },
            { BetTeamValue.A10to13, "10-13" },
            { BetTeamValue.A14to16, "14-16" },
            { BetTeamValue.A17to20, "17-20" },
            { BetTeamValue.A21Over, "21+" }
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