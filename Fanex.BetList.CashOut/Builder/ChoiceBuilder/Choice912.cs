namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using App_GlobalResources;

    public class Choice912 : Choice911
    {
        protected override string BuildCashOutLabel(string originalTicketRefNo)
            => CoreBetList.BoughtLabel + " " + originalTicketRefNo;
    }
}