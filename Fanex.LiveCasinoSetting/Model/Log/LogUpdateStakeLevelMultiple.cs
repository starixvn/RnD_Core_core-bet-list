// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.LiveCasinoSetting.Model.Log
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogUpdateStakeLevelMultiple : LogEntityBase, ILogData
    {
        private Dictionary<int, Dictionary<int, Result>> _resultDictionary;
        private Dictionary<int, List<GameType>> _oldGameTypeValues;
        private Dictionary<int, List<GameType>> _newGameTypeValues;

        public LogUpdateStakeLevelMultiple(Dictionary<int, List<GameType>> oldGameTypeValues, Dictionary<int, List<GameType>> newGameTypeValues
            , Dictionary<int, Dictionary<int, Result>> resultDictionary)
        {
            _oldGameTypeValues = oldGameTypeValues;
            _newGameTypeValues = newGameTypeValues;
            _resultDictionary = resultDictionary;

            //if (resultDictionary is Dictionary<int, Result>)
            //{
            //    ProccessSuccessfulResult();
            //}
        }
    }
}
