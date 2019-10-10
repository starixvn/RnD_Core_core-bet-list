namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Badminton AIO: Game X Winning Margin
    /// </summary>
    public class Choice711 : Choice4
    {
        public const string BadmintonWinningMarginRefId = "WMBadminton";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int minimumBetTeamLengthRequired = 3;

            var standardizedBetTeam = ticket.BetTeam.Trim().ToLowerInvariant();

            Template.betTeam = string.Empty;

            if (standardizedBetTeam.Length >= minimumBetTeamLengthRequired)
            {
                var winningMargin = standardizedBetTeam.Substring(1);
                var betTeamPattern = ticketHelper.GetResourceData(BadmintonWinningMarginRefId, winningMargin);

                if (!string.IsNullOrEmpty(betTeamPattern))
                {
                    string selection;

                    switch (standardizedBetTeam[0].ToString())
                    {
                        case BetTeamValue.A:
                            selection = CoreBetList.Away;
                            break;

                        case BetTeamValue.H:
                            selection = CoreBetList.Home;
                            break;

                        default:
                            selection = string.Empty;
                            break;
                    }

                    if (!string.IsNullOrEmpty(selection))
                    {
                        Template.betTeam = string.Format(betTeamPattern, selection);
                    }
                }
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
    }
}