namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// Bet type name: Both Teams to Score/ Total Goal
    /// </summary>
    public class Choice418 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeamPattern = $"{{0}} <span class=\"{Underdog}\">{CoreBetList.lblEt}</span> {{1}}";

            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.YO:
                    Template.betTeam = string.Format(betTeamPattern, CoreBetList.lblYes, CoreBetList.over);
                    break;

                case BetTeamValue.YU:
                    Template.betTeam = string.Format(betTeamPattern, CoreBetList.lblYes, CoreBetList.under);
                    break;

                case BetTeamValue.NO:
                    Template.betTeam = string.Format(betTeamPattern, CoreBetList.lblNo, CoreBetList.over);
                    break;

                case BetTeamValue.NU:
                    Template.betTeam = string.Format(betTeamPattern, CoreBetList.lblNo, CoreBetList.under);
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
            Template.betTeamClassName = Favorite;
        }
    }
}