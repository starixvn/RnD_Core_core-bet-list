namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Stake for most bet types.
    /// </summary>
    public class Stake1 : IStake
    {
        public Stake1()
        {
            Template = new Stake_Template();
        }

        public Stake_Template Template { get; set; }

        public virtual Stake_Template Render(ITicket ticket)
        {
            BuildStake(ticket);

            return Template;
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            Render(ticket);
            Template.stake = Template.stake.Replace("<br/>&nbsp;", "\n");

            rtfHelper.RTFRenderer.AddText(Template.stake, rtfHelper.PosFont);
            IRichTextString rtfStake = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfStake;
        }

        protected virtual void BuildStake(ITicket ticket)
        {
            Template.stake = ConvertByBetType.Stake(ticket.Stake);
        }
    }
}