namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Entities;
    public class Choice10300 : ChoiceLC
    {
        private string ChoiceRefPrefix => "WE_BC";
        private string TableCodeRefPrefix => "WE_BT";

        public override string GetDisplayChoice(ITicketHelper ticketHelper, string choice)
            => ticketHelper.GetResourceData(ChoiceRefPrefix, choice);

        public override string GetDisplayTableCode(ITicketHelper ticketHelper, string tableCode)
            => ticketHelper.GetResourceData(TableCodeRefPrefix, tableCode);
    }
}