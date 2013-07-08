using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fanex.BetList.Core.App_GlobalResources;
using Fanex.BetList.Core.Entities;
using Fanex.BetList.Core.Utils;

namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    public class Choice1104 : Choice1
    {
        protected override void BuildChoice(Entities.ITicket ticket, Entities.ITicketHelper ticketHelper, List<Entities.ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            this.Template.League.LeagueName.leagueName = string.Join(null,
                                                                     new string[] { CoreLang.LiveRoulette, "&nbsp;", ticket.ShowTime });

            if (ticketData != null && ticketData.Count > 0)
            {
                List<ITicketData> foundTicketData = ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));

                if (foundTicketData != null && foundTicketData.Count > 0)
                {
                    string betTeamName;
                    string odds;
                    StringBuilder betTeam = new StringBuilder();

                    foreach (ITicketData refDataItem in foundTicketData)
                    {
                        betTeamName = ticketHelper.GetRouletteBetItemNameById(refDataItem.BetTeam);
                        odds = ConvertByBetType.Odds(refDataItem.Odds, ticket.BetTypeId, ticket.OddsType);

                        betTeam.AppendFormat(
                            "<div><span class='favorite'>{0}</span>&nbsp;<span class='stake'>{1}</span>&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>",
                            betTeamName, ConvertByBetType.Stake(refDataItem.Stake), odds);
                    }

                    this.Template.betTeam = betTeam.ToString();
                    this.Template.Handicap.Hide();
                    this.Template.BetType.betTypeName = string.Empty;
                    this.Template.betTeamClassName = string.Empty;
                    this.Template.Match.VS.Hide();
                }
            }
            else
            {
                this.Template.betTeam = string.Empty;
                this.Template.Handicap.Hide();
                this.Template.BetType.betTypeName = CoreLang.LiveRoulette;
                this.Template.betTeamClassName = string.Empty;
                this.Template.Match.VS.Hide();
            }
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
            //Remove the last line-break
            int lastBreak = Template.betTeam.LastIndexOf("\n");
            if (lastBreak >= 0)
            {
                Template.betTeam = Template.betTeam.Remove(lastBreak, 1);
            }
        }
    }
}