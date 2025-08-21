namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using System;
    using System.Collections.Generic;

    public class ChoiceRNG : Choice1
    {
        protected virtual string BuildDetailsLink(ITicket ticket)
        => string.Empty;

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.betTeam = $"<span class=\"handicap\">{ticketHelper.GetBetTypeNameById(ticket.BetTypeId)}</span>";

            if (IsDetailTicket(ticket.BetCheck))
            {
                Template.betTeam += $" {GetTransId3rd(ticket.BetCheck)}";
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName = null;

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildDetailsLink(ticket));
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void BuildScore(ITicket ticket)
            => Template.Score = null;

        protected override void AdjustBetTeamToRTF()
            => Template.betTeam = Template.betTeam.StripHTML();

        protected override void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            var sportTypeName = Template.League.sportTypeName;

            if (!string.IsNullOrEmpty(sportTypeName))
            {
                rtfHelper.RTFRenderer.AddText($"\n{sportTypeName}");

                if(Template.League.LeagueName != null){
                    var gameGroupName = Template.League.LeagueName.ToString().StripHTML();
                    rtfHelper.RTFRenderer.AddText(gameGroupName);
                }
            }
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
           => Template.Match = null;

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;


        protected virtual bool IsDetailTicket(string betCheck)
            => !string.IsNullOrEmpty(betCheck);

        protected virtual bool IsSummaryTicket(string betCheck)
            => !IsDetailTicket(betCheck);

        protected virtual string GetTransId3rd(string transId3rd)
        {
            const string transId3rdSeparator = "|$|";
            if (transId3rd == null || !transId3rd.Contains(transId3rdSeparator))
            {
                return transId3rd;
            }

            var transIdParts = transId3rd.Split(new string[] { transId3rdSeparator }, StringSplitOptions.RemoveEmptyEntries);

            return transIdParts[transIdParts.Length - 1];
        }
    }
}