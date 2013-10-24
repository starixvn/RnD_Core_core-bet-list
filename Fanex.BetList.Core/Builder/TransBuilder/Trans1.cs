// ---------------------------------------------------------------------------------
// <copyright file="Trans1.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.12.24">Create</change>
//     <change who="Marc.Bui" date="2013.01.24">Remove BetSlip from Trans.Time</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.TransBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Trans for most bet types.
    /// </summary>
    public class Trans1 : ITrans
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trans1" /> class.
        /// </summary>
        public Trans1()
        {
            this.Template = new Trans_Template();
        }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public Trans_Template Template { get; set; }

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns>Trans_Template: the whole Trans block.</returns>
        public virtual Trans_Template Render(ITicket ticket)
        {
            Template.transTime = ticket.TransDate.ToString();
            
            // For Mixparlay (Trans9) to reuse
            Template.TransTime.refNo = ticket.TransId.ToString();

            return Template;
        }

        public IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper)
        {
            Render(ticket);

            string refNo = Template.TransTime.refNo;

            string transTime = string.Join(null, new string[] { CoreBetList.refno, ": ", refNo });
            IRichTextString rtfTrans = new HSSFRichTextString(string.Join(null, new string[] { transTime, "\n", Template.transTime }));
            
            return rtfTrans;
        }        
    }
}