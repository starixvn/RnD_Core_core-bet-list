namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;

    public class Choice3913 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var awayTeamName = ticketHelper.GetTeamNameById(ticket.AwayId);
            var homeTeamName = ticketHelper.GetTeamNameById(ticket.HomeId);
            switch (ticket.BetTeam.ToLowerInvariant())
            {
                case BetTeamValue.HU:
                    Template.betTeam = $"<span class=\"underdog\">{homeTeamName} &</span> {CoreBetList.under}";
                    break;

                case BetTeamValue.HO:
                    Template.betTeam = $"<span class=\"underdog\">{homeTeamName} &</span> {CoreBetList.over}";
                    break;

                case BetTeamValue.AU:
                    Template.betTeam = $"<span class=\"underdog\">{awayTeamName} &</span> {CoreBetList.under}";
                    break;

                case BetTeamValue.AO:
                    Template.betTeam = $"<span class=\"underdog\">{awayTeamName} &</span> {CoreBetList.over}";
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = ConvertByBetType.Hdp(ticket.Handicap1);
            Template.betTeamClassName = ticket.BetTeam.ToLowerInvariant().Contains(BetTeamValue.O) ? Favorite : Underdog;
        }
    }
}