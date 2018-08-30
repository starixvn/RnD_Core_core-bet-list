namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    public class Choice1402 : Choice1401
    {
        private readonly Dictionary<string, Func<string, ITicket, string>> gameInfoBuilders;

        private decimal _ante;

        public Choice1402()
        {
            gameInfoBuilders = new Dictionary<string, Func<string, ITicket, string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["Mode15"] = BuildBlindsInfo,
                ["Mode16"] = BuildDoubleAnteInfo,
                ["Mode17"] = BuildAnteOnlyInfo,
                ["Mode18"] = BuildBlindsAnteInfo
            };
        }

        protected override void ParseExtraInfo(XDocument extraInfoXml)
        {
            const string anteAttributeName = "at";

            decimal.TryParse(GetExtraInfo(extraInfoXml, anteAttributeName), out _ante);
            base.ParseExtraInfo(extraInfoXml);
        }

        protected override string BuildSeatAndTableBlock(ITicket ticket, ITicketHelper ticketHelper)
        {
            var seatInfo = BuildSeatInfo(ticket, ticketHelper);
            var tableInfo = BuildTableInfo(ticketHelper);
            var modeInfo = BuildModeInfo(ticket, ticketHelper);

            return $"{seatInfo}, {tableInfo} {modeInfo}";
        }

        protected string BuildModeInfo(ITicket ticket, ITicketHelper ticketHelper)
        {
            var pattern = ticketHelper.GetResourceData(GameInfoRefId, ticket.CommStatus);

            if (gameInfoBuilders.ContainsKey(ticket.CommStatus))
            {
                return gameInfoBuilders[ticket.CommStatus](pattern, ticket);
            }

            return string.Empty;
        }

        protected static string BuildBlindsInfo(string pattern, ITicket ticket)
        {
            var smallBlindAmount = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1);
            var bigBlindAmount = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1 * 2);

            return string.Format(pattern, smallBlindAmount, bigBlindAmount);
        }

        protected string BuildDoubleAnteInfo(string pattern, ITicket ticket)
        {
            var anteAmount = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1);
            var btnAnteAmount = string.Format(Formatter.DECIMALFORMAT1, _ante);

            return string.Format(pattern, anteAmount, btnAnteAmount);
        }

        protected static string BuildAnteOnlyInfo(string pattern, ITicket ticket)
        {
            var anteAmount = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1);

            return string.Format(pattern, anteAmount);
        }

        protected string BuildBlindsAnteInfo(string pattern, ITicket ticket)
        {
            var smallBlindsAmount = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1);
            var bigBlindsAmount = string.Format(Formatter.DECIMALFORMAT1, ticket.Handicap1 * 2);
            var anteAmount = string.Format(Formatter.DECIMALFORMAT1, _ante);

            return string.Format(pattern, smallBlindsAmount, bigBlindsAmount, anteAmount);
        }
    }
}