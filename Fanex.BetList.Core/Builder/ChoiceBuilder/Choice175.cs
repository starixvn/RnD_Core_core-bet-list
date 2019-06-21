namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Bet type name: Match Decided Method.
    /// </summary>
    public class Choice175 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.HR:
                    Template.betTeam = $"{Template.Match.homeTeam}/{CoreBetList.RegularTime}";
                    break;

                case BetTeamValue.HE:
                    Template.betTeam = $"{Template.Match.homeTeam}/{CoreBetList.ExtraTime}";
                    break;

                case BetTeamValue.HP:
                    Template.betTeam = $"{Template.Match.homeTeam}/{CoreBetList.Penalty}";
                    break;

                case BetTeamValue.AR:
                    Template.betTeam = $"{Template.Match.awayTeam}/{CoreBetList.RegularTime}";
                    break;

                case BetTeamValue.AE:
                    Template.betTeam = $"{Template.Match.awayTeam}/{CoreBetList.ExtraTime}";
                    break;

                case BetTeamValue.AP:
                    Template.betTeam = $"{Template.Match.awayTeam}/{CoreBetList.Penalty}";
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