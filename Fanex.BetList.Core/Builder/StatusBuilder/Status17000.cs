using System.Collections.Generic;
using Fanex.BetList.Core.Entities;

namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    public class Status17000 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
            => Template.StatusResult.Hide();
    }
}