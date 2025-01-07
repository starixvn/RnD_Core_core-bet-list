namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using App_GlobalResources;
    using Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using Fanex.BetList.Core.Utils.TicketExt;
    using Templates;
    using Utils;

    public class Choice3001 : ChoiceLC
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>({1})</span>{2}</div>";
        
        private const string BetIdTag = "<span class='bet-id'>{0}</span>";

        private const string BetTeamAPIFormat = "<span class='favorite'>{0}</span>";

        private const string NewGamingAPISpecialChars = "|$|";

        public virtual string GetRefResourceId() => "SA_BT";

        public override string GetDisplayChoice(ITicketHelper ticketHelper, string choice)
            => string.Format(BetTeamAPIFormat, ticketHelper.GetResourceData(GetRefResourceId(), choice));

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = string.Empty;

            if (IsDetailTicket(ticket))
            {
                BuildBetId(ticket,ticketHelper, ticketData);
            }
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if(IsNewDetailTicket(ticket))
            {
                base.BuildBetTeam(ticket, ticketHelper, ticketData);
            }
            else 
            {
                Template.betTeam = GetBetChoiceOldTicket(ticket, ticketHelper);
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = string.Empty;
        }

        public override string BuildDetailFunction(ITicket ticket)
        {
            var detailFunctionBuilder = new StringBuilder();

            detailFunctionBuilder.Append("showSAGamingDetails(&quot;")
                .Append(ticket.TransId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.WinlostDate.ToString(CultureInfo.InvariantCulture))
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.CustId.ToString())
                .Append("&quot;")
                .Append(",&quot;")
                .Append(ticket.GetTransactionType())
                .Append("&quot;);");

            return detailFunctionBuilder.ToString();
        }

        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = string.Join("\n", Template.betTeam.SplitHTMLIntoRows());
            Template.League.LeagueName.leagueName = string.Join(string.Empty, Template.League.LeagueName.leagueName.StripHTML());
            Template.BetType.betTypeName = string.Join(string.Empty, Template.BetType.betTypeName.StripHTML());
        }

        private void BuildBetId(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if(IsNewDetailTicket(ticket))
            {
                base.BuildBetType(ticket, ticketHelper, ticketData);
            }
            else 
            {
                Template.BetType.betTypeName = string.Format(BetIdTag, $"{CoreBetList.BetID}: {ticket.BetTeam}");
            }
        }

        private string GetBetChoiceOldTicket(ITicket ticket, ITicketHelper ticketHelper)
        {
            var betTeam = new StringBuilder();
            var choiceOption = ticket.ParseBetcheck();
            foreach (var item in choiceOption)
            {
                var choiceName = ticketHelper.GetResourceData(GetRefResourceId(), item.Key);
                var choiceStake = $"{ticket.Currency} {item.Value}";

                var choiceId = GetBetChoiceId(ticket, item.Key);

                betTeam.AppendFormat(BetTeamFormat, choiceName, choiceStake, choiceId);
            }

            return betTeam.ToString();
        }

        private static string GetBetChoiceId(ITicket ticket, string key)
        {
            var betchoiceIds = ticket.ParseBetChoice();

            return betchoiceIds.ContainsKey(key) && !string.IsNullOrEmpty(betchoiceIds[key]) ?
                string.Format(BetIdTag, $" - {betchoiceIds[key]}") :
                string.Empty;
        }

        private static bool IsNewDetailTicket(ITicket ticket)
            => !string.IsNullOrEmpty(ticket.BetCheck) && ticket.BetCheck.Contains(NewGamingAPISpecialChars);
    }
}