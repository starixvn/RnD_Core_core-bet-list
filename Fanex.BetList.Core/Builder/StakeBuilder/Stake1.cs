namespace Fanex.BetList.Core.Builder.StakeBuilder
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Stake for most bet types.
    /// </summary>
    public class Stake1 : IStake
    {
        private const string CashOutXmlTag = "p25";

        public Stake1()
        {
            Template = new Stake_Template();
        }

        public Stake_Template Template { get; set; }

        public virtual Stake_Template Render(ITicket ticket)
        {
            BuildStake(ticket);
            BuildOriginalStake(ticket);

            return Template;
        }

        public virtual IRichTextString RenderRTF(ITicket ticket, RTFHelper rtfHelper)
        {
            Render(ticket);
            Template.stake = Template.stake.Replace("<br/>&nbsp;", "\n");

            BuildOriginalStakeRTF(ticket, rtfHelper);
            rtfHelper.RTFRenderer.AddText(Template.stake, rtfHelper.PosFont);

            IRichTextString rtfStake = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfStake;
        }

        protected virtual void BuildStake(ITicket ticket)
        {
            Template.stake = ConvertByBetType.Stake(ticket.Stake);
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

        private decimal GetOriginalStake(ITicket ticket)
        {
            if (ticket.OriginalStake >= 0)
            {
                return ticket.OriginalStake;
            }
            else
            {
                if (!string.IsNullOrEmpty(ticket.ExtraInfo))
                {
                    var xmlDoc = XDocument.Parse(ticket.ExtraInfo);
                    var originalStake = xmlDoc.Element(CashOutXmlTag)?.Attribute("s")?.Value;

                    if (!string.IsNullOrEmpty(originalStake))
                    {
                        return Convert.ToDecimal(originalStake);
                    }
                }

                return 0;
            }
        }

        private bool IsCashOutTicket(ITicket ticket)
                => (ticket.BetdaqId == 0 || ticket.BetdaqId == -1) && HasCashOutExtraInfo(ticket.ExtraInfo);

        private bool HasCashOutExtraInfo(string extraInfo)
        {
            if (!string.IsNullOrEmpty(extraInfo))
            {
                var xmlDoc = XDocument.Parse(extraInfo);

                return xmlDoc.Element(CashOutXmlTag) != null;
            }

            return false;
        }
    }
}