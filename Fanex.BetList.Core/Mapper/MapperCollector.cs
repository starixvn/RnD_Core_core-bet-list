namespace Fanex.BetList.Core.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Web.Compilation;
    public class MapperCollector
    {
        private const string CACHEKEY = "Fanex.BetList.Core.CacheKey";

        private const string CHOICENAME = "Choice";
        private const string STATUSNAME = "Status";
        private const string STAKENAME = "Stake";
        private const string ODDSNAME = "Odds";
        private const string TRANSNAME = "Trans";

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

        public static void CollectMapperData()
        {
            InitializeMapperData();

            var assemblies = GetBetListAssemblies();

            CollectMapperDataFromAsm(CHOICENAME, assemblies);
            CollectMapperDataFromAsm(STATUSNAME, assemblies);
            CollectMapperDataFromAsm(STAKENAME, assemblies);
            CollectMapperDataFromAsm(ODDSNAME, assemblies);
            CollectMapperDataFromAsm(TRANSNAME, assemblies);
        }

        private static void InitializeMapperData()
        {
            _choiceMapperData = new Dictionary<int, int>();
            _statusMapperData = new Dictionary<int, int>();
            _stakeMapperData = new Dictionary<int, int>();
            _oddsMapperData = new Dictionary<int, int>();
            _transMapperData = new Dictionary<int, int>();
        }

        private static void CollectMapperDataFromAsm(string componentName, IList<Assembly> assemblies)
        {
            string typeName = string.Join(null, new string[] { "Fanex.BetList.Core.Mapping.", componentName, "Mapping" });

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

        private static void AddDataFromMapperObj(IMapper mapper, string componentName)
        {
            switch (componentName)
            {
                case CHOICENAME:
                    AppendDataToSource(ref _choiceMapperData, mapper.MapperData);
                    break;

                case STATUSNAME:
                    AppendDataToSource(ref _statusMapperData, mapper.MapperData);
                    break;

                case STAKENAME:
                    AppendDataToSource(ref _stakeMapperData, mapper.MapperData);
                    break;

                case ODDSNAME:
                    AppendDataToSource(ref _oddsMapperData, mapper.MapperData);
                    break;

                case TRANSNAME:
                    AppendDataToSource(ref _transMapperData, mapper.MapperData);
                    break;
            }
        }

        private static void AppendDataToSource(ref IDictionary<int, int> source, IDictionary<int, int> data)
        {
            source = source.Concat(data).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static List<Assembly> GetBetListAssemblies()
        {
            ObjectCache cache = MemoryCache.Default;

            var assemblies = (List<Assembly>)cache[CACHEKEY];

            if (assemblies == null || !assemblies.Any())
            {
                try
                {
                    assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
                }
                catch
                {
                    assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
                }

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.Priority = CacheItemPriority.NotRemovable;

                assemblies = assemblies.Where(x => x.FullName.StartsWith("Fanex.BetList.", StringComparison.InvariantCulture)).ToList();

                cache.Set(CACHEKEY, assemblies, policy);
            }

            return assemblies;
        }
    }
}