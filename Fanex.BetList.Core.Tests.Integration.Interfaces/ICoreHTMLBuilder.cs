// ---------------------------------------------------------------------------------
// <copyright file="ICoreHTMLBuilder.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.25">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Tests.Integration.Interfaces
{
    public interface ICoreHTMLBuilder
    {
        int TicketCount
        {
            get;
        }

        string BuildChoice(int dataIndex, bool isShowScoreMap);

        string BuildOdds(int dataIndex);

        string BuildStake(int dataIndex);

        string BuildStatus(int dataIndex, bool isShowVNIP);

        string BuildTrans(int dataIndex);

        string BuildSystemParlayDetail();

        string GetTicketJsonByIndex(int index);

        string GetTicketDataJson();
    }
}
