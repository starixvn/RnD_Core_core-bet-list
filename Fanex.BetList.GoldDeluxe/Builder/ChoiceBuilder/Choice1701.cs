namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.Utils;
    using Utils.StringExt;

    public class Choice1701 : Choice1
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;{1}&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>";

        protected virtual string ResourceId => "GD_Baccarat";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            List<ITicketData> foundTicketData = GetTicketReference(ticket, ticketData);

            if (foundTicketData.Count > 0)
            {
                var betTeam = new StringBuilder();

                foreach (ITicketData refDataItem in foundTicketData)
                {
                    string betTeamName = GetBetTeamName(refDataItem, ticketHelper);
                    string odds = ConvertByBetType.Odds(refDataItem.Odds, ticket.BetTypeId, ticket.SportTypeId, ticket.OddsType);

                    betTeam.AppendFormat(BetTeamFormat, betTeamName, ConvertByBetType.Stake(refDataItem.Stake), odds);
                }

                Template.betTeam = betTeam.ToString();
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.Hide();
            Template.betTeamClassName = string.Empty;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match = null;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTypeName = GetBetTypeName(ticket.BetTypeId, ticketHelper);

            Template.League.LeagueName.leagueName = $"{betTypeName}{HtmlCharacters.NoneBreakingSpace}{ticket.BetCheck}";
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score.Hide();
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());
        }

        private string GetBetTeamName(ITicketData ticketData, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetResourceData(ResourceId, ticketData.BetTeam);
        }

        protected static string GetBetTypeName(int betTypeId, ITicketHelper ticketHelper)
        {
            return ticketHelper.GetBetTypeNameById(betTypeId);
        }

        protected static List<ITicketData> GetTicketReference(ITicket ticket, List<ITicketData> ticketData)
        {
            var foundTicketData = new List<ITicketData>();

            if (ticketData != null && ticketData.Count > 0)
            {
                foundTicketData = ticketData.FindAll(item => item.RefNo.Equals(ticket.TransId.ToString()) && item.BetTypeId == ticket.BetTypeId);
            }

            return foundTicketData;
        }
    }
}