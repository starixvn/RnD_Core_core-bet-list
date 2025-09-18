namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Utils;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    public class Choice675 : Choice653
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeamModel = new BasketballPlayerModel(ticket.TransDesc, ticketHelper);
            var betTeam = CoreBetList.ResourceManager.GetString(betTeamModel.ResourceKey());
            betTeam = string.IsNullOrEmpty(betTeam) ? betTeamModel.ResourceKey() : betTeamModel.Format(betTeam);
            betTeam = $"<span class=\"favorite\">{betTeam}</span>";
            var playerName = ticketHelper.GetTeamNameById(betTeamModel.PlayerId);

            Template.betTeam = $"{playerName}</br>{betTeam}";
        }
    }

    [DataContract]
    public class BasketballPlayerModel
    {
        public BasketballPlayerModel()
        {
            Statistics = new List<Item>();
            Results = new List<Item>();
            AllItems = new List<Item>();
        }

        public BasketballPlayerModel(string json, ITicketHelper ticketHelper)
        {
            Statistics = new List<Item>();
            Results = new List<Item>();
            AllItems = new List<Item>();
            
            var serializer = new DataContractJsonSerializer(typeof(BasketballPlayerModel));
            var obj = serializer.SafeDeserializeFromString<BasketballPlayerModel>(json);

            Statistics = obj.Statistics ?? new List<Item>();
            Results = obj.Results ?? new List<Item>();
            PlayerId = obj.PlayerId;
            PlayerName = obj.PlayerName;
            IsHome = obj.IsHome;
            TeamId = obj.TeamId;
            TeamName = ticketHelper.GetTeamNameById(obj.TeamId);
            AllItems = Statistics.Concat(Results).ToList();
        }

        [DataMember(Name = "s")]
        public List<Item> Statistics { get; set; }

        [DataMember(Name = "r")]
        public List<Item> Results { get; set; }

        [DataMember(Name = "pid")]
        public string PlayerId { get; set; }

        [DataMember(Name = "pname")]
        public string PlayerName { get; set; }

        [DataMember(Name = "isH")]
        public bool IsHome { get; set; }

        [DataMember(Name = "tid")]
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        private List<Item> AllItems { get; set; }

        public string ResourceKey()
        {
            return $"bet_performance_{string.Join("_and_", AllItems.Select(i => i.Type))}";
        }

        public string Format(string stringFormat)
        {
            stringFormat = stringFormat.Replace("#winner#", TeamName);
            foreach (var s in AllItems)
            {
                stringFormat = stringFormat.Replace($"#{s.Type}#", s.Value);
            }

            return stringFormat;
        }
    }

    [DataContract]
    public class Item
    {
        [DataMember(Name = "t")]
        public string Type { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}