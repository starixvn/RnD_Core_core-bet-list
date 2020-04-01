namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using System.Text;
    using Entities;
    using NPOI.SS.UserModel;
    using Utils;

    public class Stake5001 : Stake1
    {
        protected override void BuildStake(ITicket ticket)
        {
            var stake = $"<div>{ConvertByBetType.Stake(ticket.Stake)}</div>";
            var actualStake = $"<div>{ConvertByBetType.Stake(ticket.ActualStake)}</div>";
            var discountValue = ticket.ActualStake - ticket.Stake;
            var discountFormat = discountValue < 0 ?
                string.Format(ConvertByBetType.Minus, ConvertByBetType.Stake(discountValue)) :
                $"<div>{ConvertByBetType.Stake(discountValue)}</div>";

            var stakeSection = new StringBuilder();
            stakeSection.Append(stake);
            stakeSection.Append(discountFormat);
            stakeSection.Append(actualStake);

            Template.stake = stakeSection.ToString();
        }

        public override IRichTextString RenderRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            var stake = ConvertByBetType.Stake(ticket.Stake);
            rtfHelper.RTFRenderer.AddText(stake + "\n", rtfHelper.PosFont);

            var discountValue = ticket.ActualStake - ticket.Stake;
            var discountStr = ConvertByBetType.Stake(discountValue);
            rtfHelper.RTFRenderer.AddText(discountStr + "\n", discountValue < 0 ? rtfHelper.NegFont : rtfHelper.PosFont);

            var actualStake = ConvertByBetType.Stake(ticket.ActualStake);
            rtfHelper.RTFRenderer.AddText(actualStake + "\n", rtfHelper.PosFont);

            IRichTextString rtfStake = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();

            return rtfStake;
        }
    }
}