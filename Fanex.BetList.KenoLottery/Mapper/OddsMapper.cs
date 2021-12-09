namespace Fanex.BetList.Core.Mapper
{
    using Fanex.BetList.Core.Constants;

    public class OddsMapper : BaseMapper
    {
        public OddsMapper()
        {
            MapIdToBaseId(KenoLotteryBetTypes.MaxDice90, KenoLotteryBetTypes.MaxDice60);
            MapIdToBaseId(KenoLotteryBetTypes.JhandiMunda, KenoLotteryBetTypes.MaxDice60);
            MapIdToBaseId(KenoLotteryBetTypes.Fireball10, KenoLotteryBetTypes.MaxDice60);
            MapIdToBaseId(KenoLotteryBetTypes.MaxDice3, KenoLotteryBetTypes.MaxDice60);
            MapIdToBaseId(KenoLotteryBetTypes.MaxDice4, KenoLotteryBetTypes.MaxDice60);
        }
    }
}