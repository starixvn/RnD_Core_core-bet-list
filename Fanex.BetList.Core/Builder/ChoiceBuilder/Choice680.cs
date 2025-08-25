namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    public class Choice680 : Choice653
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var players = new BasketballPlayers(ticket.TransDesc);
            if (players.Players.Count == 1)
            {
                var playerId = players.Players[0].PlayerId;
                var playerName = ticketHelper.GetTeamNameById(playerId);
                var betchoice = $"{playerName}";
                var playerOption = $"<span class=\"favorite\">{playerName}</span>";
                Template.betTeam = $"{betchoice} - {playerOption}";
            }
            else if (players.Players.Count >= 2)
            {
                var playerId1 = players.Players[0].PlayerId;
                var playerName1 = ticketHelper.GetTeamNameById(playerId1);
                var playerId2 = players.Players[1].PlayerId;
                var playerName2 = ticketHelper.GetTeamNameById(playerId2);
                var betchoice = $"{playerName1} & {playerName2}";
                var playerOption = $"<span class=\"favorite\">{playerName1}</span>";

                Template.betTeam = $"{betchoice} - {playerOption}";
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }
    }

    [DataContract]
    public class BasketballPlayers
    {
        public BasketballPlayers()
        {
            Players = new List<BasketballPlayerInfo>();
        }

        public BasketballPlayers(string json)
        {
            Players = new List<BasketballPlayerInfo>();

            var serializer = new DataContractJsonSerializer(typeof(BasketballPlayers));
            var obj = serializer.SafeDeserializeFromString<BasketballPlayers>(json);

            Players = obj.Players ?? new List<BasketballPlayerInfo>();
        }

        [DataMember(Name = "p")]
        public List<BasketballPlayerInfo> Players { get; set; }
    }
}