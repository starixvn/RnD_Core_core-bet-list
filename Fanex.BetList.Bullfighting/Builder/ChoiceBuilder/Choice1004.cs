namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils;
    using Resources;
    using Templates;

    public class Choice1004 : Choice1
    {
        private const string RefId = "BF";

        private string _bankerName;
        private string _playerName;
        private string _arenaCode;
        private string _arenaName;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            const string BankerIndicator = "banker";
            const string PlayerIndicator = "player";
            const string ArenaCodeIndicator = "arenacode";
            IDictionary<string, string> parsedTransDesc = ParseTransDesc(ticket.TransDesc);

            if (parsedTransDesc.ContainsKey(BankerIndicator))
            {
                _bankerName = parsedTransDesc[BankerIndicator];
            }

            if (parsedTransDesc.ContainsKey(PlayerIndicator))
            {
                _playerName = parsedTransDesc[PlayerIndicator];
            }

            if (parsedTransDesc.ContainsKey(ArenaCodeIndicator))
            {
                _arenaCode = parsedTransDesc[ArenaCodeIndicator];
            }

            _arenaName = ticketHelper.GetLeagueName3rd(ticket.LeagueId.ToString());

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.betTeam = ticketHelper.GetResourceData(RefId, ticket.BetTeam);

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = ticket.BetTeam.ToLowerInvariant() == "b" ? Favorite : Underdog;
            Template.Handicap = null;
        }

        protected override void BuildScore(ITicket ticket)
        {
            Template.Score.Visible = ticket.IsLive;

            if (ticket.IsLive)
            {
                Template.Score.SetValue($"<span class=\"{Favorite}\"> [{CoreBetList.Live}]</span>");
            }
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
            => Template.BetType = null;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
            => Template.League.LeagueName.leagueName = ticket.BetCheck;

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Match.SetValue(BuildMatch());
            Template.Match.Append(BuildMatchNo(ticket));
            Template.Match.Append(BuildArena());
        }

        private static string BuildMatchNo(ITicket ticket)
            => $"<div class=\"bullfighting-arena\"><span class=\"match-no\">{BullfightingLabels.MatchNo}:</span> {Formatter.DecFormat(ticket.Handicap1, 0)}</div>";

        private string BuildMatch()
            => $"<div class=\"bullfighting-match\"><span class=\"banker\">{_bankerName}</span> - vs - <span class=\"player\">{_playerName}</span></div>";

        private string BuildArena()
            => $"<div class=\"bullfighting-arena\"><span class=\"arena-code\">{BullfightingLabels.Arena}:</span> {_arenaCode} - {_arenaName}</div>";

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            rtfHelper.RTFRenderer.AddText("\n");

            if (!string.IsNullOrEmpty(_bankerName))
            {
                rtfHelper.RTFRenderer.AddText(_bankerName);
            }

            rtfHelper.RTFRenderer.AddText(" - vs - ");

            if (!string.IsNullOrEmpty(_playerName))
            {
                rtfHelper.RTFRenderer.AddText(_playerName);
            }

            rtfHelper.RTFRenderer.AddText("\n");
            rtfHelper.RTFRenderer.AddText($"{BullfightingLabels.MatchNo}: ", rtfHelper.PosFont);
            rtfHelper.RTFRenderer.AddText(Formatter.DecFormat(ticket.Handicap1, 0));
            rtfHelper.RTFRenderer.AddText("\n");

            if (!string.IsNullOrEmpty(_arenaCode))
            {
                rtfHelper.RTFRenderer.AddText($"{BullfightingLabels.Arena}: ", rtfHelper.PosFont);
                rtfHelper.RTFRenderer.AddText(_arenaCode);
            }

            if (!string.IsNullOrEmpty(_arenaName))
            {
                rtfHelper.RTFRenderer.AddText($" - {_arenaName}");
            }
        }

        private static IDictionary<string, string> ParseTransDesc(string originalTransDesc)
        {
            const int exepectedNumberOfTransDescInfoPairs = 2;

            var parsedTransDesc = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(originalTransDesc))
            {
                const char TransDescSeparator = ';';
                const char KeyValueSeparator = '=';
                string[] keyValuePairs = originalTransDesc.Split(new[] { TransDescSeparator }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string keyValuePair in keyValuePairs)
                {
                    var keyValue = keyValuePair.Split(new[] { KeyValueSeparator }, StringSplitOptions.RemoveEmptyEntries);

                    if (keyValue.Length == exepectedNumberOfTransDescInfoPairs)
                    {
                        parsedTransDesc.Add(keyValue[0].ToLowerInvariant(), keyValue[1]);
                    }
                }
            }

            return parsedTransDesc;
        }
    }
}