namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using System;
    using App_GlobalResources;
    using Entities;
    using BetListNPOI.SS.UserModel;
    using Templates;
    using Utils;
    using Utils.StringExt;

    /// <summary>
    /// Stake for most bet types.
    /// </summary>
    public class Stake1 : IStake
    {
        private const string CashOutXmlTag = "p25";
        private const string SportsbookXmlTag = "p1";

        public Stake1()
        {
            Template = new Stake_Template();
        }

        public Stake_Template Template { get; set; }

        public virtual Stake_Template Render(ITicket ticket, ITicketHelper ticketHelper)
        {
            BuildStake(ticket);
            BuildOriginalStake(ticket);
            BuildBonus(ticket, ticketHelper);

            return Template;
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, ITicketHelper ticketHelper, RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper);

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

        protected virtual void BuildBonus(ITicket ticket, ITicketHelper ticketHelper)
        {
            BuildNormalBonus(ticket);
            BuildRebateBonus(ticket);
            BuildNonPeakHoursPromotion(ticket);
            BuildGodOfFortune(ticket, ticketHelper);
        }

        protected void BuildNormalBonus(ITicket ticket)
        {
            const byte BonusWallet = 1;
            const byte CashWallet = 2;

            if (ticket.RefCode == 0)
            {
                switch (ticket.WalletType)
                {
                    case BonusWallet:
                        ShowIndicatingTextOfBonusWallet();
                        break;

                    case CashWallet:
                        ShowIndicatingTextOfCashWallet();
                        break;

                    default:
                        HideNormalBonusPart();
                        break;
                }
            }
            else
            {
                ShowBonusIcon(ticket);
            }
        }

        protected void HideNormalBonusPart()
            => Template.Bonus.NormalBonus.Hide();

        protected void ShowBonusIcon(ITicket ticket)
        {
            Template.Bonus.NormalBonus.BonusIcon.refCode = ticket.RefCode.ToString();
            Template.Bonus.NormalBonus.BonusIcon.transId = ticket.TransId.ToString();
            Template.Bonus.NormalBonus.BonusIcon.bonusIconTitle = CoreBetList.ShowTransactionDetails;

            Template.Bonus.NormalBonus.Wallet.Hide();
        }

        protected void ShowIndicatingTextOfBonusWallet()
        {
            Template.Bonus.NormalBonus.Wallet.type = $"({CoreBetList.Bonus})";

            Template.Bonus.NormalBonus.BonusIcon.Hide();
        }

        protected void ShowIndicatingTextOfCashWallet()
        {
            Template.Bonus.NormalBonus.Wallet.type = $"({CoreBetList.Cash})";

            Template.Bonus.NormalBonus.BonusIcon.Hide();
        }

        protected virtual void BuildRebateBonus(ITicket ticket)
            => Template.Bonus.RebateBonusIcon.Visible &= IsRebateBonusTicket(ticket);

        protected static bool IsRebateBonusTicket(ITicket ticket)
        {
            const string bonusTypeAttributeName = "t";
            const string rebateBonusType = "2";

            var bonusType = ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookXmlTag, bonusTypeAttributeName);

            return bonusType == rebateBonusType;
        }

        protected virtual void BuildNonPeakHoursPromotion(ITicket ticket)
            => Template.Bonus.NonPeakHoursPromotionIcon.Visible &= IsNonPeakHoursPromotionTicket(ticket);

        protected virtual void BuildGodOfFortune(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.Bonus.GodOfFortune.Visible &= IsGodOfFortuneTicket(ticket);
            if (Template.Bonus.GodOfFortune.Visible)
            {
                var stake = ConvertByBetType.Stake(ticket.Stake - ticket.ActualStake);
                var discount = ticket.ExtraInfo.GetXmlTagAttributeValue(SportsbookXmlTag, "d");
                var returnStakeText = ticketHelper.GetResourceData("God_of_Fortune", "29v");
                var discountText = ticketHelper.GetResourceData("God_of_Fortune", "29d");

                Template.Bonus.GodOfFortune.returnStake = string.Format(returnStakeText, stake);
                Template.Bonus.GodOfFortune.discount = string.Format(discountText, discount);
            }
        }

        protected static bool IsGodOfFortuneTicket(ITicket ticket)
        {
            const string godOfFortuneAttributeName = "d";

            return ticket.ExtraInfo.HasAttributeInXmlTag(SportsbookXmlTag, godOfFortuneAttributeName);
        }

        protected static bool IsNonPeakHoursPromotionTicket(ITicket ticket)
        {
            const string nonPeakHoursPromotionXmlTag = "p37";
            const string requiredAttributeName = "i";

            return ticket.ExtraInfo.HasAttributeInXmlTag(nonPeakHoursPromotionXmlTag, requiredAttributeName);
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

            rtfHelper.RTFRenderer.AddText(GetBonusIndicatingText(), rtfHelper.PosFont);
        }

        protected string GetBonusIndicatingText()
        {
            var bonusIndicatingText = string.Empty;

            if (Template.Bonus.NormalBonus.Visible)
            {
                bonusIndicatingText += Template.Bonus.NormalBonus.BonusIcon.Visible
                    ? $"\n({CoreBetList.Bonus})"
                    : $"\n{Template.Bonus.NormalBonus.Wallet.type}";
            }

            if (Template.Bonus.RebateBonusIcon.Visible)
            {
                bonusIndicatingText += $"\n({CoreBetList.Rebate})";
            }

            if (Template.Bonus.NonPeakHoursPromotionIcon.Visible)
            {
                bonusIndicatingText += $"\n({CoreBetList.Promotion})";
            }

            if (Template.Bonus.GodOfFortune.Visible)
            {
                bonusIndicatingText += $"\n{Template.Bonus.GodOfFortune.returnStake}";
                bonusIndicatingText += $"\n{Template.Bonus.GodOfFortune.discount}";
            }

            return bonusIndicatingText;
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