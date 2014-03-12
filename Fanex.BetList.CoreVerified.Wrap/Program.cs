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
            bool testRunningBet = true;
            bool testHTML = true;

            int ticketCount = 0;
            CoreVerifiedHTML _htmlBuilderVerified = null;
            CoreVerifiedExcel _htmlExcelBuilder = null;

            PrepareBuilder(testRunningBet, testHTML, ref _htmlBuilderVerified, ref _htmlExcelBuilder, ref ticketCount);            

            // Start generating
            if (testHTML)
            {
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

        /// <summary>
        /// Prepares the builder.
        /// </summary>
        /// <param name="testRunningBet">If set to <c>true</c> [test running bet].</param>
        /// <param name="testHTML">If set to <c>true</c> [test HTML].</param>
        /// <param name="htmlBuilderVerified">The HTML builder verified.</param>
        /// <param name="excelBuilderVerified">The excel builder verified.</param>
        /// <param name="ticketCount">The ticket count.</param>
        private static void PrepareBuilder(
            bool testRunningBet, 
            bool testHTML, 
            ref CoreVerifiedHTML htmlBuilderVerified, 
            ref CoreVerifiedExcel excelBuilderVerified, 
            ref int ticketCount)
        {
            if (testRunningBet)
            {
                DataTable _betData = BetListDAO.GetRunningBetData();

                if (_betData != null && _betData.Rows.Count > 0)
                {
                    ticketCount = _betData.Rows.Count;

                    if (testHTML)
                    {
                        htmlBuilderVerified = new CoreVerifiedHTML(_betData);
                    }
                    else
                    {
                        excelBuilderVerified = new CoreVerifiedExcel(_betData);
                    }
                }
            }
            else
            {
                DataSet _betData = BetListDAO.GetBetListData();

                if (_betData != null && _betData.Tables.Count == 2)
                {
                    ticketCount = _betData.Tables[0].Rows.Count;

                    if (testHTML)
                    {
                        htmlBuilderVerified = new CoreVerifiedHTML(_betData);
                    }
                    else
                    {
                        excelBuilderVerified = new CoreVerifiedExcel(_betData);
                    }
                }
            }
        }
    }
}
