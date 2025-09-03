namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Constants;
    using System.Collections.Generic;

    /// <summary>Playtech Live with Jackpot Prize</summary>
    public class Choice15799 : Choice15600
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            base.BuildBetTeam(ticket, ticketHelper, ticketData);
            if (IsDetailTicket(ticket))
            {
                Template.betTeam += $"{LiveCasinoConstants.OpenTagJackpotRefNo}{CoreBetList.forRefNo}: {ticket.BetdaqId}{LiveCasinoConstants.CloseTagDiv}";
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap = null;
            Template.betTeamClassName = Underdog;
        }

        protected override void AdjustBetTeamToRTF()
        {   
            Template.betTeam = Template.betTeam.Replace(LiveCasinoConstants.OpenTagJackpotRefNo, string.Empty)
                .Replace(LiveCasinoConstants.CloseTagDiv, string.Empty);

            base.AdjustBetTeamToRTF();
        }
    }
}