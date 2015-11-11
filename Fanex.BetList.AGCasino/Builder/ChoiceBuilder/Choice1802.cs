namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice1802 : Choice1801
    {
        private const string RouletteBetChoiceFormat = "{0} ({1})";
        private const string ValName = "VAL";
        private static readonly IList<int> SupportedTypes = new List<int> { 101, 102, 103, 104, 105, 106, 110 };

        protected override string GetBetChoiceName(
            ITicketHelper ticketHelper,
            int betTypeId,
            Dictionary<string, string> transDecsParsed)
        {
            var betChoiceName = base.GetBetChoiceName(ticketHelper, betTypeId, transDecsParsed);
            int type = 0;
            int.TryParse(transDecsParsed[Choice1801.BetChoiceJSONName], out type);

            if (SupportedTypes.Contains(type)
                && transDecsParsed.ContainsKey(ValName))
            {
                return string.Format(RouletteBetChoiceFormat, betChoiceName, transDecsParsed[ValName]);
            }

            return betChoiceName;
        }
    }
}