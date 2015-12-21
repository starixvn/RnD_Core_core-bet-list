namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Resources;
    using Fanex.BetList.Core.Entities;

    public class Choice1003 : Choice1
    {
        internal const string BetChoiceKey = "betchoice";
        private const string CockOwnerKey = "cockowner";
        private const string MeronChoice = "1";
        private const string WalaChoice = "2";
        internal const string BDDChoice = "3";
        internal const string FTDChoice = "4";
        private const string ResourceId = "CF";

        private readonly IDictionary<string, string> _betChoiceClassNames = new Dictionary<string, string>
        {
            { MeronChoice, "meron-choice" },
            { WalaChoice, "wala-choice" },
            { BDDChoice, "bdd-choice" },
            { FTDChoice, "ftd-choice" }
        };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (ticket == null || ticketHelper == null)
            {
                return;
            }

            Template.Handicap = null;
            base.BuildBetTeam(ticket, ticketHelper, ticketData);

            var transDescData = ParseTransDesc(ticket.TransDesc);

            if (transDescData.ContainsKey(BetChoiceKey))
            {
                var betChoice = transDescData[BetChoiceKey];

                Template.betTeam = ticketHelper.GetResourceData(ResourceId, betChoice);
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType = null;
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (ticket == null || ticketHelper == null)
            {
                return;
            }

            base.BuildMatch(ticket, ticketHelper);

            var transDescData = ParseTransDesc(ticket.TransDesc);

            if (transDescData.ContainsKey(CockOwnerKey))
            {
                var homeAwayCombineData = transDescData[CockOwnerKey].Split(',');

                Template.Match.homeTeam = homeAwayCombineData[0];
                Template.Match.awayTeam = homeAwayCombineData[1];
            }
        }

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (ticket == null || ticketHelper == null)
            {
                return;
            }

            base.BuildLeague(ticket, ticketHelper);

            var arenaName = ticketHelper.GetLeagueNameById(ticket.BetCheck);
            var fightNo = string.Format(CockFightingLang.FightNo, ticket.MatchId);

            Template.League.LeagueName.leagueName = string.Format("{0} {1}", arenaName, fightNo);
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            if (ticket == null)
            {
                return;
            }

            var transDescData = ParseTransDesc(ticket.TransDesc);

            if (transDescData.ContainsKey(BetChoiceKey)
                && _betChoiceClassNames.ContainsKey(transDescData[BetChoiceKey]))
            {
                Template.betTeamClassName = _betChoiceClassNames[transDescData[BetChoiceKey]];
            }
        }

        protected override void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildSport(ticket, ticketHelper);
        }

        internal static Dictionary<string, string> ParseTransDesc(string transDesc)
        {
            Dictionary<string, string> transDescItems = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(transDesc))
            {
                return transDescItems;
            }

            var keyValuePairs = transDesc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyValuePair in keyValuePairs)
            {
                var keyValue = keyValuePair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == 2)
                {
                    transDescItems.Add(keyValue[0].ToLower(), keyValue[1]);
                }
            }

            return transDescItems;
        }
    }
}