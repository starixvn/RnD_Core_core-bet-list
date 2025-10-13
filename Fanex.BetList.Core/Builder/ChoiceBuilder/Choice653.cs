namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    public class Choice653 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var playerInfo = new BasketballPlayerInfo(ticket.TransDesc);
            var betoption = $"<span class=\"favorite\">{ticket.BetTeam}</span>";

            Template.betTeam = $"{ticketHelper.GetTeamNameById(playerInfo.PlayerId)} - {betoption}";
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Underdog;
            Template.Handicap.handicap = null;
        }
        
        protected override void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam.Replace("</br>", "\n");
            base.AdjustBetTeamToRTF();
        }
    }

    [DataContract]
    public class BasketballPlayerInfo
    {
        public BasketballPlayerInfo()
        {
        }

        public BasketballPlayerInfo(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(BasketballPlayerInfo));
            var obj = serializer.SafeDeserializeFromString<BasketballPlayerInfo>(json);
            
            PlayerId = obj.PlayerId;            
            TeamId = obj.TeamId;
        }

        [DataMember(Name = "pid")]
        public int PlayerId { get; set; }        

        [DataMember(Name = "tid")]
        public int TeamId { get; set; }
    }
}