namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Core.Utils;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;
    using Fanex.BetList.Core.TicketHelper;

    [TestFixture]
    public class TicketHelperTest
    {
        private ITicketHelper ticketHelper;
        private ICacheRef _cacheRef;

        [SetUp]
        public void Setup()
        {
            _cacheRef = Substitute.For<ICacheRef>();
            _cacheRef.GetResourceData("Saba", "Saba").Returns("Saba");
            ticketHelper = new TicketHelper(_cacheRef);
        }

        [TestCase("Saba Super league name")]
        [TestCase("SABA Super league name")]
        [TestCase("English Saba Super league name")]
        [TestCase("English SABA Super league name")]
        public void GetLeagueNameById_HideSabaTextIsFalse_ShowSabaText(string leagueName)
        {
            // Arrange
            ticketHelper.HideSabaText = false;
            _cacheRef.GetLeagueName(Arg.Any<string>()).Returns<string>(leagueName);            

            // Act
            var act = ticketHelper.GetLeagueNameById(1, 42);

            // Assert
            Assert.AreEqual(leagueName, act);
        }

        [TestCase("Saba Super league name", "Super league name")]
        [TestCase("SABA Super league name", "Super league name")]
        [TestCase("English Saba Super league name", "English Super league name")]
        public void GetLeagueNameById_HideSabaTextIsTrue_LeagueGroupIdNeedRemove_HideSabaText(string leagueName, string expectedLeagueName)
        {
            // Arrange
            ticketHelper.HideSabaText = true;
            _cacheRef.GetLeagueName(Arg.Any<string>()).Returns<string>(leagueName);            

            // Act
            var act = ticketHelper.GetLeagueNameById(1, 42);

            // Assert
            Assert.AreEqual(expectedLeagueName, act);
        }

        [TestCase("Saba Super league name", "Saba Super league name")]
        [TestCase("SABA Super league name", "SABA Super league name")]
        [TestCase("English Saba Super league name", "English Saba Super league name")]
        public void GetLeagueNameById_HideSabaTextIsTrue_LeagueGroupIdNoNeedRemove_ShowSabaText(string leagueName, string expectedLeagueName)
        {
            // Arrange
            ticketHelper.HideSabaText = true;
            _cacheRef.GetLeagueName(Arg.Any<string>()).Returns<string>(leagueName);            

            // Act
            var act = ticketHelper.GetLeagueNameById(1, 0);

            // Assert
            Assert.AreEqual(expectedLeagueName, act);
        }
    }
}
