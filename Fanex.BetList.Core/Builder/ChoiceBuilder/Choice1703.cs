namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Constants;

    public class Choice1703 : Choice1701
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span></div>";
        private const string LeagueFormat = "<div class='league'><span class='sport'>{0}</span><span class='leagueName'>&nbsp;{1}&nbsp;{2}</span></div>";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, System.Collections.Generic.List<ITicketData> ticketData)
        {
            Template.betTeam = string.Empty;
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            var league = GetLeagueName();

            if (IsNotRunningTicket(ticket))
            {
                league = string.Format("<a id='hidden0' href='javascript:showDetail(\"parlay_{0}\");'>{1}</a>", ticket.RefNo, league);
            }


            Template.League.LeagueName.leagueName = league;
        }

        protected override string GetLeagueName()
        {
            return CoreBetList.ParlayBaccarat;
        }

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildChoice(ticket, ticketHelper, ticketData);

            var ticketDetails = GetTicketReference(ticket, ticketData);
            if (ticketDetails.Count > 0 && IsNotRunningTicket(ticket))
            {
                var choiceDetails = string.Format("<div id='parlay_{0}' style='display:none;'>", ticket.RefNo);
                int count = 1;
                foreach (var ticketDetail in ticketDetails)
                {
                    var betTeam = string.Format(BetTeamFormat, ticketHelper.GetGoldDeluxeBetTeamNameById(ticketDetail.BetTeam));
                    var league = string.Format(LeagueFormat, ticketHelper.GetSportNameById(ticket.SportTypeId), CoreBetList.ParlayBaccarat, ticketDetail.BetCheck);

                    choiceDetails += string.Format("<div class='ticketList'>{0}{1}</div>", betTeam, league);
                    if (count++ < ticketDetails.Count)
                    {
                        choiceDetails += "<div class='line'></div>";
                    }
                }

                choiceDetails += "</div>";

                Template.League.SetValue(Template.League.ToString() + choiceDetails);
            }
        }

        private bool IsNotRunningTicket(ITicket ticket)
        {
            return ticket.StatusId.ToString() != BetStatusId.Running;
        }
    }
}