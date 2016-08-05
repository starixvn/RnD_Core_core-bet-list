namespace Fanex.BetList.Example.MVC.ViewModel
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class BetListData
    {
        public IList<ITicket> Tickets
        {
            get;
            set;
        }

        public List<ITicketData> TicketData
        {
            get;
            set;
        }

        public int BetTypeId { get; set; }

        public string Language { get; set; }

        public string Name { get; set; }
    }
}