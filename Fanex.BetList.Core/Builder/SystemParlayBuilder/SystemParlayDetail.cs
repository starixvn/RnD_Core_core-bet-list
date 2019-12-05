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

        protected void RenderEvent(List<ISystemParlayTicket> dataInDate, int eventOrderInDay)
        {
            var dataInRow = dataInDate[eventOrderInDay];
            var rowBlock = new SystemParlayDetail_Contents_Block();

            rowBlock.WinlossDate.RowSpan = dataInDate.Count.ToString();
            rowBlock.WinlossDate.WinlossDate = string.Format("{0:MM/dd/yyyy}", dataInRow.WinLostDate);
            rowBlock.WinlossDate.Visible = eventOrderInDay == 0;
            rowBlock.Status = GetBetListStatusName(dataInRow.StatusId);
            rowBlock.Winloss = Formatter.HighlightNegativeNumber(dataInRow.WinLost);
            rowBlock.Odds = dataInRow.MOdds == 0 ? "-" : Formatter.HighlightNegativeNumber40(dataInRow.MOdds);
            rowBlock.Stake = Formatter.DecFormat(dataInRow.Stake);
            rowBlock.PlayerCommission.Visible = _ticketHelper.ShowSystemParlayPlayerComm;
            rowBlock.PlayerCommission.PlayerCommission = Formatter.DecFormat(dataInRow.PlayerComm);

            // Event
            RenderMatchBlock(rowBlock, dataInRow.Serial);

            _template.Contents.Append(rowBlock);
        }

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

        protected string GetBetListStatusName(int status)
        {
            switch (status.ToString())
            {
                case BetStatusId.Won:
                    return CoreBetList.lblWon;

                case BetStatusId.Lost:
                    return CoreBetList.lblLose;

                case BetStatusId.Draw:
                    return CoreBetList.draw;

                case BetStatusId.Running:
                    return CoreBetList.lblRunning;

                case BetStatusId.Waiting:
                    return CoreBetList.lblWaiting;

                case BetStatusId.Reject:
                    return CoreBetList.reject;

                case BetStatusId.Void:
                    return CoreBetList.voided;

                case BetStatusId.Refund:
                    return CoreBetList.refund;

                default:
                    return string.Empty;
            }
        }

        #endregion Utlity
    }
}