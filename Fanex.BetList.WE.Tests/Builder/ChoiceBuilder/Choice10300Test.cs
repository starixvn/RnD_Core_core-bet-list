namespace Fanex.BetList.WE.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice10300Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
          _choice = new Choice10300();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void GetDisplayChoice_PassRightRefId_ReturnBetChoiceName()
        {
            ////Arrange
            const string choice = "1001";
            const string choiceName = "Bet Choice";
            _ticketHelper.GetResourceData("WE_BC", choice).Returns(choiceName);
            _ticket.TransDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            ////Act
            var displayChoice = (_choice as Choice10300).GetDisplayChoice(_ticketHelper, choice);

            Assert.AreEqual(choiceName, displayChoice);
        }
        
        [Test]
        public void GetDisplayTableCode_PassRightRefId_ReturnDisplayTableCode()
        {
            ////Arrange
            const string tableCode = "STUDIO-BAM-8";
            const string tableCodeName = "Squeeze Baccarat 8";
            _ticketHelper.GetResourceData("WE_BT", tableCode).Returns(tableCodeName);
            _ticket.TransDesc = "choice=1001*comms=1*tableCode=STUDIO-BAM-8*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            ////Act
            var displayTableCode = (_choice as Choice10300).GetDisplayTableCode(_ticketHelper, tableCode);

            Assert.AreEqual(tableCodeName, displayTableCode);
        }
    }
}