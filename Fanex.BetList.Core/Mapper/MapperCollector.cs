namespace Fanex.BetList.Core.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class MapperCollector
    {
        private const string CACHEKEY = "Fanex.BetList.Core.CacheKey";

        private const string CHOICE = "Choice";
        private const string STATUS = "Status";
        private const string STAKE = "Stake";
        private const string ODDS = "Odds";
        private const string TRANS = "Trans";

        private static IDictionary<int, int> _choiceMapperData = null;
        private static IDictionary<int, int> _statusMapperData = null;
        private static IDictionary<int, int> _stakeMapperData = null;
        private static IDictionary<int, int> _oddsMapperData = null;
        private static IDictionary<int, int> _transMapperData = null;

        public static int GetChoiceBaseBetTypeOf(int betType) => _choiceMapperData.ContainsKey(betType) ? _choiceMapperData[betType] : betType;

        public static int GetStatusBaseBetTypeOf(int betType) => _statusMapperData.ContainsKey(betType) ? _statusMapperData[betType] : betType;

        public static int GetStakeBaseBetTypeOf(int betType) => _stakeMapperData.ContainsKey(betType) ? _stakeMapperData[betType] : betType;

        public static int GetOddsBaseBetTypeOf(int betType) => _oddsMapperData.ContainsKey(betType) ? _oddsMapperData[betType] : betType;

        public static int GetTransBaseBetTypeOf(int betType) => _transMapperData.ContainsKey(betType) ? _transMapperData[betType] : betType;

        public static void CollectAllMapperData(IList<Assembly> assemblies)
        {
            InitializeMapperData();

            CollectMapperDataOf(CHOICE, assemblies);
            CollectMapperDataOf(STATUS, assemblies);
            CollectMapperDataOf(STAKE, assemblies);
            CollectMapperDataOf(ODDS, assemblies);
            CollectMapperDataOf(TRANS, assemblies);
        }

        private static void InitializeMapperData()
        {
            _choiceMapperData = new Dictionary<int, int>();
            _statusMapperData = new Dictionary<int, int>();
            _stakeMapperData = new Dictionary<int, int>();
            _oddsMapperData = new Dictionary<int, int>();
            _transMapperData = new Dictionary<int, int>();
        }

        private static void CollectMapperDataOf(string componentName, IList<Assembly> assemblies)
        {
            string typeName = string.Join(null, new string[] { "Fanex.BetList.Core.Mapper.", componentName, "Mapper" });

            foreach (var assembly in assemblies)
            {
                Type type = Type.GetType(typeName + ", " + assembly);

                if (type != null)
                {
                    IMapper mapper = Activator.CreateInstance(type) as IMapper;

                    AddDataFromMapperObj(mapper, componentName);
                }
            }
        }

        public static void AddDataFromMapperObj(IMapper mapper, string componentName)
        {
            switch (componentName)
            {
                case CHOICE:
                    AppendDataToSource(ref _choiceMapperData, mapper.MapperData);
                    break;

                case STATUS:
                    AppendDataToSource(ref _statusMapperData, mapper.MapperData);
                    break;

                case STAKE:
                    AppendDataToSource(ref _stakeMapperData, mapper.MapperData);
                    break;

                case ODDS:
                    AppendDataToSource(ref _oddsMapperData, mapper.MapperData);
                    break;

                case TRANS:
                    AppendDataToSource(ref _transMapperData, mapper.MapperData);
                    break;
            }
        }

        private static void AppendDataToSource(ref IDictionary<int, int> source, IDictionary<int, int> data)
        {
            source = source.Concat(data).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}