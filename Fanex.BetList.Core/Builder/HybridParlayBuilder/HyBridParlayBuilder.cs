namespace Fanex.BetList.Core.Builder.HybridParlayBuilder
{
    using Entities;
    using Fanex.BetList.Core.Templates.HybridParlay;
    using System.Linq;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Utils.Parsers;
    using Fanex.BetList.Core.Utils;
    using System.Text;
    using Fanex.BetList.Core.App_GlobalResources;
    using System;

    public class HyBridParlayBuilder : IHybridParlayBuilder
    {
        private readonly BetListHTMLBuilder _betListHTMLBuilder;
        public HyBridParlayBuilder()
        {
            _betListHTMLBuilder = new BetListHTMLBuilder();
        }
        public string Build(
            ITicketHelper ticketHelper, 
            IList<ITicket> detailTickets, 
            string transDesc,
            int hdp1,
            int hdp2,
            string betCheck,
            string betTeam,
            string currency,
            decimal stake,
            bool isShowScoreMap)
        {
            if (!detailTickets.Any())
            {
                return string.Empty;
            }
            var extraVoidTicketClass = detailTickets.All(x => string.IsNullOrWhiteSpace(x.Status)) || detailTickets.All(x => x.Status?.ToLowerInvariant() == BetStatus.Void) ? "void" : string.Empty;
            var choice = new StringBuilder();
            var combinationOfferName = string.Empty;
            var combinationOfferNameList = ticketHelper.GetCombinationMPOfferNameList(betTeam, betCheck, hdp1, hdp2);
            if (combinationOfferNameList != null && combinationOfferNameList.Count > 0)
            {
                combinationOfferName = combinationOfferNameList[0];
                choice.Append($"<div class=\"offer-name-list {extraVoidTicketClass}\">{combinationOfferName} x <span class=\"unit-stake\">{currency} {stake:N0}</span></div>");
            }

            var mainLegOddsInfo = ExtraInfoParser.ParseLegOddsTransDesc(transDesc).LegOddsbyMatch;
            var hybridParlayMatches = detailTickets.OrderBy(x => x.TransId).GroupBy(x => x.MatchId);
            foreach (var match in hybridParlayMatches)
            {
                choice.Append(BuildMatchTicket(ticketHelper, match.ToList(), mainLegOddsInfo.FirstOrDefault(x => x.MatchId == match.Key), extraVoidTicketClass, isShowScoreMap));
            }
            

            return choice.ToString();
        }

        public HybridParlay_Template BuildMatchTicket(ITicketHelper ticketHelper, IList<ITicket> matchTickets, LegOddsInfo mainLegOddsInfo, string extraVoidTicketClass, bool isShowScoreMap)
        {
            var template = new HybridParlay_Template();
            if (!matchTickets.Any())
            {
                return new HybridParlay_Template();
            }
            template.MatchTicketsList.MatchTicketsList = string.Empty;
            var isSingle = matchTickets.Count ==  1;
            var isVoidMatch = matchTickets.All(x => x.Status?.ToLowerInvariant() == BetStatus.Void);
            var isNotHaveStatus = matchTickets.Any(x => string.IsNullOrWhiteSpace(x.Status));
            var selectionStatusDisplayValues = new List<string>();
            
            var matchMainTicket = matchTickets.Last();
            var oddsType = string.IsNullOrEmpty(matchMainTicket.OddsType) ? (object)0 : matchMainTicket.OddsType;

            matchMainTicket.Odds = isSingle ? RecalculateTicketOdds(matchMainTicket) : matchMainTicket.Odds;

            template.Odds.odds = matchMainTicket.Odds == 0 ? string.Empty : ConvertByBetType.Odds(matchMainTicket.Odds, matchMainTicket.BetTypeId, matchMainTicket.SportTypeId, oddsType);
            if(mainLegOddsInfo != null && matchMainTicket.Odds == 1)
            {
                template.Odds.odds = ConvertByBetType.Odds(mainLegOddsInfo.LegOdds, matchMainTicket.BetTypeId, matchMainTicket.SportTypeId, oddsType);
            }

            template.Odds.OldOdds.Hide();
            if(mainLegOddsInfo != null && matchMainTicket.Odds != mainLegOddsInfo.LegOdds && matchMainTicket.Odds != 1)
            {
                template.Odds.OldOdds.oldOdds = ConvertByBetType.Odds(mainLegOddsInfo.LegOdds, matchMainTicket.BetTypeId, matchMainTicket.SportTypeId, oddsType);
                template.Odds.OldOdds.Show();
            }

            foreach (var matchTicket in matchTickets)
            {
                var matchDetailTicket = BuildMatchDetailTicket(ticketHelper, matchTicket, isNotHaveStatus);
                selectionStatusDisplayValues.Add(matchDetailTicket.Status.status);
                if(isSingle)
                {
                    matchDetailTicket.Status.Hide();
                    matchDetailTicket.Odds.Show();
                    matchDetailTicket.Odds.odds = template.Odds;
                    matchDetailTicket.BetType.Hide();
                    template.Odds.Hide();
                    template.BetType.Show();
                    template.BetType.betTypeName = matchDetailTicket.BetType.bettypeName;
                    template.Status.status = matchDetailTicket.Status.status;
                    template.matchClass = template.Status.status == CoreBetList.voided ? "void" : extraVoidTicketClass;
                }
                else
                {
                    template.matchClass = extraVoidTicketClass;
                    matchDetailTicket.Odds.Hide();
                    matchDetailTicket.BetType.Show();
                    template.BetType.Hide();
                }

                template.MatchTicketsList.MatchTicketsList += matchDetailTicket;
            }

            template.MatchTicketsList.listBuilderClassName = string.Empty;
            if(isSingle)
            {
                template.MatchTicketsList.listBuilderClassName = "single";
            }
            else 
            {
                template.Status.status = BuildMatchStatus(matchTickets, selectionStatusDisplayValues, matchMainTicket, mainLegOddsInfo);
            }

            if(isVoidMatch)
            {
                template.matchClass = "void";
            }

            var matchBuilder = _betListHTMLBuilder.CreateChoiceBuilder(matchMainTicket.BetTypeId);
            var choice = matchBuilder.Render(matchMainTicket, ticketHelper, null, isShowScoreMap);
            template.MatchDetail.homeTeam = choice.Match.homeTeam;
            template.MatchDetail.home_firstGoal_lastGoal = choice.Match.home_firstGoal_lastGoal;
            template.MatchDetail.awayTeam = choice.Match.awayTeam;
            template.MatchDetail.away_firstGoal_lastGoal = choice.Match.away_firstGoal_lastGoal;
            template.MatchDetail.sportTypeName = choice.League.sportTypeName;
            template.MatchDetail.leagueName = choice.League.LeagueName.leagueName;
            template.EventDate.eventDate = choice.EventDate.eventDate;
            template.MatchDetail.Score.score = choice.Score;
            template.MatchDetail.Score.Visible = choice.Score.Visible;
            if(isNotHaveStatus && template.Status.status != CoreBetList.reject || string.IsNullOrEmpty(template.Status.status))
            {
                template.Status.Hide();
            }

            return template;
        }

        public HybridParlayTicket_Template BuildMatchDetailTicket(ITicketHelper ticketHelper, ITicket matchTicket, bool isNotHaveStatus)
        {
            var template = new HybridParlayTicket_Template();
            var builder = _betListHTMLBuilder.CreateChoiceBuilder(matchTicket.BetTypeId);
            var builderStatus = _betListHTMLBuilder.CreateStatusBuilder(matchTicket.BetTypeId);
            var choice = builder.Render(matchTicket, ticketHelper, null, false);
            var status = builderStatus.Render(matchTicket, ticketHelper, null, false);
            template.transId = matchTicket.TransId.ToString();
            template.BetType.bettypeName = choice.BetType.betTypeName;
            template.betTeamClassName = choice.betTeamClassName;
            template.Handicap.handicap = choice.Handicap.handicap;
            template.Handicap.Visible = choice.Handicap.Visible;
            template.betTeam = choice.betTeam;
            template.Status.status = status.result;
            if(isNotHaveStatus)
            {
                template.Status.Hide();
            }
            template.ticketClass = template.Status.status == CoreBetList.voided ? "void" : string.Empty;

            return template;
        }

        public string BuildMatchStatus(IList<ITicket> matchTickets, IList<string> selectionStatusDisplayValues,ITicket matchMainTicket, LegOddsInfo mainLegOddsInfo)
        {
            if (matchTickets.Any(x => x.Status?.ToLowerInvariant() == BetStatus.Reject)) return CoreBetList.reject;
            if (matchTickets.Any(x => string.IsNullOrWhiteSpace(x.Status))) return string.Empty;
            if (selectionStatusDisplayValues.Any(x => x == CoreBetList.lose)) return CoreBetList.lose;
            if (selectionStatusDisplayValues.Any(x => x == CoreBetList.UnderSettlement)) return CoreBetList.UnderSettlement;
            if (selectionStatusDisplayValues.Any(x => x == CoreBetList.DelayPayout)) return CoreBetList.DelayPayout;
            if (matchTickets.Any(x => x.Status?.ToLowerInvariant() == BetStatus.Lose)) return CoreBetList.lose;
            if (selectionStatusDisplayValues.Any(x => x == CoreBetList.waiting)) return CoreBetList.waiting;
            if (matchTickets.Any(x => x.Status?.ToLowerInvariant() == BetStatus.Running)) return CoreBetList.running;
            
            var mainLegOdds = mainLegOddsInfo?.LegOdds ?? 0;
            
            if (matchMainTicket.Odds < 1) return CoreBetList.lose;
            if (matchMainTicket.Odds == 1) return CoreBetList.draw;
            if (matchMainTicket.Odds > 1 && matchMainTicket.Odds <= mainLegOdds) return CoreBetList.won;
            
            return string.Empty;
        }

        public static decimal RecalculateTicketOdds(ITicket matchMainTicket)
        {
            var odds = matchMainTicket.Odds;
            switch(matchMainTicket.Status.ToLowerInvariant())
            {
                case BetStatus.HalfWon:
                    odds = 1 + (odds - 1) / 2;
                    break;
                case BetStatus.HalfLose:
                    odds = 0.5M;
                    break;
                default:
                    break;
            }

            return Math.Floor(odds*100)/100;
        }
    }
}