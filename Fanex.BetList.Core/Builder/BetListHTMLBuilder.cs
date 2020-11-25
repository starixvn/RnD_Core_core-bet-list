namespace Fanex.BetList.Core.Builder
{
    using System.Collections.Generic;
    using System.Text;
    using ChoiceBuilder;
    using Entities;
    using OddsBuilder;
    using StakeBuilder;
    using StatusBuilder;
    using SystemParlayBuilder;
    using Templates;
    using TransBuilder;

    /// <summary>
    /// This class generates the HTML markup of the Core Bet List.
    /// </summary>
    public class BetListHTMLBuilder : BetListBaseBuilder, IBetListHTMLBuilder
    {
        #region Attributes

        /// <summary>
        /// The _content.
        /// </summary>
        private readonly StringBuilder _content;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BetListHTMLBuilder" /> class.
        /// </summary>
        public BetListHTMLBuilder()
        {
            _content = new StringBuilder();
        }

        #endregion Constructors

        /// <summary>
        /// Gets the bet list.
        /// </summary>
        /// <returns>System.String: the current HTML markup of the bet list.</returns>
        public string GetBetList()
        {
            return _content.ToString();
        }

        /// <summary>
        /// Appends the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder Append(string content)
        {
            _content.Append(content);

            return this;
        }

        /// <summary>
        /// Adds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="openTag">The open tag.</param>
        /// <param name="closeTag">The close tag.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        /// <returns>Current BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder AddChoice(
                        ITicket ticket,
                        ITicketHelper ticketHelper,
                        List<ITicketData> ticketData,
                        string openTag,
                        string closeTag,
                        bool isShowScoreMap)
        {
            string choice = BuildChoice(ticket, ticketHelper, ticketData, isShowScoreMap);
            _content.Append(string.Join(null, new[] { openTag, choice, closeTag }));

            return this;
        }

        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        /// <returns>System.String: the HTML markup of Choice for input data.</returns>
        public string BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            IChoice choiceBuilder = CreateChoiceBuilder(ticket.BetTypeId);
            Choice_Template choice = choiceBuilder.Render(ticket, ticketHelper, ticketData, isShowScoreMap);

            if (!ticketHelper.ShowBetType)
            {
                choice.BetType = null;
            }

            if (!ticketHelper.ShowMatch)
            {
                choice.Match = null;
            }

            if (!ticketHelper.ShowLeague)
            {
                choice.League = null;
            }

            return choice;
        }

        /// <summary>
        /// Builds the choice block.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        /// <returns>Choice Template: the whole Choice block after the build.</returns>
        public Choice_Template BuildChoiceBlock(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            IChoice choiceBuilder = CreateChoiceBuilder(ticket.BetTypeId);

            return choiceBuilder.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        /// <summary>
        /// Adds the odds.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="openTag">The open tag.</param>
        /// <param name="closeTag">The close tag.</param>
        /// <param name="funcGetOddsTypeName">Name of the function get odds type.</param>
        /// <returns>Current BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder AddOdds(ITicket ticket, List<ITicketData> ticketData, string openTag, string closeTag, GetCachePropertyById funcGetOddsTypeName)
        {
            string odds = BuildOdds(ticket, ticketData, funcGetOddsTypeName);
            _content.Append(string.Join(null, new[] { openTag, odds, closeTag }));

            return this;
        }

        /// <summary>
        /// Builds the odds.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="funcGetOddsTypeName">Name of the function get odds type.</param>
        /// <returns>System.String: the HTML markup of Odds for input data.</returns>
        public string BuildOdds(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            IOdds oddsBuilder = CreateOddsBuilder(ticket.BetTypeId);

            return oddsBuilder.Render(ticket, ticketData, funcGetOddsTypeName);
        }

        public string BuildOddsValue(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById getOddsTypeName)
        {
            var oddsBuilder = CreateOddsBuilder(ticket.BetTypeId);

            return oddsBuilder.RenderOddsValue(ticket, ticketData, getOddsTypeName);
        }

        public string BuildOddsTypeName(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById getOddsTypeName)
        {
            var oddsBuilder = CreateOddsBuilder(ticket.BetTypeId);

            return oddsBuilder.RenderOddsType(ticket, ticketData, getOddsTypeName);
        }

        /// <summary>
        /// Adds the stake.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="openTag">The open tag.</param>
        /// <param name="closeTag">The close tag.</param>
        /// <returns>Current BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder AddStake(ITicket ticket, ITicketHelper ticketHelper, string openTag, string closeTag)
        {
            string stake = BuildStake(ticket, ticketHelper);

            _content.Append(string.Join(null, new[] { openTag, stake, closeTag }));
            return this;
        }

        /// <summary>
        /// Builds the stake.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>System.String: the HTML markup of Stake for input data.</returns>
        public string BuildStake(ITicket ticket, ITicketHelper ticketHelper)
        {
            IStake stakeBuilder = CreateStakeBuilder(ticket.BetTypeId);

            return stakeBuilder.Render(ticket, ticketHelper);
        }

        /// <summary>
        /// Adds the trans.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="openTag">The open tag.</param>
        /// <param name="closeTag">The close tag.</param>
        /// <returns>Current BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder AddTrans(ITicket ticket, string openTag, string closeTag)
        {
            string trans = BuildTrans(ticket);
            _content.Append(string.Join(null, new[] { openTag, trans, closeTag }));
            return this;
        }

        /// <summary>
        /// Builds the trans.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>System.String: the HTML markup of Trans for input data.</returns>
        public string BuildTrans(ITicket ticket)
        {
            ITrans transBuilder = CreateTransBuilder(ticket.BetTypeId);

            return transBuilder.Render(ticket);
        }

        public string BuildTransTime(ITicket ticket)
        {
            var transBuilder = CreateTransBuilder(ticket.BetTypeId);

            return transBuilder.RenderTranstime(ticket);
        }

        public string BuildRefNo(ITicket ticket)
        {
            var transBuilder = CreateTransBuilder(ticket.BetTypeId);

            return transBuilder.RenderRefNo(ticket);
        }

        public string BuildLicenseeTxId(ITicket ticket)
        {
            var transBuilder = CreateTransBuilder(ticket.BetTypeId);

            return transBuilder.RenderLicenseeTxId(ticket);
        }

        /// <summary>
        /// Adds the status.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data (Status33 Win/Place).</param>
        /// <param name="openTag">The open tag.</param>
        /// <param name="closeTag">The close tag.</param>
        /// <param name="isShowVNIP">If set to <c>true</c> [is show VNIP].</param>
        /// <returns>Current BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder AddStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, string openTag, string closeTag, bool isShowVNIP)
        {
            string status = BuildStatus(ticket, ticketHelper, ticketData, isShowVNIP);
            _content.Append(string.Join(null, new[] { openTag, status, closeTag }));
            return this;
        }

        /// <summary>
        /// Builds the status.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data (Status33 Win/Place).</param>
        /// <param name="isShowVNIP">If set to <c>true</c> [is show VNIP].</param>
        /// <returns>System.String: the HTML markup of Status for input data.</returns>
        public string BuildStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            IStatus statusBuilder = CreateStatusBuilder(ticket.BetTypeId);
            string status = statusBuilder.Render(ticket, ticketHelper, ticketData, isShowVNIP);

            return status;
        }

        /// <summary>
        /// Builds the system parlay detail.
        /// </summary>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="systemParlayData">The system parlay data.</param>
        /// <returns>System.String: the HTML markup of System Parlay Detail for input data.</returns>
        public string BuildSystemParlayDetail(ITicketHelper ticketHelper, ISystemParlayData systemParlayData)
        {
            var builder = new SystemParlayDetail();
            return builder.Build(ticketHelper, systemParlayData);
        }

        public string BuildTransEventDateAndStatusResult(ITicket ticket, List<ITicketData> ticketData)
        {
            var statusBuilder = CreateStatusBuilder(ticket.BetTypeId);
            var statusText = statusBuilder.RenderStatusResultText(ticket, ticketData);

            return statusText;
        }
    }
}