namespace Fanex.BetList.Example.Extension.BetListExcelBuilderExt
{
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using BetListNPOI.HSSF.UserModel;
    using BetListNPOI.SS.UserModel;

    public static class BetListExcelBuilderExt
    {
        /// <summary>
        /// Adds the no.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="index">The index.</param>
        /// <returns>BetListExcelBuilder object.</returns>
        public static BetListExcelBuilder AddNo(this BetListExcelBuilder factory, int index)
        {
            IRichTextString rtfNo = new HSSFRichTextString(index.ToString());
            factory.AddCell(rtfNo);
            return factory;
        }

        /// <summary>
        /// Adds the member.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <returns>BetListExcelBuilder object.</returns>
        public static BetListExcelBuilder AddUsername(this BetListExcelBuilder factory, ITicket ticket, ITicketHelper ticketHelper)
        {
            IRichTextString rtfMember = new HSSFRichTextString(ticket.UserName);
            factory.AddCell(rtfMember, ticketHelper.Index % 2 == 0 ? factory.OddCellStyleCenterAligned : factory.EvenCellStyleCenterAligned);

            return factory;
        }
    }
}