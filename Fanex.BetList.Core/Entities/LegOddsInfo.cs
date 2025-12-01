namespace Fanex.BetList.Core.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    
    [DataContract]
    public class LegOddsInfo
    {
        [DataMember(Name = "m")]
        public int MatchId { get; set; }

        [DataMember(Name = "o")]
        public decimal LegOdds { get; set; }

    }

    [DataContract]
    public class LegOddsTransDesc
    {
        public LegOddsTransDesc()
        {
            LegOddsbyMatch = new List<LegOddsInfo>();
        }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "legOdds")]
        public List<LegOddsInfo> LegOddsbyMatch { get; set; }
    }
}