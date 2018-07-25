namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// Bet Type: Baccarat.
    /// </summary>
    public class Choice1101 : Choice1
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;{1}&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.Match.VS.Hide();
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            List<ITicketData> foundTicketData = GetTicketReference(ticket, ticketData);

            if (foundTicketData.Count > 0)
            {
                var betTeam = new StringBuilder();
                foreach (ITicketData refDataItem in foundTicketData)
                {
                    string betTeamName = GetBetTeamName(refDataItem, ticketHelper);
                    string odds = ConvertByBetType.Odds(refDataItem.Odds, ticket.BetTypeId, ticket.OddsType);

                    betTeam.AppendFormat(BetTeamFormat, betTeamName, ConvertByBetType.Stake(refDataItem.Stake), odds);
                }

                Template.betTeam = betTeam.ToString();
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            List<ITicketData> foundTicketData = GetTicketReference(ticket, ticketData);

            Template.BetType.betTypeName = foundTicketData.Count > 0 ? string.Empty : GetBetTypeName(ticket.BetTypeId, ticketHelper);
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = GetBetTypeName(ticket.BetTypeId, ticketHelper);

            Template.League.LeagueName.leagueName = string.Join(null, new[] { betTypeName, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.Hide();
            Template.betTeamClassName = string.Empty;
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam
                                    .Replace("<div><span class='favorite'>", string.Empty)
                                    .Replace("</span>", string.Empty)
                                    .Replace("<span class='handicap custom'>", string.Empty)
                                    .Replace("</div>", "\n");

            // Remove the last line-break
            int lastBreak = Template.betTeam.LastIndexOf("\n", StringComparison.InvariantCulture);

            if (lastBreak >= 0)
            {
                Template.betTeam = Template.betTeam.Remove(lastBreak, 1);
            }
        }

        protected virtual string GetBetTypeName(int betTypeId, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetBetTypeNameById(betTypeId);
        }

        protected virtual string GetBetTeamName(ITicketData ticketData, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetCasinoBetTeamNameById(ticketData.BetTeam);
        }

        protected virtual List<ITicketData> GetTicketReference(ITicket ticket, List<ITicketData> ticketData)
        {
            var foundTicketData = new List<ITicketData>();

            if (ticketData != null && ticketData.Count > 0)
            {
                foundTicketData = ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo) && item.BetTypeId == ticket.BetTypeId);
            }

            return foundTicketData;
        }
    }
}