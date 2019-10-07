namespace Fanex.BetList.Example.MVC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using HtmlAgilityPack;
    using NUnit.Framework;

    [TestFixture]
    [Ignore("Used for testing when changing old bet type")]
    public class BetListControllerTests
    {
        private const string ActiveUrl = "http://ha.nexdev.net:7240/V5/";
        private const string MasterURL = "http://ha.nexdev.net:7240/Main/";

        [TestCase("")]
        [TestCase("BetList/All")]
        [TestCase("BetList/BetRadar")]
        [TestCase("BetList/New11BetTypes")]
        [TestCase("BetList/New60BetTypesBetRadar")]
        [TestCase("BetList/SportingSolutionBetTypes")]
        [TestCase("BetList/GoldDeluxe")]
        [TestCase("BetList/MetricGaming")]
        [TestCase("BetList/BetTrade")]
        [TestCase("BetList/MyanmarOdds")]
        [TestCase("BetList/AGCasino")]
        [TestCase("BetList/PHAllInOne")]
        [TestCase("BetList/PHAllInOneP2")]
        [TestCase("BetList/PHAllInOneP2b")]
        [TestCase("BetList/CockFighting")]
        [TestCase("BetList/RnGCasino")]
        [TestCase("BetList/Allbet")]
        [TestCase("BetList/Voidbridge")]
        [TestCase("BetList/GlobalGaming")]
        [TestCase("BetList/FastMarkets")]
        [TestCase("BetList/NumberGame")]
        [TestCase("BetList/LiveSports")]
        [TestCase("BetList/VirtualSports")]
        [TestCase("BetList/BasketballAIO")]
        [TestCase("BetList/CashOut")]
        [TestCase("BetList/BetRadarVirtualSports")]
        [TestCase("BetList/KenoLottery")]
        [TestCase("BetList/Keno")]
        [TestCase("BetList/PHAIO3")]
        [TestCase("BetList/BonusSystem")]
        [TestCase("BetList/ChangingToHomeAway")]
        [TestCase("BetList/CorrectTranslations")]
        [TestCase("BetList/OneRnGCasino")]
        [TestCase("BetList/FastMarkets2")]
        [TestCase("BetList/Asian1x2HDP")]
        [TestCase("BetList/UpcomingSports")]
        [TestCase("BetList/PP")]
        [TestCase("BetList/Parlay")]
        [TestCase("BetList/BetBuilder")]
        [TestCase("BetList/SettlementStatus")]
        [TestCase("BetList/ESports2")]
        [TestCase("BetList/Happy5")]
        [TestCase("BetList/ESports2InPlay")]
        [TestCase("BetList/CardClub")]
        [TestCase("BetList/AbnormalBet")]
        [TestCase("BetList/PHAIO4A")]
        [TestCase("BetList/RebateBonus")]
        [TestCase("BetList/PHAIO4B")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Choice(string productUrl)
        {
            // Arrange
            const int choiceColumnIndex = 6;

            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, choiceColumnIndex);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, choiceColumnIndex);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("")]
        [TestCase("BetList/All")]
        [TestCase("BetList/BetRadar")]
        [TestCase("BetList/New11BetTypes")]
        [TestCase("BetList/New60BetTypesBetRadar")]
        [TestCase("BetList/SportingSolutionBetTypes")]
        [TestCase("BetList/GoldDeluxe")]
        [TestCase("BetList/MetricGaming")]
        [TestCase("BetList/BetTrade")]
        [TestCase("BetList/MyanmarOdds")]
        [TestCase("BetList/AGCasino")]
        [TestCase("BetList/PHAllInOne")]
        [TestCase("BetList/PHAllInOneP2")]
        [TestCase("BetList/PHAllInOneP2b")]
        [TestCase("BetList/CockFighting")]
        [TestCase("BetList/RnGCasino")]
        [TestCase("BetList/Allbet")]
        [TestCase("BetList/Voidbridge")]
        [TestCase("BetList/GlobalGaming")]
        [TestCase("BetList/FastMarkets")]
        [TestCase("BetList/NumberGame")]
        [TestCase("BetList/LiveSports")]
        [TestCase("BetList/VirtualSports")]
        [TestCase("BetList/BasketballAIO")]
        [TestCase("BetList/CashOut")]
        [TestCase("BetList/BetRadarVirtualSports")]
        [TestCase("BetList/KenoLottery")]
        [TestCase("BetList/Keno")]
        [TestCase("BetList/PHAIO3")]
        [TestCase("BetList/BonusSystem")]
        [TestCase("BetList/ChangingToHomeAway")]
        [TestCase("BetList/CorrectTranslations")]
        [TestCase("BetList/OneRnGCasino")]
        [TestCase("BetList/FastMarkets2")]
        [TestCase("BetList/Asian1x2HDP")]
        [TestCase("BetList/UpcomingSports")]
        [TestCase("BetList/PP")]
        [TestCase("BetList/Parlay")]
        [TestCase("BetList/BetBuilder")]
        [TestCase("BetList/SettlementStatus")]
        [TestCase("BetList/ESports2")]
        [TestCase("BetList/Happy5")]
        [TestCase("BetList/ESports2InPlay")]
        [TestCase("BetList/CardClub")]
        [TestCase("BetList/AbnormalBet")]
        [TestCase("BetList/PHAIO4A")]
        [TestCase("BetList/RebateBonus")]
        [TestCase("BetList/PHAIO4B")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Trans(string productUrl)
        {
            // Arrange
            const int transColumnIndex = 1;

            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, transColumnIndex);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, transColumnIndex);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("")]
        [TestCase("BetList/All")]
        [TestCase("BetList/BetRadar")]
        [TestCase("BetList/New11BetTypes")]
        [TestCase("BetList/New60BetTypesBetRadar")]
        [TestCase("BetList/SportingSolutionBetTypes")]
        [TestCase("BetList/GoldDeluxe")]
        [TestCase("BetList/MetricGaming")]
        [TestCase("BetList/BetTrade")]
        [TestCase("BetList/MyanmarOdds")]
        [TestCase("BetList/AGCasino")]
        [TestCase("BetList/PHAllInOne")]
        [TestCase("BetList/PHAllInOneP2")]
        [TestCase("BetList/PHAllInOneP2b")]
        [TestCase("BetList/CockFighting")]
        [TestCase("BetList/RnGCasino")]
        [TestCase("BetList/Allbet")]
        [TestCase("BetList/Voidbridge")]
        [TestCase("BetList/GlobalGaming")]
        [TestCase("BetList/FastMarkets")]
        [TestCase("BetList/NumberGame")]
        [TestCase("BetList/LiveSports")]
        [TestCase("BetList/VirtualSports")]
        [TestCase("BetList/BasketballAIO")]
        [TestCase("BetList/CashOut")]
        [TestCase("BetList/BetRadarVirtualSports")]
        [TestCase("BetList/KenoLottery")]
        [TestCase("BetList/Keno")]
        [TestCase("BetList/PHAIO3")]
        [TestCase("BetList/BonusSystem")]
        [TestCase("BetList/ChangingToHomeAway")]
        [TestCase("BetList/CorrectTranslations")]
        [TestCase("BetList/OneRnGCasino")]
        [TestCase("BetList/FastMarkets2")]
        [TestCase("BetList/Asian1x2HDP")]
        [TestCase("BetList/UpcomingSports")]
        [TestCase("BetList/PP")]
        [TestCase("BetList/Parlay")]
        [TestCase("BetList/BetBuilder")]
        [TestCase("BetList/SettlementStatus")]
        [TestCase("BetList/ESports2")]
        [TestCase("BetList/Happy5")]
        [TestCase("BetList/ESports2InPlay")]
        [TestCase("BetList/CardClub")]
        [TestCase("BetList/AbnormalBet")]
        [TestCase("BetList/PHAIO4A")]
        [TestCase("BetList/RebateBonus")]
        [TestCase("BetList/PHAIO4B")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Odds(string productUrl)
        {
            // Arrange
            const int oddsColumnIndex = 7;

            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, oddsColumnIndex);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, oddsColumnIndex);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("")]
        [TestCase("BetList/All")]
        [TestCase("BetList/BetRadar")]
        [TestCase("BetList/New11BetTypes")]
        [TestCase("BetList/New60BetTypesBetRadar")]
        [TestCase("BetList/SportingSolutionBetTypes")]
        [TestCase("BetList/GoldDeluxe")]
        [TestCase("BetList/MetricGaming")]
        [TestCase("BetList/BetTrade")]
        [TestCase("BetList/MyanmarOdds")]
        [TestCase("BetList/AGCasino")]
        [TestCase("BetList/PHAllInOne")]
        [TestCase("BetList/PHAllInOneP2")]
        [TestCase("BetList/PHAllInOneP2b")]
        [TestCase("BetList/CockFighting")]
        [TestCase("BetList/RnGCasino")]
        [TestCase("BetList/Allbet")]
        [TestCase("BetList/Voidbridge")]
        [TestCase("BetList/GlobalGaming")]
        [TestCase("BetList/FastMarkets")]
        [TestCase("BetList/NumberGame")]
        [TestCase("BetList/LiveSports")]
        [TestCase("BetList/VirtualSports")]
        [TestCase("BetList/BasketballAIO")]
        [TestCase("BetList/CashOut")]
        [TestCase("BetList/BetRadarVirtualSports")]
        [TestCase("BetList/KenoLottery")]
        [TestCase("BetList/Keno")]
        [TestCase("BetList/PHAIO3")]
        [TestCase("BetList/BonusSystem")]
        [TestCase("BetList/ChangingToHomeAway")]
        [TestCase("BetList/CorrectTranslations")]
        [TestCase("BetList/OneRnGCasino")]
        [TestCase("BetList/FastMarkets2")]
        [TestCase("BetList/Asian1x2HDP")]
        [TestCase("BetList/UpcomingSports")]
        [TestCase("BetList/PP")]
        [TestCase("BetList/Parlay")]
        [TestCase("BetList/BetBuilder")]
        [TestCase("BetList/SettlementStatus")]
        [TestCase("BetList/ESports2")]
        [TestCase("BetList/Happy5")]
        [TestCase("BetList/ESports2InPlay")]
        [TestCase("BetList/CardClub")]
        [TestCase("BetList/AbnormalBet")]
        [TestCase("BetList/PHAIO4A")]
        [TestCase("BetList/RebateBonus")]
        [TestCase("BetList/PHAIO4B")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Stake(string productUrl)
        {
            // Arrange
            const int stakeColumnIndex = 10;

            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, stakeColumnIndex);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, stakeColumnIndex);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("")]
        [TestCase("BetList/All")]
        [TestCase("BetList/BetRadar")]
        [TestCase("BetList/New11BetTypes")]
        [TestCase("BetList/New60BetTypesBetRadar")]
        [TestCase("BetList/SportingSolutionBetTypes")]
        [TestCase("BetList/GoldDeluxe")]
        [TestCase("BetList/MetricGaming")]
        [TestCase("BetList/BetTrade")]
        [TestCase("BetList/MyanmarOdds")]
        [TestCase("BetList/AGCasino")]
        [TestCase("BetList/PHAllInOne")]
        [TestCase("BetList/PHAllInOneP2")]
        [TestCase("BetList/PHAllInOneP2b")]
        [TestCase("BetList/CockFighting")]
        [TestCase("BetList/RnGCasino")]
        [TestCase("BetList/Allbet")]
        [TestCase("BetList/Voidbridge")]
        [TestCase("BetList/GlobalGaming")]
        [TestCase("BetList/FastMarkets")]
        [TestCase("BetList/NumberGame")]
        [TestCase("BetList/LiveSports")]
        [TestCase("BetList/VirtualSports")]
        [TestCase("BetList/BasketballAIO")]
        [TestCase("BetList/CashOut")]
        [TestCase("BetList/BetRadarVirtualSports")]
        [TestCase("BetList/KenoLottery")]
        [TestCase("BetList/Keno")]
        [TestCase("BetList/PHAIO3")]
        [TestCase("BetList/BonusSystem")]
        [TestCase("BetList/ChangingToHomeAway")]
        [TestCase("BetList/CorrectTranslations")]
        [TestCase("BetList/OneRnGCasino")]
        [TestCase("BetList/FastMarkets2")]
        [TestCase("BetList/Asian1x2HDP")]
        [TestCase("BetList/UpcomingSports")]
        [TestCase("BetList/PP")]
        [TestCase("BetList/Parlay")]
        [TestCase("BetList/BetBuilder")]
        [TestCase("BetList/SettlementStatus")]
        [TestCase("BetList/ESports2")]
        [TestCase("BetList/Happy5")]
        [TestCase("BetList/ESports2InPlay")]
        [TestCase("BetList/CardClub")]
        [TestCase("BetList/AbnormalBet")]
        [TestCase("BetList/PHAIO4A")]
        [TestCase("BetList/RebateBonus")]
        [TestCase("BetList/PHAIO4B")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Status(string productUrl)
        {
            // Arrange
            const int statusColumnIndex = 11;

            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, statusColumnIndex);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, statusColumnIndex);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        private List<string> GetColumn(string pageContent, int columnIndex)
        {
            var choiceColumn = new List<string>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(pageContent);

            var tableBodyNode = htmlDoc.DocumentNode?.Descendants("table")?.FirstOrDefault().Descendants("tbody")?.FirstOrDefault();

            var rows = tableBodyNode?.Descendants("tr");

            if (rows != null)
            {
                foreach (var row in rows)
                {
                    var choiceBlock = row.Descendants("td").ToArray()[columnIndex];
                    choiceColumn.Add(choiceBlock?.InnerHtml);
                }
            }

            return choiceColumn;
        }
    }
}