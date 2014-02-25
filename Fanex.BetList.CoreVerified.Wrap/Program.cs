// ---------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.24">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.CoreVerified.Wrap
{
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.BetList.Core.Tests.Integration.Data;
    using Fanex.BetList.CoreVerified.Wrap.Logging;

    /// <summary>
    /// Class Program.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:AccessModifierMustBeDeclared", Justification = "Reviewed.")]
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            DataSet _betData = BetListDAO.GetBetListData();

            if (_betData != null && _betData.Tables.Count == 2)
            {
                int ticketCount = _betData.Tables[0].Rows.Count;

                bool testHTML = true;

                if (testHTML)
                {
                    var _htmlBuilderVerified = new CoreVerifiedHTML(_betData);

                    XMLLogger.Start();

                    for (int i = 0; i < ticketCount; i++)
                    {
                        string choice = _htmlBuilderVerified.BuildChoice(i, true);
                        string odds = _htmlBuilderVerified.BuildOdds(i);
                        string stake = _htmlBuilderVerified.BuildStake(i);
                        string status = _htmlBuilderVerified.BuildStatus(i, true);
                        string trans = _htmlBuilderVerified.BuildTrans(i);

                        XMLLogger.Write(i, choice, odds, stake, status, trans);
                    }

                    XMLLogger.End();
                }
                else
                {
                    var _htmlExcelBuilder = new CoreVerifiedExcel(_betData);
                    short numberOfColumns = 5;

                    _htmlExcelBuilder.InitializeWorksheet(numberOfColumns);

                    for (int i = 0; i < ticketCount; i++)
                    {
                        _htmlExcelBuilder
                                        .AddChoice(i, true)
                                        .AddOdds(i)
                                        .AddStake(i)
                                        .AddStatus(i, true)
                                        .AddTrans(i);
                    }

                    string filePath = "OldBetList.xls";
                    _htmlExcelBuilder.FinalizeAndSave(filePath);
                }
            }
        }
    }
}
