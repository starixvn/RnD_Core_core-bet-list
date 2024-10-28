namespace Fanex.BetList.SexyGaming.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Status2401Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3101();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_Over14Days_HideResultLink()
        {
            ////Arrange            
            _ticket.WinlostDate = DateTime.Today.AddDays(-20);

            ////Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_Under14Days_ShowResultLink()
        {
            ////Arrange            
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            ////Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.True(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_Under14Days_PassSiteToGameId3rd()
        {
            ////Arrange
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);
            _ticket.CurrencyId = 1;
            _ticket.CustId = 2;
            _ticket.BetCheck = "3";
            _ticket.SiteId = "4";
            _ticket.TransDesc = "choice=ZJH_STRAIGHT*site=BEST-ODDS*tableCode=STUDIO-ZJH-3";

            ////Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("'1', 'BEST-ODDS', 2, '3', '4'", template.StatusResult.betId);
        }
    }
}