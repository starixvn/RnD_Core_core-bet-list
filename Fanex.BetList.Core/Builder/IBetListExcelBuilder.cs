namespace Fanex.BetList.Core.Builder
{
    using System.Collections.Generic;
    using Entities;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;

    public interface IBetListExcelBuilder : IBetListBaseBuilder
    {
        ICellStyle OddCellStyle { get; set; }

        ICellStyle EvenCellStyle { get; set; }

        ICellStyle OddCellStyleCenterAligned { get; set; }

        ICellStyle EvenCellStyleCenterAligned { get; set; }

        RTFHelper RTFHelper { get; set; }

        int LastContentRow { get; }

        ICellStyle CreateCellStyle();

        IFont CreateFont();

        BetListExcelBuilder SetColumnWidth(int columnIndex, short columnWith);

        BetListExcelBuilder BuildWorksheet(string sheetName, short numberOfColumns, short singleRowHeight = 13, bool displayGridlines = false);

        IWorkbook GetBetList();

        CellAddress AddCell(IRichTextString rtfString);

        CellAddress AddCell(IRichTextString rtfString, ICellStyle cellStyle);

        CellAddress AddMergedCell(IRichTextString rtfString, CellRangeAddress mergedRange, ICellStyle cellStyle);

        void MergeCell(CellRangeAddress mergeRange);

        void MergeCell(CellRangeAddress mergeRange, ICellStyle cellStyle);

        BetListExcelBuilder AddChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData);

        BetListExcelBuilder AddOdds(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        BetListExcelBuilder AddOddsValue(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        BetListExcelBuilder AddOddsType(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        BetListExcelBuilder AddStake(ITicket ticket, ITicketHelper ticketHelper);

        BetListExcelBuilder AddTrans(ITicket ticket, ITicketHelper ticketHelper);

        BetListExcelBuilder AddTransTime(ITicket ticket, ITicketHelper ticketHelper);

        BetListExcelBuilder AddRefNo(ITicket ticket, ITicketHelper ticketHelper);

        BetListExcelBuilder AddLicenseeTxId(ITicket ticket, ITicketHelper ticketHelper);

        BetListExcelBuilder AddStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP);
    }
}