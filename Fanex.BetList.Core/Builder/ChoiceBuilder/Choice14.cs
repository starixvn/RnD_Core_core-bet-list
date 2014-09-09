namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;

    /// <summary>
    /// First Goal/Last Goal.
    /// </summary>
    public class Choice14 : Choice1
    {
        private static readonly Dictionary<string, string> BetTeamPatterns
              = new Dictionary<string, string>
        {
            { "0:0", CoreBetList.no0 + HtmlCharacters.NoneBreakingSpace + CoreBetList.goal },
            { "1:1", CoreBetList.home + HtmlCharacters.NoneBreakingSpace + CoreBetList.FG },
            { "1:2", CoreBetList.home + HtmlCharacters.NoneBreakingSpace + CoreBetList.LG },
            { "2:1", CoreBetList.away + HtmlCharacters.NoneBreakingSpace + CoreBetList.FG },
            { "2:2", CoreBetList.away + HtmlCharacters.NoneBreakingSpace + CoreBetList.LG }
        };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {            
            Template.betTeam = 
                (!string.IsNullOrWhiteSpace(ticket.BetTeam) && BetTeamPatterns.ContainsKey(ticket.BetTeam))
                ? BetTeamPatterns[ticket.BetTeam]
                : string.Empty;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}