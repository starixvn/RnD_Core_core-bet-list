namespace Fanex.BetList.Core.Mapper
{
    using Constants;
    using System.Collections.Generic;
    public class StatusMapper : BaseMapper
    {
        private static readonly IEnumerable<RangeMapping> StatusMapping = new List<RangeMapping>
        {
            new RangeMapping { MinRange = RNGBetTypes.SmartSoftMin + 1, MaxRange = RNGBetTypes.SmartSoftMax, BaseId = RNGBetTypes.SmartSoftMin },
            new RangeMapping { MinRange = RNGBetTypes.HotDogMin + 1, MaxRange = RNGBetTypes.HotDogMax, BaseId = RNGBetTypes.HotDogMin },
            new RangeMapping { MinRange = RNGBetTypes.HotDogMax, MaxRange = RNGBetTypes.HotDogMax - 1, BaseId = BetTypes.SabaLuckySpin },
            new RangeMapping { MinRange = RNGBetTypes.OdinGamingMin + 1, MaxRange = RNGBetTypes.OdinGamingMax, BaseId = RNGBetTypes.OdinGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.RichGamingMin + 1, MaxRange = RNGBetTypes.RichGamingPromotion - 1, BaseId = RNGBetTypes.RichGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.RichGamingPromotion, MaxRange = RNGBetTypes.RichGamingPromotion, BaseId = BetTypes.SabaLuckySpin },
            new RangeMapping { MinRange = RNGBetTypes.AIGamingMin + 1, MaxRange = RNGBetTypes.AIGamingMax, BaseId = RNGBetTypes.AIGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.ClotPlayingMin + 1, MaxRange = RNGBetTypes.ClotPlayingMax, BaseId = RNGBetTypes.ClotPlayingMin },
            new RangeMapping { MinRange = RNGBetTypes.GPILotteryMin + 1, MaxRange = RNGBetTypes.GPILotteryMax, BaseId = RNGBetTypes.GPILotteryMin },
            new RangeMapping { MinRange = RNGBetTypes.BGamingMin + 1, MaxRange = RNGBetTypes.BGamingMax, BaseId = RNGBetTypes.BGamingMin },
            new RangeMapping { MinRange = RNGBetTypes.VPGamingMin + 1, MaxRange = RNGBetTypes.VPGamingMax, BaseId = RNGBetTypes.VPGamingMin },
        };
        
        public StatusMapper()
        {
            foreach (var mapping in StatusMapping)
            {
                for (var betType = mapping.MinRange; betType <= mapping.MaxRange; betType++)
                {
                    MapIdToBaseId(betType, mapping.BaseId);
                }
            }
        }
    }
}