namespace Fanex.BetList.Core.Builder
{
    using System.Collections.Generic;
    using Entities;
    using BetListNPOI.SS.UserModel;
    using BetListNPOI.SS.Util;
    using CellAddress = Fanex.BetList.Core.Entities.CellAddress;
    
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

        IBetListExcelBuilder SetColumnWidth(int columnIndex, short columnWith);

        IBetListExcelBuilder BuildWorksheet(string sheetName, short numberOfColumns, short singleRowHeight = 13, bool displayGridlines = false);

        IWorkbook GetBetList();

        CellAddress AddCell(IRichTextString rtfString);

        CellAddress AddCell(IRichTextString rtfString, ICellStyle cellStyle);

        CellAddress AddMergedCell(IRichTextString rtfString, CellRangeAddress mergedRange, ICellStyle cellStyle);

        void MergeCell(CellRangeAddress mergeRange);

        void MergeCell(CellRangeAddress mergeRange, ICellStyle cellStyle);

        IBetListExcelBuilder AddChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData);

        IBetListExcelBuilder AddOdds(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        IBetListExcelBuilder AddOddsValue(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        IBetListExcelBuilder AddOddsType(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        IBetListExcelBuilder AddStake(ITicket ticket, ITicketHelper ticketHelper);

        IBetListExcelBuilder AddTrans(ITicket ticket, ITicketHelper ticketHelper);

        IBetListExcelBuilder AddTransTime(ITicket ticket, ITicketHelper ticketHelper);

        IBetListExcelBuilder AddRefNo(ITicket ticket, ITicketHelper ticketHelper);

        IBetListExcelBuilder AddLicenseeTxId(ITicket ticket, ITicketHelper ticketHelper);

        IBetListExcelBuilder AddStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP);
    }
}