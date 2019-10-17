namespace Fanex.BetList.ColossusBets.Builder
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Fanex.BetList.ColossusBets.Templates;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Resources;
    using Fanex.BetList.Core.Utils;

    public static class BuilderExtension
    {
        private const int PercentageDenominator = 100;

        public static string BuildColossusBetsTransactionType(int betTypeId, decimal cashOutPercent, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(betTypeId);

            if (betTypeId == ColossusBetsBetTypes.CashOut && cashOutPercent > 0)
            {
                return $"{betTypeName} {Formatter.DecFormat(cashOutPercent * PercentageDenominator, 0)}%";
            }

            return betTypeName;
        }

        public static string BuildColossusBetsDetailStatus(string ticketStatus)
        {
            if (!string.IsNullOrWhiteSpace(ticketStatus))
            {
                switch (ticketStatus.ToLowerInvariant())
                {
                    case BetStatus.Running:
                        return CoreBetList.running;

                    case BetStatus.Reject:
                        return CoreBetList.reject;

                    case BetStatus.Won:
                        return CoreBetList.won;

                    case BetStatus.Lose:
                        return CoreBetList.lose;

                    case BetStatus.Draw:
                        return CoreBetList.draw;

                    case BetStatus.Waiting:
                        return CoreBetList.waiting;

                    case BetStatus.Void:
                        return CoreBetList.voided;

                    case BetStatus.Refund:
                        return CoreBetList.refund;

                    default:
                        return string.Empty;
                }
            }

            return string.Empty;
        }

        public static string BuildColossusBetDetails(this BetListHTMLBuilder factory, IEnumerable<ITicket> detailTickets, ITicketHelper ticketHelper)
        {
            var detailTemplate = new ColossusBetsDetail_Template();

            BuildSummaryCashOut(detailTemplate, detailTickets);
            BuildColossusBetDetailsTable(detailTemplate, detailTickets, ticketHelper);

            return detailTemplate.ToString();
        }

        private static void BuildSummaryCashOut(ColossusBetsDetail_Template detailTemplate, IEnumerable<ITicket> detailTickets)
        {
            var cashOutPercent = 0M;
            var cashOutAmount = 0M;

            foreach (var detailTicket in detailTickets)
            {
                if (detailTicket.BetTypeId == ColossusBetsBetTypes.CashOut)
                {
                    cashOutPercent += detailTicket.Handicap1 * PercentageDenominator;
                    cashOutAmount += detailTicket.Winlost;
                }
            }

            var cashOwn = 100 - cashOutPercent;
            detailTemplate.cashOwn = string.Format(ColosussBetsLabel.YouOwnPercent, Formatter.DecFormat(cashOwn, 0));

            detailTemplate.cashSold = cashOutPercent == 0
                ? ColosussBetsLabel.YouSoldZeroPercent
                : string.Format(
                    ColosussBetsLabel.YouSoldPercent,
                    Formatter.DecFormat(cashOutPercent, 0),
                    BuildColossusBetsWinLoss(cashOutAmount));
        }

        private static void BuildColossusBetDetailsTable(
            ColossusBetsDetail_Template detailTemplate,
            IEnumerable<ITicket> detailTickets,
            ITicketHelper ticketHelper)
        {
            var totalWinloss = 0M;

            detailTemplate.Contents.Clear();

            foreach (var detailTicket in detailTickets)
            {
                BuildBodyRow(detailTemplate, detailTicket, ticketHelper);

                totalWinloss += detailTicket.Winlost;
            }

            BuildTotalRow(detailTemplate, detailTickets.Any(), totalWinloss);
        }

        private static void BuildBodyRow(
            ColossusBetsDetail_Template detailTemplate,
            ITicket detailTicket,
            ITicketHelper ticketHelper)
        {
            var detailContent = new ColossusBetsDetail_Contents_Block
            {
                date = detailTicket.TransDate.ToString(CultureInfo.InvariantCulture),
                transactionType = BuildColossusBetsTransactionType(detailTicket.BetTypeId, detailTicket.Handicap1, ticketHelper),
                winloss = BuildColossusBetsWinLoss(detailTicket.Winlost),
                status = BuildColossusBetsDetailStatus(detailTicket.Status)
            };

            detailTemplate.Contents.Append(detailContent);
        }

        private static void BuildTotalRow(
            ColossusBetsDetail_Template detailTemplate,
            bool hasTicketDetails,
            decimal totalWinloss)
        {
            detailTemplate.TotalContents.Clear();
            detailTemplate.NoData.Clear();

            if (hasTicketDetails)
            {
                var totalContent = new ColossusBetsDetail_TotalContents_Block
                {
                    totalWinloss = BuildColossusBetsWinLoss(totalWinloss)
                };

                detailTemplate.TotalContents.Append(totalContent);
            }
            else
            {
                detailTemplate.NoData.Append(new ColossusBetsDetail_NoData_Block());
            }
        }

        public static string BuildColossusBetsWinLoss(decimal winloss)
        {
            var winlossFormatted = Formatter.DecFormat(winloss, 2);

            if (winloss >= 0)
            {
                return winloss == 0
                    ? "0"
                    : $"<span class='positive-win-loss'>{winlossFormatted}</span>";
            }

            return $"<span class='negative-win-loss'>{winlossFormatted}</span>";
        }
    }
}