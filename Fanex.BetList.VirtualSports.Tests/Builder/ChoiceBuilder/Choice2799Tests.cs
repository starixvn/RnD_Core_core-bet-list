namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2799Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2799();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetRadarSharedBetTypes.MixParlay;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(BetRadarSharedBetTypes.MixParlay).Returns("Mix Parlay");
        }

        [Test]
        public void BuildChoice_Always_BetTypeNameIsTheNameOfBetType2911GettingFromRemotingRef()
        {
            // Arrange.
            const string TestingBetTypeName = "Testing Name";

            _ticketHelper.GetBetTypeNameById(BetRadarSharedBetTypes.MixParlay).Returns(TestingBetTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.Contains(TestingBetTypeName, template.ToString());
        }

        [Test]
        public void BuildChoice_ClientSystemParlaySubbetsDetailFunctionOfTicketHelperIsNotNull_SetClientSystemParlaySubbetsDetailFunctionAsJsFunctionOfTheLink()
        {
            // Arrange.
            const string TestingMixParlaySubBetsFuncName = "TestingFunc";

            _ticketHelper.ClientSystemParlaySubbetsDetailFunction = TestingMixParlaySubBetsFuncName;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.Contains($"<a id=\"hidden0\" href=\"{TestingMixParlaySubBetsFuncName}\">", template.ToString());
        }

        [Test]
        public void BuildChoice_ClientSystemParlaySubbetsDetailFunctionOfTicketHelperIsNull_BuildDefaultMixParlaySubBetsFuncName()
        {
            // Arrange.
            _ticketHelper.ClientSystemParlaySubbetsDetailFunction = null;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.Contains($"<a id=\"hidden0\" href=\"javascript:showNewCombMP(", template.ToString());
        }

        [Test]
        public void BuildChoice_ClientSystemParlaySubbetsDetailFunctionOfTicketHelperIsNull_BuildDefaultMixParlaySubBetsFuncWithAllNeededParameters()
        {
            // Arrange.
            const long testingCustId = 123;
            const long testingTransId = 123;
            const int testingIndex = 1;
            const string testingRefNo = "123";
            const decimal testingHandicap1 = 1;
            const decimal testingHandicap2 = 4;
            const string testingBetCheck = "128";
            const string testingBetTeam = "1";

            _ticket.CustId = testingCustId;
            _ticket.TransId = testingTransId;
            _ticket.RefNo = testingRefNo;
            _ticket.Handicap1 = testingHandicap1;
            _ticket.Handicap2 = testingHandicap2;
            _ticket.BetCheck = testingBetCheck;
            _ticket.BetTeam = testingBetTeam;

            _ticketHelper.Index = testingIndex;
            _ticketHelper.ClientSystemParlaySubbetsDetailFunction = null;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParamterList = $"'{testingTransId}',{testingIndex},'{testingRefNo}',{testingHandicap1},{testingHandicap2},'{testingBetCheck}','{testingBetTeam}',{testingCustId}";
            var expectedHtml = $"<a id=\"hidden0\" href=\"javascript:showNewCombMP({expectedParamterList});\">";

            StringAssert.Contains(expectedHtml, template.ToString());
        }

        [Test]
        public void BuildChoice_Always_AddCombinationAfterBetTypeName()
        {
            // Arrange.
            const string TestingCombination = "Trebles (1bets)";

            _ticketHelper
                .GetCombinationMPOfferNameList(_ticket.BetTeam, _ticket.BetCheck, _ticket.Handicap1, _ticket.Handicap2)
                .Returns(new List<string> { TestingCombination });

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.Contains($"<div>{TestingCombination}</div>", template.ToString());
        }
    }
}