namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2799Tests
    {
        private const int TestingBetTypeId = 2799;

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2799();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = TestingBetTypeId;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(TestingBetTypeId).Returns("Mix Parlay");
        }

        [Test]
        public void BuildChoice_Always_BetTypeNameIsTheNameOfBetType2911GettingFromRemotingRef()
        {
            // Arrange.
            const string TestingBetTypeName = "Testing Name";

            _ticketHelper.GetBetTypeNameById(TestingBetTypeId).Returns(TestingBetTypeName);

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
            const long TestingTransId = 123;
            const int TestingIndex = 1;
            const string TestingRefNo = "123";
            const decimal TestingHandicap1 = 1;
            const decimal TestingHandicap2 = 4;
            const string TestingBetCheck = "128";
            const string TestingBetTeam = "1";

            _ticket.TransId = TestingTransId;
            _ticket.RefNo = TestingRefNo;
            _ticket.Handicap1 = TestingHandicap1;
            _ticket.Handicap2 = TestingHandicap2;
            _ticket.BetCheck = TestingBetCheck;
            _ticket.BetTeam = TestingBetTeam;

            _ticketHelper.Index = TestingIndex;
            _ticketHelper.ClientSystemParlaySubbetsDetailFunction = null;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParamterList = $"'{TestingTransId}','{TestingIndex}','{TestingRefNo}','{TestingHandicap1}','{TestingHandicap2}','{TestingBetCheck}','{TestingBetTeam}'";
            var expectedHtml = $"<a id=\"hidden0\" href=\"javascript:showNewCombMP({expectedParamterList});\">";

            StringAssert.Contains(expectedHtml, template.ToString());
        }

        [Test]
        public void BuildChoice_Always_AddCombinationAfterBetTypeName()
        {
            // Arrange.
            const string TestingCombination = "Trebles (1bets)";

            _ticketHelper
                .GetCombinationMPOfferNameList(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<decimal>())
                .Returns(new List<string> { TestingCombination });

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.Contains($"<div>{TestingCombination}</div>", template.ToString());
        }
    }
}