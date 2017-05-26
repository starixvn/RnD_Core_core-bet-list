namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using System;
    using App_GlobalResources;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;
    using Utils.StringExt;

    /// <summary>
    /// Stake for most bet types.
    /// </summary>
    public class Stake1 : IStake
    {
        private const string CashOutXmlTag = "p25";
        protected const sbyte CombinedWalletType = -1;
        protected const sbyte BonusWalletType = 1;
        protected const sbyte CashWalletType = 2;

        public Stake1()
        {
            Template = new Stake_Template();
        }

        public Stake_Template Template { get; set; }

        public virtual Stake_Template Render(ITicket ticket)
        {
            BuildStake(ticket);
            BuildOriginalStake(ticket);
            BuildBonus(ticket);

            return Template;
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            Render(ticket);

            BuildOriginalStakeRTF(ticket, rtfHelper);
            BuildStake(rtfHelper);
            BuildBonus(rtfHelper);

            IRichTextString rtfStake = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfStake;
        }

        protected virtual void BuildStake(ITicket ticket)
        {
            Template.stake = ConvertByBetType.Stake(ticket.Stake);
        }

        protected virtual void BuildBonus(ITicket ticket)
        {
            if (ticket.WalletType == BonusWalletType)
            {
                if (ticket.RefCode == 0)
                {
                    ShowIndicatingTextOfBonusWallet();
                }
                else
                {
                    ShowBonusIcon(ticket);
                }
            }
            else if (ticket.WalletType == CashWalletType)
            {
                if (ticket.RefCode == 0)
                {
                    ShowIndicatingTextOfCashWallet();
                }
                else
                {
                    ShowBonusIcon(ticket);
                }
            }
            else if (ticket.WalletType == CombinedWalletType && ticket.RefCode != 0)
            {
                ShowBonusIcon(ticket);
            }
            else
            {
                HideBonusPart();
            }
        }

        protected virtual void BuildStake(RTFHelper rtfHelper)
        {
            Template.stake = Template.stake.Replace("<br/>&nbsp;", "\n");

            rtfHelper.RTFRenderer.AddText(Template.stake, rtfHelper.PosFont);
        }

        protected virtual void BuildBonus(RTFHelper rtfHelper)
        {
            if (!Template.Bonus.Visible)
            {
                return;
            }

            if (Template.Bonus.BonusIcon.Visible)
            {
                rtfHelper.RTFRenderer.AddText($"\n({CoreBetList.Bonus})", rtfHelper.PosFont);
            }
            else
            {
                rtfHelper.RTFRenderer.AddText("\n" + Template.Bonus.Wallet.type, rtfHelper.PosFont);
            }
        }

        protected void HideBonusPart()
            => Template.Bonus.Visible = false;

        protected void ShowBonusIcon(ITicket ticket)
        {
            Template.Bonus.BonusIcon.refCode = ticket.RefCode.ToString();
            Template.Bonus.BonusIcon.transId = ticket.TransId.ToString();
            Template.Bonus.BonusIcon.bonusIconTitle = CoreBetList.ShowTransactionDetails;

            Template.Bonus.Wallet.Visible = false;
        }

        protected void ShowIndicatingTextOfBonusWallet()
        {
            Template.Bonus.Wallet.type = $"({CoreBetList.Bonus})";

            Template.Bonus.BonusIcon.Visible = false;
        }

        protected void ShowIndicatingTextOfCashWallet()
        {
            Template.Bonus.Wallet.type = $"({CoreBetList.Cash})";

            Template.Bonus.BonusIcon.Visible = false;
        }

        protected virtual void BuildOriginalStake(ITicket ticket)
        {
            if (IsCashOutTicket(ticket))
            {
                Template.OriginalStake.stake = ConvertByBetType.Stake(GetOriginalStake(ticket));
            }
            else
            {
                Template.OriginalStake.Hide();
            }
        }

        protected virtual void BuildOriginalStakeRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            if (IsCashOutTicket(ticket))
            {
                rtfHelper.RTFRenderer.AddText(Template.OriginalStake.stake + "\n", rtfHelper.PosFontCrossed);
            }
        }

        private static decimal GetOriginalStake(ITicket ticket)
        {
            if (ticket.OriginalStake >= 0)
            {
                return ticket.OriginalStake;
            }

            var originalStake = ticket.ExtraInfo.GetXmlTagAttributeValue(CashOutXmlTag, "s");

            return string.IsNullOrEmpty(originalStake) ? 0 : Convert.ToDecimal(originalStake);
        }

        private static bool IsCashOutTicket(ITicket ticket)
            => (ticket.BetdaqId == 0 || ticket.BetdaqId == -1) &&
                ticket.ExtraInfo.HasAttributeInXmlTag(CashOutXmlTag, "s");
    }
}