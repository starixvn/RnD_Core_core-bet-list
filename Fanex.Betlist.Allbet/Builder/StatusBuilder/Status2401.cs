
namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    class Status2401 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.StatusResult.Hide();
            }
        }
    }
}
