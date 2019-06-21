namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: Both Team Score/ Result
    /// </summary>
    public class Choice417 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.YH:
                    Template.betTeam = $"{CoreBetList.lblYes}/{CoreBetList.Home}";
                    break;

                case BetTeamValue.YD:
                    Template.betTeam = $"{CoreBetList.lblYes}/{CoreBetList.draw}";
                    break;

                case BetTeamValue.YA:
                    Template.betTeam = $"{CoreBetList.lblYes}/{CoreBetList.Away}";
                    break;

                case BetTeamValue.NH:
                    Template.betTeam = $"{CoreBetList.lblNo}/{CoreBetList.Home}";
                    break;

                case BetTeamValue.ND:
                    Template.betTeam = $"{CoreBetList.lblNo}/{CoreBetList.draw}";
                    break;

                case BetTeamValue.NA:
                    Template.betTeam = $"{ CoreBetList.lblNo}/{CoreBetList.Away}";
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}