namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;

    public class Choice654 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var playerInfo = new BasketballPlayerInfo(ticket.TransDesc);
            var overUnder = !string.IsNullOrEmpty(ticket.BetTeam) && BetTeamValue.O == ticket.BetTeam.ToLowerInvariant() ?
               $"<span class=\"favorite\">{CoreBetList.over}</span>" :
               CoreBetList.under;

            Template.betTeam = $"{ticketHelper.GetTeamNameById(playerInfo.PlayerId)} - {overUnder}";
        }
        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;
            Template.Handicap.handicap = ConvertByBetType.Hdp(handicap);

            Template.betTeamClassName = Underdog;
        }

    }
}