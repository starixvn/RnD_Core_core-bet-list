namespace Fanex.BetList.Core.Entities
{
    /// <summary>
    /// Reference data for bet types:
    ///                     1) Horse Racing (33) => Build Odds.
    ///                     2) Live Casino Baccarat (1101) => Build Choice.
    ///                     3) Live Roulette (1102) => Build Choice.
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