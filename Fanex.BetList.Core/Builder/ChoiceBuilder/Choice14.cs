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
            { "0:0", CoreLang.no0 + HtmlCharacters.NoneBreakingSpace + CoreLang.goal },
            { "1:1", CoreLang.home + HtmlCharacters.NoneBreakingSpace + CoreLang.FG },
            { "1:2", CoreLang.home + HtmlCharacters.NoneBreakingSpace + CoreLang.LG },
            { "2:1", CoreLang.away + HtmlCharacters.NoneBreakingSpace + CoreLang.FG },
            { "2:2", CoreLang.away + HtmlCharacters.NoneBreakingSpace + CoreLang.LG }
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