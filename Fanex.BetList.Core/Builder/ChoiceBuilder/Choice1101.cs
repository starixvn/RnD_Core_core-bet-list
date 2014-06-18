namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Bet Type: Baccarat.
    /// </summary>
    public class Choice1101 : Choice1
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

            var betTypeName = this.GetLeagueName();
            this.Template.League.LeagueName.leagueName = string.Join(null, new string[] { betTypeName, HtmlCharacter.NoneBreakingSpace, ticket.ShowTime });

            if (ticketData != null && ticketData.Count > 0)
            {
                List<ITicketData> foundTicketData = ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));

                if (foundTicketData != null && foundTicketData.Count > 0)
                {
                    string betTeamName;
                    string odds;
                    StringBuilder betTeam = new StringBuilder();
                    string betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='stake'>{1}</span>&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>";
                    foreach (ITicketData refDataItem in foundTicketData)
                    {
                        betTeamName = this.GetBetTypeName(refDataItem.BetTeam, ticketHelper);
                        odds = ConvertByBetType.Odds(refDataItem.Odds, ticket.BetTypeId, ticket.OddsType);

                        betTeam.AppendFormat(betTeamFormat, betTeamName, ConvertByBetType.Stake(refDataItem.Stake), odds);
                    }

                    this.Template.betTeam = betTeam.ToString();
                    this.Template.BetType.betTypeName = string.Empty;
                }
            }
            else
            {
                this.Template.betTeam = string.Empty;
                this.Template.BetType.betTypeName = betTypeName;
            }

            this.Template.Handicap.Hide();
            this.Template.betTeamClassName = string.Empty;
            this.Template.Match.VS.Hide();
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