namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Utils;

    /// <summary>
    /// Bet Type: HT/FT.
    /// </summary>
    public class Choice16 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = ConvertByBetType.GetBetTeam(ticket.BetTeam, ticket.BetTypeId);

            //char[] chars = ticket.BetTeam.ToCharArray();
            //var betTeam = string.Empty;
            //if (chars[0] == '1')
            //{
            //    betTeam = CoreLang.h;
            //}
            //else if (chars[0] == '2')
            //{
            //    betTeam = CoreLang.a;
            //}
            //else
            //{
            //    betTeam = CoreLang.d;
            //}

            //if (chars[c - 1] == '1')
            //{
            //    betTeam += CoreLang.h;
            //}
            //else if (chars[c - 1] == '2')
            //{
            //    betTeam += CoreLang.a;
            //}
            //else
            //{
            //    betTeam += CoreLang.d;
            //}

            //Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }
    }
}