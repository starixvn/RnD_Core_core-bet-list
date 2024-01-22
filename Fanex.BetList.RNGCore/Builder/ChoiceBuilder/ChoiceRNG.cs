namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Fanex.BetList.Core.TransId3rdHelper;
    using System;
    using System.Collections.Generic;

    public class ChoiceRNG : Choice1
    {
        private string _parseTransId3rd;

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
            var gameGroupName = Template.League.LeagueName.ToString().StripHTML();

            if (!string.IsNullOrEmpty(sportTypeName) && !string.IsNullOrEmpty(gameGroupName))
            {
                rtfHelper.RTFRenderer.AddText($"\n{sportTypeName}{gameGroupName}");
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
            
        protected virtual string GetTransId3rd(string transId3rd){
            if(_parseTransId3rd == null){
                _parseTransId3rd = TransId3rdRNGHelper.GetTransId3rd(transId3rd);
            }

            return _parseTransId3rd;
        }
    }
}