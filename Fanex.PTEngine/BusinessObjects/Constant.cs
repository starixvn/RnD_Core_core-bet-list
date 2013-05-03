namespace Fanex.PTEngine.BusinessObjects
{
    public class Constant
    {
        public const string SPORT_ID_MAP = "{SportId}";

        public const string IS_LIVE_MAP = "{IsLive}";

        public const string BET_TYPE_ID_MAP = "{BetTypeId}";

        public const string ROOT_ELEMENT_PT_GRID = "SportTypes";

        public const string ROOT_ELEMENT_BETTING_LIMIT = "BettingLimitItems";

        public const string PT_GRID_NAME = "name";

        // Bet type properties
        public const string BET_TYPE_GROUP_ID = "id";

        public const string BET_TYPE_ID = "id";

        public const string BET_TYPE_NAME = "name";

        public const string BET_TYPE_RESOURCE_KEY = "resourcekey";

        public const string BET_TYPE_TITLE = "title";

        public const string BET_TYPE_COPY_FROM_BET_TYPE = "copyFromBetType";

        public const string BET_TYPE_COPY_FROM_SPORT_TYPE = "copyFromSportType";

        public const string BET_TYPE_VISIBLE = "visible";

        public const string BET_TYPE_MIN_WIDTH = "minWidth";

        public const string BET_TYPE_UPDATE = "updateminmaxbet";

        public const int BET_TYPE_INDEX_UPDATE_PARAMETER = 2;

        // Sport type properties
        public const string SPORT_TYPE_ID = "id";

        public const string SPORT_TYPE_NAME = "name";

        public const string SPORT_TYPE_KEY = "key";

        public const string SPORT_TYPE_TITLE = "title";

        public const string SPORT_TYPE_RESOURCE_KEY = "resourcekey";

        public const string SPORT_TYPE_COLLAPSED = "collapsed";

        public const string SPORT_TYPE_UPDATE = "updatepositiontaking";

        public const int SPORT_TYPE_INDEX_UPDATE_PARAMETER = 12;

        // Bet type group set properties
        public const string BETTYPE_GROUPSET_IS_LIVE = "isLive";

        public const string BETTYPE_GROUPSET_SHOW_LABLE = "showGroupSetLabel";

        public const int SPECIAL_SPORT_TYPE_ID = 99;

        public const string SPECIAL_SPORT_TYPE_STRING = "mp";

        public const string SPECIAL_SPORT_TYPE_NAME = "Mix Sports Parlay";

        public const int SPECIAL_TARGET_ROLE_ID = 4;

        public const int NUMBER_GAME_SPORT_TYPE_ID = 161;

        // Path to xml configuration
        public const string PT_GRID_CONFIG = "~/PTEngine/PTGrid.xml";

        public const string BETTING_LIMIT_CONFIG = "~/PTEngine/BettingLimit.xml";

        // Error message
        public const string ERROR_MESSAGE = "Error Operation.";

        public const string ERROR_CODE = "1000";

        public const int ADMIN_ROLEID = 5;

        public const int SUPER_ROLEID = 4;

        public const int MASTER_ROLEID = 3;

        public const int AGENT_ROLEID = 2;

        public const int MEMBER_ROLEID = 1;
    }
}