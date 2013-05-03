namespace Fanex.PTEngine.Services
{
    using Fanex.PTEngine.BusinessObjects;

    public interface IBettingLimitService
    {
        /// <summary>
        /// Get BetSetting For Edit
        /// </summary>
        /// <param name="requestContext">The context of Edit</param>
        /// <returns>Return a IBettingLimit contains current</returns>
        IBettingLimit<BettingLimitItem> GetBettingForEdit(IRequestContext context);

        /// <summary>
        /// Update Bet Setting
        /// </summary>
        /// <param name="bettingLimit"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Result UpdateBetting(IBettingLimit<BettingLimitItem> bettingLimit, IRequestContext context);
    }
}