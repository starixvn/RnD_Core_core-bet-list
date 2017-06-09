namespace Fanex.BetList.Example.MVC.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using HtmlAgilityPack;
    using NUnit.Framework;

    [TestFixture]
    public class BetListControllerTests
    {
        private const string ActiveUrl = "http://10.18.200.202:7240/";
        private const string MasterURL = "http://10.18.200.209:7240/";

        [TestCase("/")]
        [TestCase("/BetList/GoldDeluxe")]
        [TestCase("/BetList/MetricGamming")]
        [TestCase("/BetList/MicroGamming")]
        [TestCase("/BetList/BetTrade")]
        [TestCase("/BetList/MyanmarOdds")]
        [TestCase("/BetList/ColossusBets")]
        [TestCase("/BetList/AGCasino")]
        [TestCase("/BetList/PHAllInOne")]
        [TestCase("/BetList/PHAllInOneP2")]
        [TestCase("/BetList/PHAllInOneP2b")]
        [TestCase("/BetList/CockFighting")]
        [TestCase("/BetList/RnGCasino")]
        [TestCase("/BetList/Allbet")]
        [TestCase("/BetList/Voidbridge")]
        [TestCase("/BetList/GlobalGaming")]
        [TestCase("/BetList/FastMarkets")]
        [TestCase("/BetList/NumberGame")]
        [TestCase("/BetList/LiveSports")]
        [TestCase("/BetList/CashOut")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Choice(string productUrl)
        {
            // Arrange
            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, 4);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, 4);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("/")]
        [TestCase("/BetList/GoldDeluxe")]
        [TestCase("/BetList/MetricGamming")]
        [TestCase("/BetList/MicroGamming")]
        [TestCase("/BetList/BetTrade")]
        [TestCase("/BetList/MyanmarOdds")]
        [TestCase("/BetList/ColossusBets")]
        [TestCase("/BetList/AGCasino")]
        [TestCase("/BetList/PHAllInOne")]
        [TestCase("/BetList/PHAllInOneP2")]
        [TestCase("/BetList/PHAllInOneP2b")]
        [TestCase("/BetList/CockFighting")]
        [TestCase("/BetList/RnGCasino")]
        [TestCase("/BetList/Allbet")]
        [TestCase("/BetList/Voidbridge")]
        [TestCase("/BetList/GlobalGaming")]
        [TestCase("/BetList/FastMarkets")]
        [TestCase("/BetList/NumberGame")]
        [TestCase("/BetList/LiveSports")]
        [TestCase("/BetList/CashOut")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Trans(string productUrl)
        {
            // Arrange
            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, 1);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, 1);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("/")]
        [TestCase("/BetList/GoldDeluxe")]
        [TestCase("/BetList/MetricGamming")]
        [TestCase("/BetList/MicroGamming")]
        [TestCase("/BetList/BetTrade")]
        [TestCase("/BetList/MyanmarOdds")]
        [TestCase("/BetList/ColossusBets")]
        [TestCase("/BetList/AGCasino")]
        [TestCase("/BetList/PHAllInOne")]
        [TestCase("/BetList/PHAllInOneP2")]
        [TestCase("/BetList/PHAllInOneP2b")]
        [TestCase("/BetList/CockFighting")]
        [TestCase("/BetList/RnGCasino")]
        [TestCase("/BetList/Allbet")]
        [TestCase("/BetList/Voidbridge")]
        [TestCase("/BetList/GlobalGaming")]
        [TestCase("/BetList/FastMarkets")]
        [TestCase("/BetList/NumberGame")]
        [TestCase("/BetList/LiveSports")]
        [TestCase("/BetList/CashOut")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Odds(string productUrl)
        {
            // Arrange
            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, 5);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, 5);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("/")]
        [TestCase("/BetList/GoldDeluxe")]
        [TestCase("/BetList/MetricGamming")]
        [TestCase("/BetList/MicroGamming")]
        [TestCase("/BetList/BetTrade")]
        [TestCase("/BetList/MyanmarOdds")]
        [TestCase("/BetList/ColossusBets")]
        [TestCase("/BetList/AGCasino")]
        [TestCase("/BetList/PHAllInOne")]
        [TestCase("/BetList/PHAllInOneP2")]
        [TestCase("/BetList/PHAllInOneP2b")]
        [TestCase("/BetList/CockFighting")]
        [TestCase("/BetList/RnGCasino")]
        [TestCase("/BetList/Allbet")]
        [TestCase("/BetList/Voidbridge")]
        [TestCase("/BetList/GlobalGaming")]
        [TestCase("/BetList/FastMarkets")]
        [TestCase("/BetList/NumberGame")]
        [TestCase("/BetList/LiveSports")]
        [TestCase("/BetList/CashOut")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Stake(string productUrl)
        {
            // Arrange
            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, 6);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, 6);

            // Assert
            Assert.AreEqual(masterBetListChoiceColumnHtml, activeBetListChoiceColumnHtml);
        }

        [TestCase("/")]
        [TestCase("/BetList/GoldDeluxe")]
        [TestCase("/BetList/MetricGamming")]
        [TestCase("/BetList/MicroGamming")]
        [TestCase("/BetList/BetTrade")]
        [TestCase("/BetList/MyanmarOdds")]
        [TestCase("/BetList/ColossusBets")]
        [TestCase("/BetList/AGCasino")]
        [TestCase("/BetList/PHAllInOne")]
        [TestCase("/BetList/PHAllInOneP2")]
        [TestCase("/BetList/PHAllInOneP2b")]
        [TestCase("/BetList/CockFighting")]
        [TestCase("/BetList/RnGCasino")]
        [TestCase("/BetList/Allbet")]
        [TestCase("/BetList/Voidbridge")]
        [TestCase("/BetList/GlobalGaming")]
        [TestCase("/BetList/FastMarkets")]
        [TestCase("/BetList/NumberGame")]
        [TestCase("/BetList/LiveSports")]
        [TestCase("/BetList/CashOut")]
        public void CompareHtmlBetweenMasterBranchAndActiveBranch_Status(string productUrl)
        {
            // Arrange
            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            var activeBetListChoiceColumnHtml = GetColumn(activeResponseHtml, 7);
            var masterBetListChoiceColumnHtml = GetColumn(masterResponseHtml, 7);

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