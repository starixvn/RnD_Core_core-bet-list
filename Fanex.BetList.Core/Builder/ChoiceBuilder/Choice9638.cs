namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// Home 1st Inns Wicket X Method of Dismissal (6 Way)
    /// </summary>
    public class Choice9638 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betteams = new Dictionary<string, string>
            {
                { BetTeamValue.C, CoreBetList.Caught },
                { BetTeamValue.B, CoreBetList.Bowled },
                { BetTeamValue.LBW, CoreBetList.LBW },
                { BetTeamValue.Ro, CoreBetList.RunOut },
                { BetTeamValue.S, CoreBetList.Stumped },
                { BetTeamValue.Ot, CoreBetList.Others }
            };
            Template.betTeam = betteams.ContainsKey(ticket.BetTeam.ToLowerInvariant()) ?
                betteams[ticket.BetTeam.ToLowerInvariant()] :
                ticket.BetTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        => Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
    }
}