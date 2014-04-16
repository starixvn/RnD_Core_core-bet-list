// ---------------------------------------------------------------------------------
// <copyright file="BetListHTMLBuilder.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.11.04">Adjust builder creation to work with 3rd products which have negative bettype ids (internal convention)</change>
//     <change who="Marc.Bui" date="2013.10.08">Check stylecop & refactor</change>
//     <change who="Marc.Bui" date="2013.07.22">Add BuildChoiceBlock to get sub-block</change>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

/// <summary>
/// The Builder namespace.
/// </summary>
namespace Fanex.BetList.Core.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Builder.SystemParlayBuilder;
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    /// <summary>
    /// This class generates the HTML markup of the Core Bet List.
    /// </summary>
    public class BetListHTMLBuilder : IBetListBuilder
    {
        #region Attributes

        /// <summary>
        /// The choice builder namespace.
        /// </summary>
        private const string CHOICEBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.ChoiceBuilder";

        /// <summary>
        /// The odds builder namespace.
        /// </summary>
        private const string ODDSBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.OddsBuilder";

        /// <summary>
        /// The stake builder namespace.
        /// </summary>
        private const string STAKEBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.StakeBuilder";

        /// <summary>
        /// The trans builder namespace.
        /// </summary>
        private const string TRANSBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.TransBuilder";

        /// <summary>
        /// The status builder namespace.
        /// </summary>
        private const string STATUSBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.StatusBuilder";

        /// <summary>
        /// The _content.
        /// </summary>
        private StringBuilder _content;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BetListHTMLBuilder" /> class.
        /// </summary>
        public BetListHTMLBuilder()
        {
            this._content = new StringBuilder();
        }

        #endregion

        /// <summary>
        /// Gets the bet list.
        /// </summary>
        /// <returns>System.String: the current HTML markup of the bet list.</returns>
        public string GetBetList()
        {
            return this._content.ToString();
        }

        /// <summary>
        /// Appends the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder Append(string content)
        {
            this._content.Append(content);
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
            this._content.Append(string.Join(null, new string[] { openTag, choice, closeTag }));
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
            IChoice choiceBuilder = this.CreateChoiceBuilder(ticket.BetTypeId);
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
            IChoice choiceBuilder = this.CreateChoiceBuilder(ticket.BetTypeId);
            Choice_Template choice = choiceBuilder.Render(ticket, ticketHelper, ticketData, isShowScoreMap);

            return choice;
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
            this._content.Append(string.Join(null, new string[] { openTag, odds, closeTag }));
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
            IOdds oddsBuilder = this.CreateOddsBuilder(ticket.BetTypeId);
            string odds = oddsBuilder.Render(ticket, ticketData, funcGetOddsTypeName);

            return odds;
        }

        /// <summary>
        /// Adds the stake.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="openTag">The open tag.</param>
        /// <param name="closeTag">The close tag.</param>
        /// <returns>Current BetListHTMLBuilder object.</returns>
        public BetListHTMLBuilder AddStake(ITicket ticket, string openTag, string closeTag)
        {
            string stake = BuildStake(ticket);

            this._content.Append(string.Join(null, new string[] { openTag, stake, closeTag }));
            return this;
        }

        /// <summary>
        /// Builds the stake.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>System.String: the HTML markup of Stake for input data.</returns>
        public string BuildStake(ITicket ticket)
        {
            IStake stakeBuilder = this.CreateStakeBuilder(ticket.BetTypeId);
            string stake = stakeBuilder.Render(ticket);

            return stake;
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
            this._content.Append(string.Join(null, new string[] { openTag, trans, closeTag }));
            return this;
        }

        /// <summary>
        /// Builds the trans.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>System.String: the HTML markup of Trans for input data.</returns>
        public string BuildTrans(ITicket ticket)
        {
            ITrans transBuilder = this.CreateTransBuilder(ticket.BetTypeId);
            string trans = transBuilder.Render(ticket);
            return trans;
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
            this._content.Append(string.Join(null, new string[] { openTag, status, closeTag }));
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
            IStatus statusBuilder = this.CreateStatusBuilder(ticket.BetTypeId);
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

        /// <summary>
        /// Creates the choice builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IChoice: the Choice builder object.</returns>
        private IChoice CreateChoiceBuilder(int bettype)
        {
            string choiceName = ".Choice";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                choiceName = "._3rd.Choice";
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Keno games.
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { CHOICEBUILDERNAMESPACE, choiceName, bettype.ToString().Replace('-', '_') });

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                return new Choice1();
            }
            else
            {
                return Activator.CreateInstance(type) as IChoice;
            }
        }

        /// <summary>
        /// Creates the odd builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IOdds: the Odds builder object.</returns>
        private IOdds CreateOddsBuilder(int bettype)
        {
            string oddsName = ".Odds";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                oddsName = "._3rd.Odds";
            }
            else if (bettype <= 1199 && bettype >= 1101)
            {
                // Odds of Live Casino bettypes
                bettype = 1101;
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Odds of Keno games
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { ODDSBUILDERNAMESPACE, oddsName, bettype.ToString().Replace('-', '_') });

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                return new Odds1();
            }
            else
            {
                return Activator.CreateInstance(type) as IOdds;
            }
        }

        /// <summary>
        /// Creates the stake builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IStake: the Stake builder object.</returns>
        private IStake CreateStakeBuilder(int bettype)
        {
            string stakeName = ".Stake";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                stakeName = "._3rd.Stake";
            }

            string typeName = string.Join(null, new string[] { STAKEBUILDERNAMESPACE, stakeName, bettype.ToString().Replace('-', '_') });

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                return new Stake1();
            }
            else
            {
                return Activator.CreateInstance(type) as IStake;
            }
        }

        /// <summary>
        /// Creates the status builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IStatus: the Status builder object.</returns>
        private IStatus CreateStatusBuilder(int bettype)
        {
            string statusName = ".Status";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                statusName = "._3rd.Status";
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Keno games
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { STATUSBUILDERNAMESPACE, statusName, bettype.ToString().Replace('-', '_') });

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                return new Status1();
            }
            else
            {
                return Activator.CreateInstance(type) as IStatus;
            }
        }

        /// <summary>
        /// Creates the trans builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>ITrans: the Trans builder object.</returns>
        private ITrans CreateTransBuilder(int bettype)
        {
            string transName = ".Trans";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                transName = "._3rd.Trans";
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Keno games
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { TRANSBUILDERNAMESPACE, transName, bettype.ToString().Replace('-', '_') });

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                return new Trans1();
            }
            else
            {
                return Activator.CreateInstance(type) as ITrans;
            }
        }
    }
}