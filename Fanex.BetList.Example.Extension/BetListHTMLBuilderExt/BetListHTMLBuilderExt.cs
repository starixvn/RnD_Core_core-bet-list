namespace Fanex.BetList.Example.Extension.BetListHTMLBuilderExt
{
    using Core.Builder;
    using Core.Entities;

    public static class BetListHTMLBuilderExt
    {
        /// <summary>
        /// Adds the no.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="ticket">The ticket.</param>
        /// <returns>Index string.</returns>
        public static string BuildUsername(this BetListHTMLBuilder factory, ITicket ticket)
        {
            return ticket.UserName;
        }
    }
}