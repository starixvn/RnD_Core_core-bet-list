// ---------------------------------------------------------------------------------
// <copyright file="BetListExcelBuilderExt.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.04.29">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Example.Extension.BetListExcelBuilderExt
{
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class BetListExcelBuilderExt.
    /// </summary>
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