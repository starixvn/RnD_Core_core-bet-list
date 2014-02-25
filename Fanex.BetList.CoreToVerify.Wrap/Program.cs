// ---------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.25">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.CoreToVerify.Wrap
{
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.BetList.Core.Tests.Integration.Data;

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

                var _htmlExcelBuilder = new CoreToVerifyExcel(_betData);
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

                string filePath = "NewBetList.xls";

                _htmlExcelBuilder.FinalizeAndSave(filePath);
            }
        }
    }
}
