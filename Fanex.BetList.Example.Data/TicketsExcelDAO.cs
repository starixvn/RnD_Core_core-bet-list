namespace Fanex.BetList.Example.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using Fanex.BetList.Core.Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.POIFS.FileSystem;
    using NPOI.SS.UserModel;

    public class TicketsExcelDAO : ITicketsDAO
    {
        private readonly string _dashboardFile;
        private IWorkbook _workbook;
        private IFormulaEvaluator _formulaEvaluator;
        private DataFormatter _dataFormatter;

        public TicketsExcelDAO(string filePath)
        {
            _dashboardFile = filePath;
        }

        /// <summary>
        /// Gets the tickets.
        /// </summary>
        /// <returns>List of ITicket.</returns>
        public IList<ITicket> GetTickets()
        {
            this.LazyLoadWorkbook();

            ISheet betTicketSheet = _workbook.GetSheet("BetTickets");

            IList<ITicket> tickets = new List<ITicket>();

            if (betTicketSheet != null)
            {
                // Remove header row
                betTicketSheet.RemoveRow(betTicketSheet.GetRow(0));

                foreach (IRow row in betTicketSheet)
                {
                    ITicket ticket = this.MapRowToTicketObject(row);
                    if (ticket != null && ticket.BetTypeId > 0)
                    {
                        tickets.Add(ticket);
                    }
                }
            }

            return tickets;
        }

        /// <summary>
        /// Gets the ticket data.
        /// </summary>
        /// <returns>List of ITicketData.</returns>
        public IList<ITicketData> GetTicketData()
        {
            this.LazyLoadWorkbook();

            ISheet ticketDataSheet = _workbook.GetSheet("ReferenceData");

            IList<ITicketData> ticketData = new List<ITicketData>();

            if (ticketDataSheet != null)
            {
                // Remove header row
                ticketDataSheet.RemoveRow(ticketDataSheet.GetRow(0));

                foreach (IRow row in ticketDataSheet)
                {
                    ITicketData data = this.MapRowToTicketDataObject(row);
                    if (data != null)
                    {
                        ticketData.Add(data);
                    }
                }
            }

            return ticketData;
        }

        /// <summary>
        /// Maps the row to ticket data object.
        /// </summary>
        /// <param name="row">The IRow object.</param>
        /// <returns>ITicketData object.</returns>
        private ITicketData MapRowToTicketDataObject(IRow row)
        {
            try
            {
                ITicketData data = new TicketData();

                data.AgentPT = Convert.ToDecimal(this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.AgentPT)));
                data.BetTeam = this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.BetTeam));
                data.BetTypeId = Convert.ToInt32(this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.BetTypeId)));
                data.MasterPT = Convert.ToDecimal(this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.MasterPT)));
                data.Odds = Convert.ToDecimal(this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.Odds)));
                data.RefNo = this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.RefNo));
                data.Stake = Convert.ToDecimal(this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.Stake)));
                data.Status = this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.Status));
                data.SuperPT = Convert.ToDecimal(this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.SuperPT)));
                data.TransDesc = this.GetCellValue(row.GetCell((int)TicketDataTemplateColumnIndex.TransDesc));

                return data;
            }
            catch (Exception)
            {
            }

            return null;
        }

        /// <summary>
        /// Lazily the load workbook.
        /// </summary>
        private void LazyLoadWorkbook()
        {
            if (_workbook == null && !string.IsNullOrWhiteSpace(_dashboardFile))
            {
                _workbook = LoadWorkbook(_dashboardFile);
                _formulaEvaluator = new HSSFFormulaEvaluator(_workbook);
                _dataFormatter = new HSSFDataFormatter(new CultureInfo("en-US"));
            }
        }

        /// <summary>
        /// Maps the row to ticket object.
        /// </summary>
        /// <param name="row">The IRow object.</param>
        /// <returns>ITicket object.</returns>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        private ITicket MapRowToTicketObject(IRow row)
        {
            ITicket ticket = new Ticket();

            try
            {
                ticket.ActualStake = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.ActualStake)));
                ticket.AgentComm = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.AgentComm)));
                ticket.AgentDiscount = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.AgentDiscount)));
                ticket.AgentPositionTaking = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.AgentPositionTaking)));
                ticket.AgentWinlost = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.AgentWinlost)));
                ticket.AwayId = Convert.ToInt64(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.AwayId)));
                ticket.BetCheck = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.BetCheck));
                ticket.BetId = Convert.ToInt64(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.BetId)));
                ticket.BetTeam = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.BetTeam));
                ticket.BetTypeId = Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.BetTypeId)));
                ticket.Comm = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.Comm)));
                ticket.CommStatus = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.CommStatus));
                ticket.CustId = Convert.ToInt64(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.CustId)));
                ticket.Currency = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.Currency));
                ticket.EventDate = Convert.ToDateTime(GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.EventDate)));
                ticket.EventStatus = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.EventStatus));
                ticket.Handicap1 = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.Handicap1)));
                ticket.Handicap2 = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.Handicap2)));
                ticket.HomeId = Convert.ToInt64(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.HomeId)));
                ticket.IP = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.IP));
                ticket.IsLive = Convert.ToBoolean(Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.IsLive))));
                ticket.IsNeutral = Convert.ToBoolean(Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.IsNeutral))));
                ticket.LeagueId = Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.LeagueId)));
                ticket.LiveAwayScore = Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.LiveAwayScore)));
                ticket.LiveHomeScore = Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.LiveHomeScore)));
                ticket.MasterDiscount = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.MasterDiscount)));
                ticket.MasterOdds = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.MasterOdds)));
                ticket.MasterPositionTaking = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.MasterPositionTaking)));
                ticket.MasterWinlost = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.MasterWinlost)));
                ticket.MatchCode = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.MatchCode));
                ticket.MatchId = Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.MatchId)));
                ticket.Odds = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.Odds)));
                ticket.OddsType = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.OddsType));
                ticket.PlayerComm = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.PlayerComm)));
                ticket.Race = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.Race));
                ticket.RefNo = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.RefNo));
                ticket.ShowTime = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.ShowTime));
                ticket.SportTypeId = Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.SportTypeId)));
                ticket.Stake = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.Stake)));
                ticket.Status = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.Status));
                ticket.SuperComm = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.SuperComm)));
                ticket.SuperDiscount = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.SuperDiscount)));
                ticket.SuperPositionTaking = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.SuperPositionTaking)));
                ticket.SuperWinlost = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.SuperWinlost)));
                ticket.TransDate = Convert.ToDateTime(GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.TransDate)));
                ticket.TransId = Convert.ToInt64(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.TransId)));
                ticket.UserName = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.UserName));
                ticket.Winlost = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.Winlost)));
                ticket.WinlostDate = Convert.ToDateTime(GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.WinlostDate)));
                ticket.TransDesc = GetCellValue(row.GetCell((int)TicketTemplateColumnIndex.TransDesc));
                ticket.OddsSpread = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.OddsSpread)));
                ticket.OriginalBetType = Convert.ToInt32(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.OriginalBetType)));
                ticket.BetdaqId = Convert.ToInt64(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.BetdaqId)));
                ticket.ExtraInfo = row.GetCell((int)TicketTemplateColumnIndex.ExtraInfo).ToString();
                ticket.OriginalStake = Convert.ToDecimal(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.OriginalStake)));
                ticket.RefCode = Convert.ToInt64(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.RefCode)));
                ticket.WalletType = Convert.ToByte(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.WalletType)));
                ticket.SettlementStatus = Convert.ToByte(GetNumericValue(row.GetCell((int)TicketTemplateColumnIndex.SettlementStatus)));
            }
            catch
            {
            }

            return ticket;
        }

        /// <summary>
        /// Loads the workbook.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>IWorkbook object.</returns>
        private static IWorkbook LoadWorkbook(string filePath)
        {
            using (StreamReader input = new StreamReader(filePath))
            {
                IWorkbook workbook = new HSSFWorkbook(new POIFSFileSystem(input.BaseStream));

                return workbook;
            }
        }

        private string GetCellValue(ICell cell)
        {
            return NPOIUtil.GetValue(cell, _dataFormatter, _formulaEvaluator);
        }

        private string GetNumericValue(ICell cell)
        {
            var cellValue = NPOIUtil.GetValue(cell, _dataFormatter, _formulaEvaluator);

            return string.IsNullOrEmpty(cellValue) ? "0" : cellValue;
        }
    }
}