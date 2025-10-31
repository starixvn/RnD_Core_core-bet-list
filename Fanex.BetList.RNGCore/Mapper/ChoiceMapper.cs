namespace Fanex.BetList.Core.Mapper
{
    using Constants;
    using System.Collections.Generic;
    public class ChoiceMapper : BaseMapper
    {
        private static readonly IEnumerable<RangeMapping> ChoiceMapping = new List<RangeMapping>
        {
            new RangeMapping { MinRange = RNGBetTypes.SmartSoftMin + 1, MaxRange = RNGBetTypes.SmartSoftMax, BaseId = RNGBetTypes.SmartSoftMin },
            new RangeMapping { MinRange = RNGBetTypes.GPILotteryMin + 1, MaxRange = RNGBetTypes.GPILotteryMax, BaseId = RNGBetTypes.GPILotteryMin },
            new RangeMapping { MinRange = RNGBetTypes.HotDogMin + 1, MaxRange = RNGBetTypes.HotDogMax, BaseId = RNGBetTypes.HotDogMin },
            new RangeMapping { MinRange = RNGBetTypes.OdinGamingMin + 1, MaxRange = RNGBetTypes.OdinGamingMax, BaseId = RNGBetTypes.OdinGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.RichGamingMin + 1, MaxRange = RNGBetTypes.RichGamingPromotion, BaseId = RNGBetTypes.RichGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.AIGamingMin + 1, MaxRange = RNGBetTypes.AIGamingMax, BaseId = RNGBetTypes.AIGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.ClotPlayingMin + 1, MaxRange = RNGBetTypes.ClotPlayingMax, BaseId = RNGBetTypes.ClotPlayingMin },
            new RangeMapping { MinRange = RNGBetTypes.BGamingMin + 1, MaxRange = RNGBetTypes.BGamingMax, BaseId = RNGBetTypes.BGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.GeminiMin + 1, MaxRange = RNGBetTypes.GeminiMax, BaseId = RNGBetTypes.GeminiMin },
            new RangeMapping { MinRange = RNGBetTypes.VPGamingMin + 1, MaxRange = RNGBetTypes.VPGamingMax, BaseId = RNGBetTypes.VPGamingMin }
        };
        
        public ChoiceMapper()
        {
            foreach (var mapping in ChoiceMapping)
            {
                for (var betType = mapping.MinRange; betType <= mapping.MaxRange; betType++)
                {
                    MapIdToBaseId(betType, mapping.BaseId);
                }
            }
        }
    }
}