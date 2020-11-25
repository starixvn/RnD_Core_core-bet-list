namespace Fanex.BetList.Core.Builder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    /// The top abstraction layer of concrete BetListBuilders.
    /// </summary>
    public interface IBetListHTMLBuilder : IBetListBaseBuilder
    {
        BetListHTMLBuilder Append(string content);

        BetListHTMLBuilder AddChoice(
                        ITicket ticket,
                        ITicketHelper ticketHelper,
                        List<ITicketData> ticketData,
                        string openTag,
                        string closeTag,
                        bool isShowScoreMap);

        string BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap);

        Choice_Template BuildChoiceBlock(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap);

        BetListHTMLBuilder AddOdds(ITicket ticket, List<ITicketData> ticketData, string openTag, string closeTag, GetCachePropertyById funcGetOddsTypeName);

        string BuildOdds(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName);

        string BuildOddsValue(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById getOddsTypeName);

        string BuildOddsTypeName(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById getOddsTypeName);

        BetListHTMLBuilder AddStake(ITicket ticket, ITicketHelper ticketHelper, string openTag, string closeTag);

        string BuildStake(ITicket ticket, ITicketHelper ticketHelper);

        BetListHTMLBuilder AddTrans(ITicket ticket, string openTag, string closeTag);

        string BuildTrans(ITicket ticket);

        string BuildTransTime(ITicket ticket);

        string BuildRefNo(ITicket ticket);

        string BuildLicenseeTxId(ITicket ticket);

        BetListHTMLBuilder AddStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, string openTag, string closeTag, bool isShowVNIP);

        string BuildStatus(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowVNIP);

        string BuildSystemParlayDetail(ITicketHelper ticketHelper, ISystemParlayData systemParlayData);

        string BuildTransEventDateAndStatusResult(ITicket ticket, List<ITicketData> ticketData);
    }
}