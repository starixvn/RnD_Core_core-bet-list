namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using System.Text;
    using Entities;
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

        protected override void BuildStake(RTFHelper rtfHelper)
        {            
            Template.stake = Template.stake.Replace(ConvertByBetType.FontOpenTag, string.Empty);
            Template.stake = Template.stake.Replace(ConvertByBetType.FontCloseTag, "\n");
            Template.stake = Template.stake.Replace("</div>", "\n");
            Template.stake = Template.stake.Replace("<div>", "\n");

            base.BuildStake(rtfHelper);
        }
    }
}