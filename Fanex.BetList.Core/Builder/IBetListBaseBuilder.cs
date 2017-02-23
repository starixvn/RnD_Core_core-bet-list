namespace Fanex.BetList.Core.Builder
{
    using ChoiceBuilder;
    using OddsBuilder;
    using StakeBuilder;
    using StatusBuilder;
    using TransBuilder;

    public interface IBetListBaseBuilder
    {
        IChoice CreateChoiceBuilder(int bettype);

        IOdds CreateOddsBuilder(int bettype);

        IStake CreateStakeBuilder(int bettype);

        IStatus CreateStatusBuilder(int bettype);

        ITrans CreateTransBuilder(int bettype);
    }
}