namespace Fanex.BetList.SexyGaming.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice11850Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
          _choice = new Choice11850();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void GetDisplayChoice_PassRightRefId_ReturnBetChoiceName()
        {
            ////Arrange
            const string choice = "1001";
            const string choiceName = "Bet Choice";
            _ticketHelper.GetResourceData("ON_BC", choice).Returns(choiceName);
            _ticket.TransDesc = "choice=LW_LW_0*site=BEST-ODDS*tableCode=VNB01";

            ////Act
            var displayChoice = (_choice as Choice11850).GetDisplayChoice(_ticketHelper, choice);

            Assert.AreEqual(choiceName, displayChoice);
        }
        
        [Test]
        public void GetDisplayTableCode_PassRightRefId_ReturnDisplayTableCode()
        {
            ////Arrange
            const string tableCode = "LW_LW_0";
            const string tableCodeName = "VN Baccarat";
            _ticketHelper.GetResourceData("ON_BT", tableCode).Returns(tableCodeName);
            _ticket.TransDesc = "choice=LW_LW_0*site=BEST-ODDS*tableCode=VNB01";

            ////Act
            var displayTableCode = (_choice as Choice11850).GetDisplayTableCode(_ticketHelper, tableCode);

            Assert.AreEqual(tableCodeName, displayTableCode);
        }
    }
}