using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

[DataContract]
public class BetData
{
    [DataMember(Name = "s")]
    public List<Stat> Stats { get; set; }

    [DataMember(Name = "r")]
    public List<Result> Results { get; set; }

    [DataMember(Name = "pid")]
    public string PlayerId { get; set; }

    [DataMember(Name = "pname")]
    public string PlayerName { get; set; }

    [DataMember(Name = "isH")]
    public bool IsHome { get; set; }

    [DataMember(Name = "tid")]
    public int TeamId { get; set; }

    public BetData(string json)
    {
        var serializer = new DataContractJsonSerializer(typeof(BetData));
        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            var obj = (BetData)serializer.ReadObject(ms);

            // copy property vào this
            this.Stats = obj.Stats;
            this.Results = obj.Results;
            this.PlayerId = obj.PlayerId;
            this.PlayerName = obj.PlayerName;
            this.IsHome = obj.IsHome;
            this.TeamId = obj.TeamId;
        }
    }

    public override string ToString()
    {
        var statTexts = Stats?.Select(s => s.ToString()).ToList() ?? new List<string>();
        string text = statTexts.Count > 0
            ? $"{PlayerName} have {string.Join(", ", statTexts)}"
            : PlayerName;

        if (Results != null && Results.Any())
        {
            var rTexts = Results.Select(r => r.ToString()).ToList();
            if (rTexts.Count == 1)
            {
                text += " and " + rTexts[0];
            }
            else
            {
                text += ", " + string.Join(", ", rTexts.Take(rTexts.Count - 1))
                        + " and " + rTexts.Last();
            }
        }

        return text;
    }
}

[DataContract]
public class Stat
{
    [DataMember(Name = "t")]
    public string Type { get; set; }

    [DataMember(Name = "value")]
    public string Value { get; set; }

    public override string ToString()
    {
        switch (Type)
        {
            case "points": return $"{Value} points";
            case "assists": return $"{Value} assists";
            case "three_points": return $"{Value} three point field goals";
            default: return $"{Value} {Type}";
        }
    }
}

[DataContract]
public class Result
{
    [DataMember(Name = "t")]
    public string Type { get; set; }

    [DataMember(Name = "value")]
    public string Value { get; set; }

    public override string ToString()
    {
        switch (Type)
        {
            case "winner": return $"Team {Value} to Win";
            case "over": return $"Total Points Over {Value}";
            case "handicap": return $"Handicap {Value}";
            default: return $"{Type} {Value}";
        }
    }
}