namespace Fanex.BetList.Example.MVC.Tests
{
    using System;
    using System.Net.Http;
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
        public void CompareHtmlBetweenMasterBranchAndActiveBranch(string productUrl)
        {
            // Arrange
            var activeClient = new HttpClient { BaseAddress = new Uri(ActiveUrl) };
            var masterClient = new HttpClient { BaseAddress = new Uri(MasterURL) };

            // Act
            var activeResponseHtml = activeClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;
            var masterResponseHtml = masterClient.GetAsync(productUrl).Result.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.AreEqual(masterResponseHtml, activeResponseHtml);
        }
    }
}