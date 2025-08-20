namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;

    [DataContract]
    public class BasketballPlayerModel
    {
        public BasketballPlayerModel(string json, ITicketHelper ticketHelper)
        {
            Statistics = new List<Item>();
            Results = new List<Item>();
            AllItems = new List<Item>();
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(BasketballPlayerModel));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var obj = (BasketballPlayerModel)serializer.ReadObject(ms);

                    Statistics = obj.Statistics ?? new List<Item>();
                    Results = obj.Results ?? new List<Item>();
                    PlayerId = obj.PlayerId;
                    PlayerName = obj.PlayerName;
                    IsHome = obj.IsHome;
                    TeamId = obj.TeamId;
                    TeamName = ticketHelper.GetTeamNameById(obj.TeamId);
                    AllItems = Statistics.Concat(Results).ToList();
                }
            }
            catch { }
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
