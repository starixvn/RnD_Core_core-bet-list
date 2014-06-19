namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Utils;

    /// <summary>
    /// Bet Type: Baccarat.
    /// </summary>
    public class Choice1101 : Choice1
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='stake'>{1}</span>&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>";

        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            if (ticketData != null && ticketData.Count > 0)
            {
                List<ITicketData> foundTicketData = ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));

                if (foundTicketData.Count > 0)
                {
                    var betTeam = new StringBuilder();
                    foreach (ITicketData refDataItem in foundTicketData)
                    {
                        string betTeamName = GetBetTypeName(refDataItem.BetTeam, ticketHelper);
                        string odds = ConvertByBetType.Odds(refDataItem.Odds, ticket.BetTypeId, ticket.OddsType);

                        betTeam.AppendFormat(BetTeamFormat, betTeamName, ConvertByBetType.Stake(refDataItem.Stake), odds);
                    }

                    Template.betTeam = betTeam.ToString();
                    Template.BetType.betTypeName = string.Empty;
                }
            }
            else
            {
                Template.betTeam = string.Empty;
                Template.BetType.betTypeName = CoreLang.PartyBaccarat;
            }

            Template.Match.VS.Hide();
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.leagueName = string.Join(null, new string[] { CoreLang.PartyBaccarat, HtmlCharacters.NoneBreakingSpace, ticket.ShowTime });
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.Hide();
            Template.betTeamClassName = string.Empty;
        }

        /// <summary>
        /// Adjusts the bet team to RTF.
        /// </summary>
        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam
                                    .Replace("<div><span class='favorite'>", string.Empty)
                                    .Replace("</span>", string.Empty)
                                    .Replace("<span class='handicap custom'>", string.Empty)
                                    .Replace("</div>", "\n")
                                    .Replace("<span class='stake'>", string.Empty);

            // Remove the last line-break
            int lastBreak = Template.betTeam.LastIndexOf("\n");
            if (lastBreak >= 0)
            {
                Template.betTeam = Template.betTeam.Remove(lastBreak, 1);
            }
        }

        protected virtual string GetLeagueName()
        {
            return CoreLang.PartyBaccarat;
        }

        protected virtual string GetBetTypeName(string betTeam, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetCasinoBetTeamNameById(betTeam);
        }
    }
}