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
            _cacheRef.GetLeagueName(Arg.Any<string>(), Arg.Any<string>()).Returns<string>(leagueName);            

            // Act
            var act = ticketHelper.GetLeagueNameById(1, 42);

            // Assert
            Assert.AreEqual(leagueName, act);
        }
    }
}
