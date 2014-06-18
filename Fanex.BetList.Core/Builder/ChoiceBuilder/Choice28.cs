namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// 3-way handicap.
    /// </summary>
    public class Choice28 : Choice1
    {
        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            Template.Handicap.handicap = null;

            string teamName = CoreBetList.lblDraw;
            decimal hdp1 = ticket.Handicap1;
            decimal hdp2 = ticket.Handicap2;

            string handicap = string.Empty;
            Template.betTeamClassName = Favorite;

            if ("1" == ticket.BetTeam)
            {
                teamName = Template.Match.homeTeam;
                handicap = Formatter.FormatSignNumber((int)(hdp2 - hdp1));
            }
            else if ("2" == ticket.BetTeam)
            {
                teamName = Template.Match.awayTeam;
                handicap = Formatter.FormatSignNumber((int)(hdp1 - hdp2));
            }
            else
            {
                teamName = CoreBetList.lblDraw;
                if (hdp1 - hdp2 > 0)
                {
                    handicap = CoreBetList.a + HtmlCharacters.NoneBreakingSpace;
                }
                else if (hdp1 - hdp2 < 0)
                {
                    handicap = CoreBetList.h + HtmlCharacters.NoneBreakingSpace;
                }
                else
                {
                    handicap = string.Empty;
                }

                handicap += Formatter.FormatSignNumber((int)Math.Abs(hdp2 - hdp1));
            }

            Template.betTeam = string.Join(null, new string[] { teamName, "&nbsp;<span class=\"underdog\">(", handicap.ToString(), ")</span>" });
        }
    }
}