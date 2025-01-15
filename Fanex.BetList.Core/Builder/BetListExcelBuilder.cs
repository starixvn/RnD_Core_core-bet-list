namespace Fanex.BetList.Core.Builder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ChoiceBuilder;
    using Entities;
    using BetListNPOI.HSSF.UserModel;
    using BetListNPOI.SS.UserModel;
    using BetListNPOI.SS.Util;
    using OddsBuilder;
    using StakeBuilder;
    using StatusBuilder;
    using TransBuilder;
    using Utils;
    using Utils.NPOIExt;
    using Utils.StringExt;
    using CellAddress = Fanex.BetList.Core.Entities.CellAddress;
    public class BetListExcelBuilder : BetListBaseBuilder, IBetListExcelBuilder, IDisposable
    {
        #region Attributes

        private IWorkbook _workbook;
        private short _reportColumnsNum;
        private ISheet _worksheet;
        private bool _isDisposed;
        private int _nextCellIndex;

        #endregion Attributes

        #region Constructors

        public BetListExcelBuilder()
        {
            _workbook = new HSSFWorkbook();

            _isDisposed = false;

            OddCellStyle = _workbook.CreateCellStyle();
            EvenCellStyle = _workbook.CreateCellStyle();
            OddCellStyleCenterAligned = _workbook.CreateCellStyle();
            EvenCellStyleCenterAligned = _workbook.CreateCellStyle();

            _nextCellIndex = -1;

            RTFHelper = new RTFHelper
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

        public ICellStyle OddCellStyle { get; set; }

        public ICellStyle EvenCellStyle { get; set; }

        public ICellStyle OddCellStyleCenterAligned { get; set; }

        public ICellStyle EvenCellStyleCenterAligned { get; set; }

        public RTFHelper RTFHelper { get; set; }

        public int LastContentRow
        {
            get
            {
                return _worksheet.LastRowNum;
            }
        }

        #endregion Properties

        #region IDisposable's Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable's Implementation

        public ICellStyle CreateCellStyle()
        {
            return _workbook.CreateCellStyle();
        }

        public IFont CreateFont()
        {
            return _workbook.CreateFont();
        }

        public IBetListExcelBuilder SetColumnWidth(int columnIndex, short columnWith)
        {
            _worksheet.SetColumnWidth(columnIndex, columnWith);
            return this;
        }

        public IBetListExcelBuilder BuildWorksheet(string sheetName, short numberOfColumns, short singleRowHeight = 13, bool displayGridlines = false)
        {
            _worksheet = _workbook.CreateSheet(sheetName);
            _worksheet.DisplayGridlines = displayGridlines;
            _worksheet.DefaultRowHeight = singleRowHeight;
            _reportColumnsNum = numberOfColumns;
            return this;
        }

        public IWorkbook GetBetList()
        {
            AdaptRowHeight();
            return _workbook;
        }

        public CellAddress AddCell(IRichTextString rtfString)
        {
            _nextCellIndex++;
            IRow lastRow = GetLastRow();

            if (_nextCellIndex >= _reportColumnsNum)
            {
                _nextCellIndex = 0;
            }

            ICell lastCell;

            lastCell = IsEven(lastRow.RowNum)
                ? lastRow.CreateCell(_nextCellIndex, EvenCellStyle)
                : lastRow.CreateCell(_nextCellIndex, OddCellStyle);

            lastCell.SetCellValue(rtfString);

            return new CellAddress(lastCell.RowIndex, _nextCellIndex);
        }

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

        public void MergeCell(CellRangeAddress mergeRange)
        {
            _worksheet.AddMergedRegion(mergeRange);
            _nextCellIndex += mergeRange.LastColumn - mergeRange.FirstColumn;
        }

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

        public IBetListExcelBuilder AddChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            IChoice choiceBuilder = CreateChoiceBuilder(ticket.BetTypeId);
            List<IRichTextString> rtfChoiceList = choiceBuilder.RenderRTF(ticket, ticketHelper, ticketData, false, RTFHelper);

            foreach (IRichTextString rtfString in rtfChoiceList)
            {
                AddCell(rtfString);
            }

            return this;
        }

        public IBetListExcelBuilder AddOdds(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            IOdds oddsBuilder = CreateOddsBuilder(ticket.BetTypeId);
            IRichTextString rtfOdds = oddsBuilder.RenderRTF(ticket, ticketData, funcGetOddsTypeName, RTFHelper);
            AddCell(rtfOdds);
            return this;
        }

        public IBetListExcelBuilder AddOddsValue(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            IOdds oddsBuilder = CreateOddsBuilder(ticket.BetTypeId);
            var oddsValueExcel = oddsBuilder.RenderOddsValueRTF(ticket, ticketData, funcGetOddsTypeName, RTFHelper);
            AddCell(oddsValueExcel);

            return this;
        }

        public IBetListExcelBuilder AddOddsType(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            IOdds oddsBuilder = CreateOddsBuilder(ticket.BetTypeId);
            var oddsTypeExcel = oddsBuilder.RenderOddsTypeRTF(ticket, ticketData, funcGetOddsTypeName, RTFHelper);
            AddCell(oddsTypeExcel);

            return this;
        }

        public IBetListExcelBuilder AddStake(ITicket ticket, ITicketHelper ticketHelper)
        {
            IStake stakeBuilder = CreateStakeBuilder(ticket.BetTypeId);
            IRichTextString rtfStake = stakeBuilder.RenderRTF(ticket, ticketHelper, RTFHelper);
            AddCell(rtfStake);

            return this;
        }

        public IBetListExcelBuilder AddTrans(ITicket ticket, ITicketHelper ticketHelper)
        {
            ITrans transBuilder = CreateTransBuilder(ticket.BetTypeId);
            IRichTextString rtfTrans = transBuilder.RenderRTF(ticket, RTFHelper);

            AddCell(rtfTrans, IsEven(ticketHelper.Index) ? OddCellStyleCenterAligned : EvenCellStyleCenterAligned);
            return this;
        }

        public IBetListExcelBuilder AddTransTime(ITicket ticket, ITicketHelper ticketHelper)
        {
            ITrans transBuilder = CreateTransBuilder(ticket.BetTypeId);
            IRichTextString rtfTrans = transBuilder.RenderTransTimeRTF(ticket, RTFHelper);
            AddCell(rtfTrans, IsEven(ticketHelper.Index) ? OddCellStyleCenterAligned : EvenCellStyleCenterAligned);

            return this;
        }

        public IBetListExcelBuilder AddRefNo(ITicket ticket, ITicketHelper ticketHelper)
        {
            ITrans transBuilder = CreateTransBuilder(ticket.BetTypeId);
            IRichTextString rtfRefNo = transBuilder.RenderRefNoRTF(ticket, RTFHelper);
            AddCell(rtfRefNo, IsEven(ticketHelper.Index) ? OddCellStyleCenterAligned : EvenCellStyleCenterAligned);

            return this;
        }

        public IBetListExcelBuilder AddLicenseeTxId(ITicket ticket, ITicketHelper ticketHelper)
        {
            ITrans transBuilder = CreateTransBuilder(ticket.BetTypeId);
            IRichTextString rtfLicenseeTxId = transBuilder.RenderLicenseeTxIdRTF(ticket, RTFHelper);
            AddCell(rtfLicenseeTxId, IsEven(ticketHelper.Index) ? OddCellStyleCenterAligned : EvenCellStyleCenterAligned);

            return this;
        }

        public IBetListExcelBuilder AddStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP)
        {
            IStatus statusBuilder = CreateStatusBuilder(ticket.BetTypeId);
            IRichTextString rtfStatus = statusBuilder.RenderRTF(ticket, ticketHelper, ticketData, isShowVNIP, RTFHelper);
            AddCell(rtfStatus, IsEven(ticketHelper.Index) ? OddCellStyleCenterAligned : EvenCellStyleCenterAligned);
            return this;
        }

        private IRow GetLastRow()
        {
            IRow lastRow = _worksheet.GetRow(_worksheet.LastRowNum);
            if (lastRow == null || lastRow.Cells.Count == _reportColumnsNum || _nextCellIndex >= _reportColumnsNum)
            {
                lastRow = AddNewRow();
            }

            return lastRow;
        }

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

        private bool IsSheetHasRows()
        {
            return _worksheet.GetRow(_worksheet.LastRowNum) != null;
        }

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

        private void InitDefaultRTFHelper()
        {
            const string fontName = "Tahoma";
            const short fontSize = 9;

            RTFHelper.PosFont.Boldweight = (short)FontBoldWeight.Bold;
            RTFHelper.PosFontCrossed.Boldweight = (short)FontBoldWeight.Bold;

            RTFHelper.NegFont.Boldweight = (short)FontBoldWeight.Bold;
            RTFHelper.NegFontCrossed.Boldweight = (short)FontBoldWeight.Bold;

            RTFHelper.NormalFont.Boldweight = (short)FontBoldWeight.Normal;
            RTFHelper.NormalFontCrossed.Boldweight = (short)FontBoldWeight.Normal;

            RTFHelper.NegFont.FontName = fontName;
            RTFHelper.NegFontCrossed.FontName = fontName;
            RTFHelper.NormalFont.FontName = fontName;
            RTFHelper.NormalFontCrossed.FontName = fontName;
            RTFHelper.PosFont.FontName = fontName;
            RTFHelper.PosFontCrossed.FontName = fontName;

            RTFHelper.NegFont.FontHeightInPoints = fontSize;
            RTFHelper.NegFontCrossed.FontHeightInPoints = fontSize;
            RTFHelper.NormalFont.FontHeightInPoints = fontSize;
            RTFHelper.NormalFontCrossed.FontHeightInPoints = fontSize;
            RTFHelper.PosFont.FontHeightInPoints = fontSize;
            RTFHelper.PosFontCrossed.FontHeightInPoints = fontSize;

            RTFHelper.NormalFontCrossed.IsStrikeout = true;
            RTFHelper.PosFontCrossed.IsStrikeout = true;
            RTFHelper.NegFontCrossed.IsStrikeout = true;

            RTFHelper.NegFont.Color = (short)FontColor.Red;
            RTFHelper.NegFontCrossed.Color = (short)FontColor.Red;
        }

        private void InitDefaultCellStyles()
        {
            const BorderStyle borderStyle = BorderStyle.Thin;
            const short borderColorCode = 54;
            const short oddCellForegroundColorCode = 26;
            const short oddCellBackgroundColorCode = 43;
            const short evenCellForegroundColorCode = 9;
            const short evenCellBackgroundColorCode = 27;

            OddCellStyle.Alignment = HorizontalAlignment.Right;
            EvenCellStyle.Alignment = HorizontalAlignment.Right;

            // There's probably a bug in the VerticalAlignment enum of NPOI 2.0.6.0,
            // where VerticalAlignment.Top is actually resulting in middle alignment,
            // and VerticalAlignment.Bottom is in fact top alignment.
            OddCellStyle.VerticalAlignment = VerticalAlignment.Center;
            EvenCellStyle.VerticalAlignment = VerticalAlignment.Center;
            OddCellStyle.FillPattern = FillPattern.SolidForeground;
            EvenCellStyle.FillPattern = FillPattern.SolidForeground;

            OddCellStyle.BorderTop = borderStyle;
            OddCellStyle.BorderRight = borderStyle;
            OddCellStyle.BorderBottom = borderStyle;
            OddCellStyle.BorderLeft = borderStyle;

            EvenCellStyle.BorderTop = borderStyle;
            EvenCellStyle.BorderRight = borderStyle;
            EvenCellStyle.BorderBottom = borderStyle;
            EvenCellStyle.BorderLeft = borderStyle;

            OddCellStyle.TopBorderColor = borderColorCode;
            OddCellStyle.RightBorderColor = borderColorCode;
            OddCellStyle.BottomBorderColor = borderColorCode;
            OddCellStyle.LeftBorderColor = borderColorCode;
            EvenCellStyle.TopBorderColor = borderColorCode;
            EvenCellStyle.RightBorderColor = borderColorCode;
            EvenCellStyle.BottomBorderColor = borderColorCode;
            EvenCellStyle.LeftBorderColor = borderColorCode;

            OddCellStyle.FillForegroundColor = oddCellForegroundColorCode;
            OddCellStyle.FillBackgroundColor = oddCellBackgroundColorCode;

            EvenCellStyle.FillForegroundColor = evenCellForegroundColorCode;
            EvenCellStyle.FillBackgroundColor = evenCellBackgroundColorCode;

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

        private static bool IsEven(int rowNumber)
        {
            const int even = 2;

            return rowNumber % even == 0;
        }

        #region IDisposable's Implementation

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (!disposing && _workbook != null)
                {
                    _workbook = null;
                }

                _isDisposed = true;
            }
        }

        ~BetListExcelBuilder()
        {
            Dispose(false);
        }

        #endregion IDisposable's Implementation
    }
}