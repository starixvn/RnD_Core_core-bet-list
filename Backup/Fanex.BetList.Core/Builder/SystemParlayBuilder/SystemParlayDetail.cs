namespace Fanex.BetList.Core.Builder.SystemParlayBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using App_GlobalResources;
    using Entities;
    using Templates;
    using Utils;

    /// <summary>
    /// Build html for system parlay ticket detail.
    /// </summary>
    public class SystemParlayDetail : ISystemParlayDetail
    {
        private SystemParlayDetail_Template _template;

        private ISystemParlayData _systemParlayData;

        private ITicketHelper _ticketHelper;

        private IList<ISystemParlayTicket> _ticketList;

        private IList<ISystemParlaySerial> _serialList;

        #region ISystemParlayDetail Members

        /// <summary>
        /// Build html.
        /// </summary>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="systemParlayData">System Parlay Data.</param>
        /// <returns>The html string is returned to client.</returns>
        public virtual string Build(ITicketHelper ticketHelper, ISystemParlayData systemParlayData)
        {
            _ticketHelper = ticketHelper;
            _systemParlayData = systemParlayData;

            PrepareTemplate();
            PrepareTicketListAndSerialList();
            RenderRows();
            RenderTotalRows();
            RenderOutstanding();

            return _template.ToString();
        }

        #endregion ISystemParlayDetail Members

        #region Utlity

        protected void PrepareTemplate()
        {
            _template = new SystemParlayDetail_Template();
            _template.Contents.Clear();
            _template.SubTotalContents.Clear();
        }

        protected void PrepareTicketListAndSerialList()
        {
            _ticketList = _systemParlayData.TicketList;
            _serialList = _systemParlayData.SerialList;
        }

        protected void RenderOutstanding()
        {
            _template.Outstanding.Outstanding = Formatter.HighlightNegativeNumber(_systemParlayData.Outstanding);
        }

        protected void RenderTotalRows()
        {
            var totalPlayerCommission = _ticketHelper.ShowSystemParlayPlayerComm ? _ticketList.Sum(d => d.PlayerComm) : 0;
            var totalWinLost = _ticketList.Sum(d => d.WinLost) + totalPlayerCommission;
            _template.TotalContents.Visible = _ticketList.Count > 0;
            _template.TotalContents.TotalWinloss = Formatter.HighlightNegativeNumber(totalWinLost);
        }

        /// <summary>
        /// Renders the rows.
        /// </summary>
        protected void RenderRows()
        {
            var dateList = _ticketList.Select(d => d.WinLostDate)
                        .Distinct()
                        .OrderBy(d => d)
                        .ToList();

            foreach (var date in dateList)
            {
                RenderOneDay(date);
            }

            RenderEmptyRowInCaseHaveNoData();
        }

        /// <summary>
        /// Render one day block.
        /// </summary>
        /// <param name="date">Date value.</param>
        protected void RenderOneDay(DateTime date)
        {
            var dataInDate = _ticketList.Where(d => d.WinLostDate == date).ToList();
            var dataCount = dataInDate.Count;

            for (var j = 0; j < dataCount; j++)
            {
                RenderEvent(dataInDate, j);
            }

            RenderSubTotalForOneDay(dataInDate);
        }

        /// <summary>
        /// Renders the event.
        /// </summary>
        /// <param name="dataInDate">The data in date.</param>
        /// <param name="eventOrderInDay">The event order in day.</param>
        protected void RenderEvent(List<ISystemParlayTicket> dataInDate, int eventOrderInDay)
        {
            var dataInRow = dataInDate[eventOrderInDay];
            var rowBlock = new SystemParlayDetail_Contents_Block();

            rowBlock.WinlossDate.RowSpan = dataInDate.Count.ToString();
            rowBlock.WinlossDate.WinlossDate = string.Format("{0:MM/dd/yyyy}", dataInRow.WinLostDate);
            rowBlock.WinlossDate.Visible = eventOrderInDay == 0;
            rowBlock.Status = GetBetListStatusName(dataInRow.StatusId);
            rowBlock.Winloss = Formatter.HighlightNegativeNumber(dataInRow.WinLost);
            rowBlock.Odds = dataInRow.MOdds == 0 ? "-" : Formatter.HighlightNegativeNumber(dataInRow.MOdds);
            rowBlock.Stake = Formatter.DecFormat(dataInRow.Stake, 2);
            rowBlock.PlayerCommission.Visible = _ticketHelper.ShowSystemParlayPlayerComm;
            rowBlock.PlayerCommission.PlayerCommission = Formatter.DecFormat(dataInRow.PlayerComm, 2);

            // Event
            RenderMatchBlock(rowBlock, dataInRow.Serial);

            _template.Contents.Append(rowBlock);
        }

        /// <summary>
        /// Render the match block.
        /// </summary>
        /// <param name="rowBlock">Row block.</param>
        /// <param name="serial">Serial ID.</param>
        protected void RenderMatchBlock(SystemParlayDetail_Contents_Block rowBlock, long serial)
        {
            rowBlock.Match.Clear();
            var serialDataInOneDay = _serialList.Where(d => d.Serial == serial).ToList();
            if (serialDataInOneDay.Count == 0)
            {
                rowBlock.Match.SetValue(CoreBetList.losingtickets);
                return;
            }

            foreach (var serialData in serialDataInOneDay)
            {
                var matchBlock = new SystemParlayDetail_Contents_Match_Block();
                matchBlock.HomeTeam = _ticketHelper.GetTeamNameById(serialData.HomeId);
                matchBlock.AwayTeam = _ticketHelper.GetTeamNameById(serialData.AwayId);
                rowBlock.Match.Append(matchBlock);
            }
        }

        protected void RenderSubTotalForOneDay(List<ISystemParlayTicket> dataInDate)
        {
            var subTotalBlock = new SystemParlayDetail_SubTotalContents_Block();
            var subTotalPlayerCommission = _ticketHelper.ShowSystemParlayPlayerComm ? dataInDate.Sum(d => d.PlayerComm) : 0;
            var subTotal = dataInDate.Sum(d => d.WinLost) + subTotalPlayerCommission;

            subTotalBlock.TotalWinloss = Formatter.HighlightNegativeNumber(subTotal);
            _template.Contents.Append(subTotalBlock);
        }

        /// <summary>
        /// Renders the empty row in case have no data.
        /// </summary>
        protected void RenderEmptyRowInCaseHaveNoData()
        {
            if (_ticketList.Count > 0)
            {
                return;
            }

            var emptycontent = new SystemParlayDetail_Contents_Block
            {
                Stake = string.Empty,
                Winloss = string.Empty,
                WinlossDate = new SystemParlayDetail_Contents_WinlossDate_Block { WinlossDate = string.Empty },
                Odds = string.Empty,
                Status = string.Empty,
                PlayerCommission = null
            };

            emptycontent.Match.Hide();
            _template.Contents.Append(emptycontent);
        }

        /// <summary>
        /// Get bet list status name.
        /// </summary>
        /// <param name="status">Status code.</param>
        /// <returns>Status name in string.</returns>
        protected string GetBetListStatusName(int status)
        {
            switch (status)
            {
                case 112:
                    return CoreBetList.lblWon;
                case 113:
                    return CoreBetList.lblLose;
                case 111:
                    return CoreBetList.lblDraw;
                case 0:
                    return CoreBetList.lblRunning;
                case 1:
                    return CoreBetList.lblWaiting;
                case 101:
                    return CoreBetList.reject;
                case 102:
                    return CoreBetList.voided;
                case 103:
                    return CoreBetList.refund;
            }

            return string.Empty;
        }

        #endregion Utlity
    }
}