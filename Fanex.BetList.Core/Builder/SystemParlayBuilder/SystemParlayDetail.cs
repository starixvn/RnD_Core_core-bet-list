// ---------------------------------------------------------------------------------
// <copyright file="SystemParlayDetail.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.10.24">Refactor & Apply stylecop</change>
//     <change who="Jacob.Phan" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.SystemParlayBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Build html for system parlay ticket detail.
    /// </summary>
    public class SystemParlayDetail : ISystemParlayDetail
    {
        #region ISystemParlayDetail Members

        #region Utlity

        /// <summary>
        /// Get bet list status name.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns>Status string.</returns>
        public static string GetBetListStatusName(int status)
        {
            switch (status)
            {
                case 112:
                    return App_GlobalResources.CoreLang.lblWon;
                case 113:
                    return App_GlobalResources.CoreLang.lblLose;
                case 111:
                    return App_GlobalResources.CoreLang.lblDraw;
                case 0:
                    return App_GlobalResources.CoreLang.lblRunning;
                case 1:
                    return App_GlobalResources.CoreLang.lblWaiting;
                case 101:
                    return App_GlobalResources.CoreLang.reject;
                case 102:
                    return App_GlobalResources.CoreLang.voided;
                case 103:
                    return App_GlobalResources.CoreLang.refund;
            }

            return string.Empty;
        }

        #endregion

        /// <summary>
        /// Builds the specified ticket helper.
        /// </summary>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="systemParlayData">The system parlay data.</param>
        /// <returns>System.String: HTML string represents the System Parlay detail.</returns>
        public string Build(ITicketHelper ticketHelper, ISystemParlayData systemParlayData)
        {
            var ticketList = systemParlayData.TicketList;
            var serialList = systemParlayData.SerialList;

            // Loop all date.
            var dateList = ticketList.Select(d => d.WinLostDate)
                        .Distinct()
                        .OrderBy(d => d)
                        .ToList();
            var dateCount = dateList.Count();

            // Create new template & fill in row data .
            var template = this.CreateTemplateWithDetailRows(ticketList, dateList, serialList, ticketHelper);

            if (ticketList.Count == 0)
            {
                var emptycontent = new SystemParlayDetail_Contents_Block
                {
                    Stake = string.Empty,
                    Winloss = string.Empty,
                    WinlossDate = new SystemParlayDetail_Contents_WinlossDate_Block { WinlossDate = string.Empty },
                    Odds = string.Empty,
                    Status = string.Empty
                };

                emptycontent.Match.Hide();
                template.Contents.Append(emptycontent);
            }

            var totalWinLost = ticketList.Sum(d => d.WinLost);
            template.TotalContents.Visible = ticketList.Count > 0;
            template.TotalContents.TotalWinloss = Formatter.HighlightNegativeNumber(totalWinLost);
            template.Outstanding.Outstanding = Formatter.HighlightNegativeNumber(systemParlayData.Outstanding);
            return template.ToString();
        }

        /// <summary>
        /// Creates the template with detail rows.
        /// </summary>
        /// <param name="ticketList">The ticket list.</param>
        /// <param name="dateList">The date list.</param>
        /// <param name="serialList">The serial list.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <returns>SystemParlayDetail_Template object.</returns>
        private SystemParlayDetail_Template CreateTemplateWithDetailRows(
                    IList<ISystemParlayTicket> ticketList, 
                    IList<DateTime> dateList, 
                    IList<ISystemParlaySerial> serialList, 
                    ITicketHelper ticketHelper)
        {
            var template = this.CreateTemplate();
            for (var i = 0; i < dateList.Count; i++)
            {
                var dataInDate = ticketList.Where(d => d.WinLostDate == dateList[i]).ToList();
                var rowspan = dataInDate.Count();
                for (var j = 0; j < rowspan; j++)
                {
                    var row = dataInDate[j];

                    // Rows
                    var rowBlock = new SystemParlayDetail_Contents_Block();
                    rowBlock.WinlossDate.RowSpan = rowspan.ToString();
                    rowBlock.WinlossDate.WinlossDate = string.Format("{0:MM/dd/yyyy}", row.WinLostDate);
                    rowBlock.WinlossDate.Visible = j == 0;
                    rowBlock.Status = GetBetListStatusName(row.StatusId);
                    rowBlock.Winloss = Formatter.HighlightNegativeNumber(row.WinLost);
                    rowBlock.Odds = row.MOdds == 0 ? "-" : Formatter.DecFormat(row.MOdds, 2);
                    rowBlock.Stake = Formatter.DecFormat(row.Stake, 2);

                    // Event
                    rowBlock.Match.Clear();
                    var eventDataRows = serialList.Where(d => d.Serial == row.Serial).ToList();
                    if (eventDataRows.Count > 0)
                    {
                        foreach (var eventRow in eventDataRows)
                        {
                            var matchBlock = new SystemParlayDetail_Contents_Match_Block();
                            matchBlock.HomeTeam = ticketHelper.GetTeamNameById(eventRow.HomeId);
                            matchBlock.AwayTeam = ticketHelper.GetTeamNameById(eventRow.AwayId);
                            rowBlock.Match.Append(matchBlock);
                        }
                    }
                    else
                    {
                        rowBlock.Match.SetValue(App_GlobalResources.CoreLang.TotalLosingTicket);
                    }

                    template.Contents.Append(rowBlock);
                }

                var subTotalBlock = new SystemParlayDetail_SubTotalContents_Block();
                var subTotal = dataInDate.Sum(d => d.WinLost);
                subTotalBlock.TotalWinloss = Formatter.HighlightNegativeNumber(subTotal);
                template.Contents.Append(subTotalBlock);
            }

            return template;
        }

        /// <summary>
        /// Creates the template.
        /// </summary>
        /// <returns>SystemParlayDetail_Template object.</returns>
        private SystemParlayDetail_Template CreateTemplate()
        {
            var template = new SystemParlayDetail_Template();
            template.Contents.Clear(); 
            template.SubTotalContents.Clear();

            return template;
        }

        #endregion
    }
}