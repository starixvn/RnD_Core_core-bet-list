// ---------------------------------------------------------------------------------
// <copyright file="SystemParlayDetail.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Jacob.Phan" date="2012.11.16">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.SystemParlayBuilder
{
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Entities;
    using System.Linq;
    using System;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;

    /// <summary>
    /// Build html for system parlay ticket detail
    /// </summary>
    public class SystemParlayDetail : ISystemParlayDetail
    {
        #region ISystemParlayDetail Members

        /// <summary>
        /// Build html
        /// </summary>
        /// <param name="ticketHelper"></param>
        /// <param name="systemParlayData"></param>
        /// <returns></returns>
        public string Build(ITicketHelper ticketHelper, ISystemParlayData systemParlayData)
        {
            #region Render Rows

            // Prepare the template
            var template = new SystemParlayDetail_Template();
            template.Contents.Clear();
            template.SubTotalContents.Clear();

            var ticketList = systemParlayData.TicketList;
            var serialList = systemParlayData.SerialList;

            // Loop all date
            var dateList = ticketList.Select(d => d.WinLostDate)
                        .Distinct()
                        .OrderBy(d => d)
                        .ToList();
            var dateCount = dateList.Count();

            for (var i = 0; i < dateCount; i++)
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

            #endregion

            #region Render Total Rows

            var totalWinLost = ticketList.Sum(d => d.WinLost);
            template.TotalContents.Visible = ticketList.Count > 0;
            template.TotalContents.TotalWinloss = Formatter.HighlightNegativeNumber(totalWinLost);

            #endregion

            #region Render Outstanding

            template.Outstanding.Outstanding = Formatter.HighlightNegativeNumber(systemParlayData.Outstanding);

            #endregion

            return template.ToString();
        }

        #endregion

        #region Utlity

        /// <summary>
        /// Get bet list status name
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
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
    }
}