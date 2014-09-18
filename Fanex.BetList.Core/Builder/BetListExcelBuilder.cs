namespace Fanex.BetList.Core.Builder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ChoiceBuilder;
    using Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using OddsBuilder;
    using StakeBuilder;
    using StatusBuilder;
    using TransBuilder;
    using Utils;
    using Utils.NPOIExt;
    using Utils.StringExt;

    /// <summary>
    /// CoreBetList Excel Builder.
    /// </summary>
    public class BetListExcelBuilder : BetListBaseBuilder, IBetListBuilder, IDisposable
    {
        #region Attributes

        /// <summary>
        /// The workbook.
        /// </summary>
        private IWorkbook _workbook;

        /// <summary>
        /// The report columns number.
        /// </summary>
        private short _reportColumnsNum;

        /// <summary>
        /// The worksheet.
        /// </summary>
        private ISheet _worksheet;

        private bool _isDisposed;

        /// <summary>
        /// Index of the next cell in current row.
        /// </summary>
        private int _nextCellIndex;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BetListExcelBuilder" /> class.
        /// </summary>
        public BetListExcelBuilder()
        {
            _workbook = new HSSFWorkbook();

            _isDisposed = false;

            OddCellStyle = _workbook.CreateCellStyle();
            EvenCellStyle = _workbook.CreateCellStyle();
            OddCellStyleCenterAligned = _workbook.CreateCellStyle();
            EvenCellStyleCenterAligned = _workbook.CreateCellStyle();

            _nextCellIndex = -1;

            RTFHelper = new RTFHelper()
            {
                NegFont = _workbook.CreateFont(),
                NormalFont = _workbook.CreateFont(),
                PosFont = _workbook.CreateFont(),
                NegFontCrossed = _workbook.CreateFont(),
                NormalFontCrossed = _workbook.CreateFont(),
                PosFontCrossed = _workbook.CreateFont(),
                RTFRenderer = new RtfTextRender()
            };

            InitDefaultRTFHelper();
            InitDefaultCellStyles();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the odd cell style.
        /// </summary>
        /// <value>The odd cell style.</value>
        public ICellStyle OddCellStyle { get; set; }

        /// <summary>
        /// Gets or sets the even cell style.
        /// </summary>
        /// <value>The even cell style.</value>
        public ICellStyle EvenCellStyle { get; set; }

        /// <summary>
        /// Gets or sets the odd cell style center aligned.
        /// </summary>
        /// <value>The odd cell style center aligned.</value>
        public ICellStyle OddCellStyleCenterAligned { get; set; }

        /// <summary>
        /// Gets or sets the even cell style center aligned.
        /// </summary>
        /// <value>The even cell style center aligned.</value>
        public ICellStyle EvenCellStyleCenterAligned { get; set; }

        /// <summary>
        /// Gets or sets the RTF helper.
        /// </summary>
        /// <value>The RTF helper.</value>
        public RTFHelper RTFHelper { get; set; }

        /// <summary>
        /// Gets the last content row.
        /// </summary>
        /// <value>The last content row.</value>
        public int LastContentRow
        {
            get
            {
                return _worksheet.LastRowNum;
            }
        }

        #endregion Properties

        #region IDisposable's Implementation

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch
            {
                throw;
            }
        }

        #endregion IDisposable's Implementation

        /// <summary>
        /// Creates the cell style.
        /// </summary>
        /// <returns>ICellStyle: the cell style object attaching to current workbook.</returns>
        public ICellStyle CreateCellStyle()
        {
            return _workbook.CreateCellStyle();
        }

        /// <summary>
        /// Creates the font.
        /// </summary>
        /// <returns>IFont: the Font object, attaching with current workbook.</returns>
        public IFont CreateFont()
        {
            return _workbook.CreateFont();
        }

        /// <summary>
        /// Sets the width of the column.
        /// </summary>
        /// <param name="columnIndex">Index of the column.</param>
        /// <param name="columnWith">The column with.</param>
        /// <returns>Current BetListExcelBuilder object.</returns>
        public BetListExcelBuilder SetColumnWidth(int columnIndex, short columnWith)
        {
            _worksheet.SetColumnWidth(columnIndex, columnWith);
            return this;
        }

        /// <summary>
        /// Builds the worksheet.
        /// </summary>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="numberOfColumns">The number of columns.</param>
        /// <param name="singleRowHeight">Height of the single row.</param>
        /// <param name="displayGridlines">If set to <c>true</c> [display gridlines].</param>
        /// <returns>Current BetListExcelBuilder object.</returns>
        public BetListExcelBuilder BuildWorksheet(string sheetName, short numberOfColumns, short singleRowHeight = 13, bool displayGridlines = false)
        {
            _worksheet = _workbook.CreateSheet(sheetName);
            _worksheet.DisplayGridlines = displayGridlines;
            _worksheet.DefaultRowHeight = singleRowHeight;
            _reportColumnsNum = numberOfColumns;
            return this;
        }

        /// <summary>
        /// Gets the bet list.
        /// </summary>
        /// <returns>IWorkbook: current built workbook.</returns>
        public IWorkbook GetBetList()
        {
            AdaptRowHeight();
            return _workbook;
        }

        /// <summary>
        /// Adds the cell.
        /// </summary>
        /// <param name="rtfString">The RTF string.</param>
        /// <returns>CellAddress: the address object of the newly added cell.</returns>
        public CellAddress AddCell(IRichTextString rtfString)
        {
            _nextCellIndex++;
            IRow lastRow = GetLastRow();

            if (_nextCellIndex >= _reportColumnsNum)
            {
                _nextCellIndex = 0;
            }

            ICell lastCell;

            if (lastRow.RowNum % 2 == 0)
            {
                lastCell = lastRow.CreateCell(_nextCellIndex, EvenCellStyle);
            }
            else
            {
                lastCell = lastRow.CreateCell(_nextCellIndex, OddCellStyle);
            }

            lastCell.SetCellValue(rtfString);

            return new CellAddress(lastCell.RowIndex, _nextCellIndex);
        }

        /// <summary>
        /// Adds the cell.
        /// </summary>
        /// <param name="rtfString">The RTF string.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <returns>CellAddress: the address object of the newly added cell.</returns>
        public CellAddress AddCell(IRichTextString rtfString, ICellStyle cellStyle)
        {
            _nextCellIndex++;
            IRow lastRow = GetLastRow();

            if (_nextCellIndex >= _reportColumnsNum)
            {
                _nextCellIndex = 0;
            }

            ICell lastCell = lastRow.CreateCell(_nextCellIndex, cellStyle);

            lastCell.SetCellValue(rtfString);

            return new CellAddress(lastCell.RowIndex, _nextCellIndex);
        }

        /// <summary>
        /// Adds the merged cell.
        /// </summary>
        /// <param name="rtfString">The RTF string.</param>
        /// <param name="mergedRange">The merged range.</param>
        /// <param name="cellStyle">The cell style.</param>
        /// <returns>CellAddress: the address object of the newly merged cell.</returns>
        public CellAddress AddMergedCell(IRichTextString rtfString, CellRangeAddress mergedRange, ICellStyle cellStyle)
        {
            _nextCellIndex++;

            if (_nextCellIndex >= _reportColumnsNum)
            {
                _nextCellIndex = 0;
            }

            MergeCell(mergedRange);

            IRow firstRow = _worksheet.GetRow(mergedRange.FirstRow);
            ICell lastCell = firstRow.GetCell(mergedRange.FirstColumn);
            lastCell.CellStyle = cellStyle;
            lastCell.SetCellValue(rtfString);

            return new CellAddress(lastCell.RowIndex, _nextCellIndex);
        }

        /// <summary>
        /// Merges the cell.
        /// </summary>
        /// <param name="mergeRange">The merge range.</param>
        public void MergeCell(CellRangeAddress mergeRange)
        {
            _worksheet.AddMergedRegion(mergeRange);
            _nextCellIndex += mergeRange.LastColumn - mergeRange.FirstColumn;
        }

        /// <summary>
        /// Merges the cell.
        /// </summary>
        /// <param name="mergeRange">The merge range.</param>
        /// <param name="cellStyle">The cell style.</param>
        public void MergeCell(CellRangeAddress mergeRange, ICellStyle cellStyle)
        {
            _worksheet.AddMergedRegion(mergeRange);

            for (int i = mergeRange.FirstRow; i <= mergeRange.LastRow; i++)
            {
                IRow mergedRow = _worksheet.GetRow(i);
                if (mergedRow != null)
                {
                    mergedRow.CreateCell(mergeRange.LastColumn, cellStyle);
                }
            }

            _nextCellIndex += mergeRange.LastColumn - mergeRange.FirstColumn;
        }

        /// <summary>
        /// Adds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <returns>Current BetListExcelBuilder object.</returns>
        public BetListExcelBuilder AddChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            IChoice choiceBuilder = CreateChoiceBuilder(ticket.BetTypeId);
            List<IRichTextString> rtfChoiceList = choiceBuilder.RenderRTF(ticket, ticketHelper, ticketData, false, RTFHelper);

            foreach (IRichTextString rtfString in rtfChoiceList)
            {
                AddCell(rtfString);
            }

            return this;
        }

        /// <summary>
        /// Adds the odds.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="funcGetOddsTypeName">Name of the function to get odds type.</param>
        /// <returns>Current BetListExcelBuilder object.</returns>
        public BetListExcelBuilder AddOdds(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            IOdds oddsBuilder = CreateOddsBuilder(ticket.BetTypeId);
            IRichTextString rtfOdds = oddsBuilder.RenderRTF(ticket, ticketData, funcGetOddsTypeName, RTFHelper);
            AddCell(rtfOdds);
            return this;
        }

        /// <summary>
        /// Adds the stake.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>Current BetListExcelBuilder object.</returns>
        public BetListExcelBuilder AddStake(ITicket ticket)
        {
            IStake stakeBuilder = CreateStakeBuilder(ticket.BetTypeId);
            IRichTextString rtfStake = stakeBuilder.RenderRTF(ticket, RTFHelper);
            AddCell(rtfStake);

            return this;
        }

        /// <summary>
        /// Adds the trans.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <returns>Current BetListExcelBuilder object.</returns>
        public BetListExcelBuilder AddTrans(ITicket ticket, ITicketHelper ticketHelper)
        {
            ITrans transBuilder = CreateTransBuilder(ticket.BetTypeId);
            IRichTextString rtfTrans = transBuilder.RenderRTF(ticket, ticketHelper);

            AddCell(rtfTrans, ticketHelper.Index % 2 == 0 ? OddCellStyleCenterAligned : EvenCellStyleCenterAligned);
            return this;
        }

        /// <summary>
        /// Adds the status.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowVNIP">If set to <c>true</c> VNIP is shown.</param>
        /// <returns>Current BetListExcelBuilder object.</returns>
        public BetListExcelBuilder AddStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            IStatus statusBuilder = CreateStatusBuilder(ticket.BetTypeId);
            IRichTextString rtfStatus = statusBuilder.RenderRTF(ticket, ticketHelper, ticketData, isShowVNIP, RTFHelper);
            AddCell(rtfStatus, ticketHelper.Index % 2 == 0 ? OddCellStyleCenterAligned : EvenCellStyleCenterAligned);
            return this;
        }

        /// <summary>
        /// Gets the last row.
        /// </summary>
        /// <returns>IRow: last row object in the current worksheet.</returns>
        private IRow GetLastRow()
        {
            IRow lastRow = _worksheet.GetRow(_worksheet.LastRowNum);
            if (lastRow == null || lastRow.Cells.Count == _reportColumnsNum || _nextCellIndex >= _reportColumnsNum)
            {
                lastRow = AddNewRow();
            }

            return lastRow;
        }

        /// <summary>
        /// Adds the new row.
        /// </summary>
        /// <returns>IRow: new added row.</returns>
        private IRow AddNewRow()
        {
            int newRowIndex = _worksheet.LastRowNum;

            if (IsSheetHasRows())
            {
                newRowIndex += 1;
            }

            IRow newRow = _worksheet.CreateRow(newRowIndex);
            return newRow;
        }

        /// <summary>
        /// Determines whether the target sheet has rows.
        /// </summary>
        /// <returns><c>true</c> if the target sheet has rows; otherwise, <c>false</c>.</returns>
        private bool IsSheetHasRows()
        {
            return _worksheet.GetRow(_worksheet.LastRowNum) != null;
        }

        /// <summary>
        /// Adapts the width of the row.
        /// </summary>
        private void AdaptRowWidth()
        {
            for (short i = 0; i < _reportColumnsNum; i++)
            {
                _worksheet.AutoSizeColumn(i);
            }
        }

        /// <summary>
        /// Adapts the height of the row.
        /// </summary>
        private void AdaptRowHeight()
        {
            IEnumerator rowEnum = _worksheet.GetRowEnumerator();

            while (rowEnum.MoveNext())
            {
                IRow currentRow = rowEnum.Current as HSSFRow;
                int totalLineBreaks = 0;
                foreach (ICell cell in currentRow.Cells)
                {
                    totalLineBreaks = Math.Max(totalLineBreaks, cell.StringCellValue.GetLineBreakTotal());
                }

                currentRow.HeightInPoints = totalLineBreaks * _worksheet.DefaultRowHeight;
            }
        }

        /// <summary>
        /// Initialize the default RTF helper.
        /// </summary>
        private void InitDefaultRTFHelper()
        {
            RTFHelper.PosFont.Boldweight = RTFHelper.PosFontCrossed.Boldweight = (short)FontBoldWeight.Bold;
            RTFHelper.NegFont.Boldweight = RTFHelper.NegFontCrossed.Boldweight = (short)FontBoldWeight.Bold;
            RTFHelper.NormalFont.Boldweight = RTFHelper.NormalFontCrossed.Boldweight = (short)FontBoldWeight.Normal;

            RTFHelper.NegFont.FontName = RTFHelper.NegFontCrossed.FontName = RTFHelper.NormalFont.FontName = "Tahoma";
            RTFHelper.NormalFontCrossed.FontName = RTFHelper.PosFont.FontName = RTFHelper.PosFontCrossed.FontName = "Tahoma";

            RTFHelper.NegFont.FontHeightInPoints = RTFHelper.NegFontCrossed.FontHeightInPoints = RTFHelper.NormalFont.FontHeightInPoints = 9;
            RTFHelper.NormalFontCrossed.FontHeightInPoints = RTFHelper.PosFont.FontHeightInPoints = RTFHelper.PosFontCrossed.FontHeightInPoints = 9;

            RTFHelper.NormalFontCrossed.IsStrikeout = RTFHelper.PosFontCrossed.IsStrikeout = RTFHelper.NegFontCrossed.IsStrikeout = true;

            RTFHelper.NegFont.Color = RTFHelper.NegFontCrossed.Color = IndexedColors.Red.Index;
        }

        /// <summary>
        /// Initialize the default cell styles.
        /// </summary>
        private void InitDefaultCellStyles()
        {
            OddCellStyle.Alignment = EvenCellStyle.Alignment = HorizontalAlignment.Right;

            // There's probably a bug in the VerticalAlignment enum of NPOI 2.0.6.0,
            // where VerticalAlignment.Top is actually resulting in middle alignment,
            // and VerticalAlignment.Bottom is in fact top alignment.
            OddCellStyle.VerticalAlignment = EvenCellStyle.VerticalAlignment = VerticalAlignment.Bottom;
            OddCellStyle.FillPattern = EvenCellStyle.FillPattern = FillPattern.SolidForeground;

            OddCellStyle.BorderTop = OddCellStyle.BorderRight = OddCellStyle.BorderBottom = OddCellStyle.BorderLeft = BorderStyle.Thin;
            EvenCellStyle.BorderTop = EvenCellStyle.BorderRight = EvenCellStyle.BorderBottom = EvenCellStyle.BorderLeft = BorderStyle.Thin;

            OddCellStyle.TopBorderColor = OddCellStyle.RightBorderColor = OddCellStyle.BottomBorderColor = OddCellStyle.LeftBorderColor = 54;
            EvenCellStyle.TopBorderColor = EvenCellStyle.RightBorderColor = EvenCellStyle.BottomBorderColor = EvenCellStyle.LeftBorderColor = 54;

            OddCellStyle.FillForegroundColor = 26;
            OddCellStyle.FillBackgroundColor = 43;

            EvenCellStyle.FillForegroundColor = 9;
            EvenCellStyle.FillBackgroundColor = 27;

            OddCellStyle.SetFont(RTFHelper.NormalFont);
            EvenCellStyle.SetFont(RTFHelper.NormalFont);

            OddCellStyleCenterAligned.CloneStyleFrom(OddCellStyle);
            OddCellStyleCenterAligned.Alignment = HorizontalAlignment.Center;
            OddCellStyleCenterAligned.SetFont(RTFHelper.NormalFont);

            EvenCellStyleCenterAligned.CloneStyleFrom(EvenCellStyle);
            EvenCellStyleCenterAligned.Alignment = HorizontalAlignment.Center;
            EvenCellStyleCenterAligned.SetFont(RTFHelper.NormalFont);

            // For line-breaks in MS Office
            OddCellStyle.WrapText = true;
            EvenCellStyle.WrapText = true;
            EvenCellStyleCenterAligned.WrapText = true;
            OddCellStyleCenterAligned.WrapText = true;
        }

        #region IDisposable's Implementation

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>True</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (!disposing)
                {
                    if (_workbook != null)
                    {
                        _workbook = null;
                    }
                }

                _isDisposed = true;
            }
        }

        #endregion IDisposable's Implementation
    }
}