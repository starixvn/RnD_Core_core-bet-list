namespace Fanex.BetList.ColossusBets.Builder
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.BetList.ColossusBets.Templates;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Resources;
    using Fanex.BetList.Core.Utils;

    public static class BuilderExtension
    {
        private const int CashOutBetTypeId = 18002;

        public static string BuildColossusBetsTransactionType(int betTypeId, decimal cashOutPercent, ITicketHelper ticketHelper)
        {
            var betTypeName = ticketHelper.GetBetTypeNameById(betTypeId);

            if (betTypeId == CashOutBetTypeId
                && cashOutPercent > 0)
            {
                return string.Join(string.Empty, new string[] { betTypeName, " ", Formatter.DecFormat(cashOutPercent * 100, 0), "%" });
            }

            return betTypeName;
        }

        public static string BuildColossusBetsDetailStatus(string ticketStatus)
        {
            if (!string.IsNullOrWhiteSpace(ticketStatus))
            {
                switch (ticketStatus.ToLower())
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
                }
            }

            return string.Empty;
        }

        public static string BuildColossusBetDetails(this BetListHTMLBuilder factory, IEnumerable<ITicket> detailTickets, ITicketHelper ticketHelper)
        {
            ColossusBetsDetail_Template detailTemplate = new ColossusBetsDetail_Template();

            BuildSummaryCashOut(detailTemplate, detailTickets);
            BuildColossusBetDetailsTable(detailTemplate, detailTickets, ticketHelper);

            return detailTemplate.ToString();
        }

        private static void BuildSummaryCashOut(ColossusBetsDetail_Template detailTemplate, IEnumerable<ITicket> detailTickets)
        {
            decimal cashOutPercent = 0;
            decimal cashOutAmount = 0;

            foreach (var detailTicket in detailTickets)
            {
                if (detailTicket.BetTypeId == CashOutBetTypeId)
                {
                    cashOutPercent += detailTicket.Handicap1 * 100;
                    cashOutAmount += detailTicket.Winlost;
                }
            }

            var cashOwn = 100 - cashOutPercent;
            detailTemplate.cashOwn = string.Format(ColosussBetsLabel.YouOwnPercent, Formatter.DecFormat(cashOwn, 0));

            if (cashOutPercent == 0)
            {
                detailTemplate.cashSold = ColosussBetsLabel.YouSoldZeroPercent;
            }
            else
            {
                detailTemplate.cashSold = string.Format(
                ColosussBetsLabel.YouSoldPercent,
                Formatter.DecFormat(cashOutPercent, 0),
                BuildColossusBetsWinLoss(cashOutAmount));
            }
        }

        private static void BuildColossusBetDetailsTable(
            ColossusBetsDetail_Template detailTemplate,
            IEnumerable<ITicket> detailTickets,
            ITicketHelper ticketHelper)
        {
            decimal totalWinloss = 0;
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
            ColossusBetsDetail_Contents_Block detailContent = new ColossusBetsDetail_Contents_Block();
            detailContent.date = detailTicket.TransDate.ToString();
            detailContent.transactionType = BuildColossusBetsTransactionType(
                detailTicket.BetTypeId,
                detailTicket.Handicap1,
                ticketHelper);
            detailContent.winloss = BuildColossusBetsWinLoss(detailTicket.Winlost);
            detailContent.status = BuildColossusBetsDetailStatus(detailTicket.Status);

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
                ColossusBetsDetail_TotalContents_Block totalContent = new ColossusBetsDetail_TotalContents_Block();

                totalContent.totalWinloss = BuildColossusBetsWinLoss(totalWinloss);
                detailTemplate.TotalContents.Append(totalContent);
            }
            else
            {
                detailTemplate.NoData.Append(new ColossusBetsDetail_NoData_Block());
            }
        }

        public static string BuildColossusBetsWinLoss(decimal winloss)
        {
            string winlossFormatted = Formatter.DecFormat(winloss, 2);
            if (winloss >= 0)
            {
                return winloss == 0
                    ? "0"
                    : "<span class='positive-win-loss'>" + winlossFormatted + "</span>";
            }

            return "<span class='negative-win-loss'>" + winlossFormatted + "</span>";
        }
    }
}