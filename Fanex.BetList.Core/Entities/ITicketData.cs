namespace Fanex.BetList.Core.Entities
{
    /// <summary>
    /// Reference data for bet types:
    /// 1) Horse Racing (33) => Build Odds, Status.
    /// 2) Live Casino Baccarat (1101) => Build Choice.
    /// 3) Live Roulette (1102) => Build Choice.
    /// 4) Virtual Horse Racing - Win/Place (1233) => Build Odds, Status.
    /// 5) Gold Deluxe Baccarat (1702) => Build Choice.
    /// 6) Gold Deluxe Live Roulette (1702) => Build Choice.
    /// 7) Gold Deluxe Parlay Baccarat (1703) => Build Choice.
    /// </summary>
    public interface ITicketData
    {
        string RefNo { get; set; }

        int BetTypeId { get; set; }

        string Status { get; set; }

        decimal Odds { get; set; }

        decimal Stake { get; set; }

        string BetTeam { get; set; }

        string TransDesc { get; set; }

        decimal SuperPT { get; set; }

        decimal MasterPT { get; set; }

        decimal AgentPT { get; set; }
    }
}