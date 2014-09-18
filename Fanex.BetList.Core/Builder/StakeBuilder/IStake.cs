namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;

    /// <summary>
    /// Interface IStake.
    /// </summary>
    public interface IStake
    {
        Stake_Template Template { get; set; }

        Stake_Template Render(ITicket ticket);

        IRichTextString RenderRTF(ITicket ticket, RTFHelper rtfHelper);
    }
}