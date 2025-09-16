namespace Fanex.BetList.Example.MVC.Controllers
{
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.Example.Extension.BetListExcelBuilderExt;
    using Fanex.BetList.Example.MVC.Models;
    using Fanex.BetList.Example.MVC.Services;
    using Fanex.BetList.Example.MVC.ViewModel;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;

    public class BetListController : Controller
    {
        private readonly DataService _dataService;
        private const string DefaultLanguage = "en-US";

        private string DataSourcePath
        {
            get { return Session["DataSourcePath"].ToString(); }
            set { Session["DataSourcePath"] = value; }
        }

        public BetListController()
        {
            _dataService = new DataService();
        }

        public ActionResult All(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BetListDummyData.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return View(model);
        }

        public ActionResult LCProduct(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(12550, 12599);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/LCProduct.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult RNGProduct(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(12300, 12301);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/RNGProduct.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SabaKeno(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(12300,12301);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/SabaKeno.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult VourcherId(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/VoucherId.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        
        public ActionResult VirtualSports5(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 11050; betTypeId <= 11099; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/VirtualSports5.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BaseballAIO(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 8800; betTypeId <= 8899; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BaseballAIO.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult ParlayBoost(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(29);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/ParlayBoost.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SabaxD(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(10801, 10802);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/SabaxD.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult TaraPlaysxD(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(10801, 10802);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/SabaxD.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult VirtualSports3(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(5302, 5303, 5304);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/VirtualSports3.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult VirtualSports4(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 10400; betTypeId <= 10449; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/VirtualSports4.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PlayerTips(BetListData model = null)
        {

            var betTypes = _dataService.QueryBetTypes(8700);

            DataSourcePath = "~/App_Data/PlayerTips.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult ESports4(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 9089; betTypeId <= 9138; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/ESports4.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Bitcoin(BetListData model = null)
        {

            var betTypes = _dataService.QueryBetTypes(7901, 7902);

            DataSourcePath = "~/App_Data/Bitcoin.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult VGaming(BetListData model = null)
        {

            var betTypes = _dataService.QueryBetTypes(3802, 3820, 3829, 3898, 3899);

            DataSourcePath = "~/App_Data/VGaming.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Habanero(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 7001; betTypeId <= 7300; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/Habanero.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult CG(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 7301; betTypeId <= 7600; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/CG.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult AdvantPlay(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 7701; betTypeId <= 7799; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/AdvantPlay.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult AdvantPlayMini(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 7801; betTypeId <= 7899; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/AdvantPlayMini.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        public ActionResult CricketAIO(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 9400; betTypeId <= 9606; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/CricketAIOP1.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult CricketAIO2(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 9440; betTypeId <= 9739; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/CricketAIOP2.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SportsLottery(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 3611; betTypeId <= 3620; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);

            DataSourcePath = $"~/App_Data/SportsLottery.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult MaxGame(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 5401; betTypeId <= 5700; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/MaxGame.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Happy5LinkedGames(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 3601; betTypeId <= 3610; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/Happy5LinkedGames.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult LeapGaming(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 3501; betTypeId <= 3599; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/LeapGaming.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }


        public ActionResult GDG(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 5701; betTypeId <= 5799; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/GDG.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BG(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/BG.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(29));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult GodOfFortune(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/GodOfFurtune.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(29));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        public ActionResult WM(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 3401; betTypeId <= 3499; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/WM.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PlaytechRNG(BetListData model = null)
        {

            var betTypes = _dataService.QueryBetTypes(15600);

            DataSourcePath = "~/App_Data/Playtech.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PlaytechLive(BetListData model = null)
        {

            var betTypes = _dataService.QueryBetTypes(15600);

            DataSourcePath = "~/App_Data/PlaytechLive.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult MyanmarOdds(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();
            ViewBag.BetTypes = CreateSelectListItem(betTypes);

            DataSourcePath = "~/App_Data/MyanmarOdds.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult TimeMachine(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();
            ViewBag.BetTypes = CreateSelectListItem(betTypes);

            DataSourcePath = "~/App_Data/TimeMachine.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Joker(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 6401; betTypeId <= 6700; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/Joker.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PGSoft(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 6001; betTypeId <= 6300; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/PGSoft.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BasketballAIO2(BetListData model = null)
        {
            var bettypeIds = new int[50];
            for (var i = 0; i <= 29; i++)
            {
                bettypeIds[i] = i + 618;
            }
            var betTypes = _dataService.QueryBetTypes(bettypeIds);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BasketballAIOP2.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult LicenseeId(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();
            DataSourcePath = "~/App_Data/LicenseeId.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return View("LicenseeId", model);
        }

        public ActionResult RNGWAR(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(5500);
            DataSourcePath = "~/App_Data/RNGWAR.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BBIN(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(3300);
            DataSourcePath = "~/App_Data/BBIN.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult DelayPayout(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/DelayPayout.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(1, 2, 3));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult IONCasino(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(5601, 5602, 5603, 5604);
            DataSourcePath = "~/App_Data/IONCasino.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult GTCasino(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(3201, 3202, 3203, 3204, 3205, 3206);
            DataSourcePath = "~/App_Data/GTCasino.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult TableGames(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 4901; betTypeId <= 5000; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);

            DataSourcePath = $"~/App_Data/{nameof(TableGames)}.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SAGaming(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(3001, 3002, 3003, 3004, 3005, 3006, 3007);
            DataSourcePath = "~/App_Data/SAGaming.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Indo4D(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(5001, 5002, 5003, 5004, 5005, 5006, 5007);
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/Indo4D.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SabaClub(BetListData model = null)
        {

            var betTypes = _dataService.QueryBetTypes(1612, 1634, 1640, 1698, 1699);

            DataSourcePath = "~/App_Data/SabaClub.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        public ActionResult SexyGaming(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(3101, 3102, 3103, 3104, 3105, 3149, 3150);
            DataSourcePath = "~/App_Data/SexyGaming.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BetRadar(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(133, 134, 135, 140, 141, 145, 146, 147, 148, 149, 150, 151, 152);
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BetListDummyData.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult New11BetTypes(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(128, 129, 130, 131, 136, 137, 138, 139, 143, 144, 14, 127, 16, 6, 126);
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/11NewBetTypeData.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult New60BetTypesBetRadar(BetListData model = null)
        {
            var betTypes = new List<BetType>();
            var ticketHelper = new TicketHelper();
            betTypes.Add(new BetType
            {
                Id = 157,
                Name = ticketHelper.GetBetTypeNameById(157)
            });

            for (int i = 159; i <= 217; i++)
            {
                betTypes.Add(new BetType
                {
                    Id = i,
                    Name = ticketHelper.GetBetTypeNameById(i)
                });
            }

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/60NewBetTypeBetRadarData.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SportingSolutionBetTypes(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(153, 154, 155, 156, 1301, 1302, 1303, 1305, 1306, 1311, 1312, 1316, 1317, 1318, 1324);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/SportingSolutionBetTypesData.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult GoldDeluxe(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(1701, 1702, 1703, 1704, 1705);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/GoldDeluxe.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult MetricGaming(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(801, 803, 804, 805, 806);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/MetricGaming.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Allbet(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(2401, 2402, 2403, 2404, 2405, 2406);
            DataSourcePath = "~/App_Data/Allbet.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult AGCasino(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(1801, 1802, 1803, 1804);

            DataSourcePath = "~/App_Data/AGCasino.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult ColossusBets(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(18000, 18001, 18002, 18003, 18004, 18005);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/ColossusBets.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult ColosussBetsDetails()
        {
            var betListData = GetDataSource(new BetListData(), sourcePath: "~/App_Data/ColossusBetsDetails.xls");

            return View(betListData);
        }

        public ActionResult GlobalGaming(BetListData model = null)
        {
            ViewBag.BetTypes = new List<SelectListItem>();

            DataSourcePath = "~/App_Data/GlobalGaming.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BetTrade(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(901, 902);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BetTrade.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PHAllInOne(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(4, 30, 151, 159, 161, 162, 171, 191, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414, 461, 462, 463, 464);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/PHAllInOne.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PHAllInOneP2(BetListData model = null)
        {
            var betTypes = new List<BetType>();
            for (var betTypeId = 415; betTypeId <= 447; betTypeId++)
            {
                betTypes.AddRange(_dataService.QueryBetTypes(betTypeId));
            }

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/PHAllInOneP2.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PHAllInOneP2b(BetListData model = null)
        {
            ViewBag.BetTypes = new List<SelectListItem>();
            DataSourcePath = "~/App_Data/PHAllInOneP2b.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult RnGCasino(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/RnGCasinoSingleWallet.xls";

            ViewBag.BetTypes = CreateRNGDropDown();
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        private IEnumerable<SelectListItem> CreateRNGDropDown()
        {
            var betTypes = new List<BetType>();

            for (var betType = 2101; betType <= 2177; betType++)
            {
                betTypes.AddRange(_dataService.QueryBetTypes(betType));
            }

            for (var betType = 2201; betType <= 2206; betType++)
            {
                betTypes.AddRange(_dataService.QueryBetTypes(betType));
            }

            for (var betType = 2301; betType <= 2308; betType++)
            {
                betTypes.AddRange(_dataService.QueryBetTypes(betType));
            }

            return CreateSelectListItem(betTypes);
        }

        public ActionResult FastMarkets(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/FastMarkets.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(221, 222, 223, 224, 225));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Voidbridge(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/VoidBridge.xls";

            ViewBag.BetTypes = new List<SelectListItem>();
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult NumberGame(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(81, 82, 83, 84, 85, 86, 87, 88, 89, 90);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/NumberGame.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult LiveSports(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/LiveSports.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult VirtualSports(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(1201, 1203, 1204, 1205, 1206, 1220, 1224, 1231, 1232, 1233, 1235, 1236);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/VirtualSports.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BasketballAIO(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(601, 602, 603, 604, 605, 606, 607, 608, 609, 610, 611, 612, 613, 614, 615, 616, 617);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BasketballAIO.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult CashOut(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(911);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/CashOut.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BetRadarVirtualSports(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(2701, 2702, 2703, 2704, 2705, 2706, 2707, 2799, 2801, 2802, 2803, 2804, 2805, 2806, 2807, 2808, 2809, 2811, 2812, 2813);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BetRadarVirtualSports.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult KenoLottery(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 1031; betTypeId <= 1048; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/KenoLottery.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Keno(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(1501, 1515, 1516);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/Keno.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PHAIO3(BetListData model = null)
        {
            var betTypeIds = new List<int> { 22 };

            for (var betTypeId = 448; betTypeId <= 460; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/PHAllInOneP3.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BonusSystem(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BonusSystem.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult ChangingToHomeAway(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(24, 410, 1224, 151, 143, 163, 144, 171, 172, 415, 186, 431, 408, 426, 601, 602, 608, 614);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/ChangingToHomeAway.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult CorrectTranslations(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(16, 28, 453, 408, 171, 426, 416, 430, 177);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/CorrectTranslations.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult OneRnGCasino(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(2178, 2319, 2919);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/OneRnGCasino.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult FastMarkets2(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/FastMarkets2.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(221, 222, 223, 224, 225, 226, 227));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Asian1x2HDP(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/Asian1x2HDP.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(465, 466));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult UpcomingSports(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/UpcomingSports.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(1, 2, 3, 10, 20, 29));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PP(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 4101; betTypeId <= 4300; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/PP.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Parlay(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/Parlay.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(29));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BetBuilder(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/BetBuilder.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(38));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult CardClub(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 1901; betTypeId <= 1999; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/CardClub.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SettlementStatus(BetListData model = null)
        {
            DataSourcePath = "~/App_Data/SettlementStatus.xls";

            ViewBag.BetTypes = CreateSelectListItem(_dataService.QueryBetTypes(1, 2, 3));
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult ESports2(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 9001; betTypeId <= 9086; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/ESports2.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Happy5(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 8101; betTypeId <= 8105; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/Happy5.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult ESports2InPlay(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 9001; betTypeId <= 9086; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/ESports2InPlay.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult AbnormalBet(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 9001; betTypeId <= 9086; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);

            DataSourcePath = "~/App_Data/AbnormalBet.xls";

            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PHAIO4A(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(467, 468, 469);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/PHAllInOneP4A.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult RefundResult(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/RefundResult.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult RebateBonus(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/RebateBonus.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult PHAIO4B(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 470; betTypeId <= 497; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/PHAllInOneP4B.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult NonPeakHoursPromotion(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/NonPeakHoursPromotion.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult BadmintonAIO(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 701; betTypeId <= 714; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BadmintonAIO.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult VirtualGames(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(4601);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/VirtualGames.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PHAIO5(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 381; betTypeId <= 400; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/PHAllInOneP5.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PHAIO51(BetListData model = null)
        {
            var betTypeIds = new List<int>() { 367 };
            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/PHAllInOneP51.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult SG(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 4301; betTypeId <= 4326; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/SG.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult PragmaticLiveCasino(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes(3701, 3702, 3703, 3704, 3705, 3706, 3707, 3708, 3709);
            DataSourcePath = "~/App_Data/PragmaticLiveCasino.xls";

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult FGG(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 8202; betTypeId <= 8299; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }
            DataSourcePath = "~/App_Data/FGG.xls";

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult VolleyballAIO(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 3900; betTypeId <= 3917; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/VolleyballAIO.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult MGRNGCasino(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 8400; betTypeId <= 8699; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/Microgaming.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        public ActionResult SealEntertainment(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 8900; betTypeId <= 8999; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/SealEntertainment.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Jili(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 5100; betTypeId <= 5299; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/Jili.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult YeeBet(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 3151; betTypeId <= 3200; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());

            DataSourcePath = "~/App_Data/YeeBet.xls";
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            model = GetDataSource(model, DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult BasketballAIO3(BetListData model = null)
        {
            var bettypeIds = new int[4] { 647, 648, 649, 650 };
            var betTypes = _dataService.QueryBetTypes(bettypeIds);

            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/BasketballAIOP3.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);

            return BetListView(model);
        }

        public ActionResult MaxPayout(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/MaxPayout.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Live22(BetListData model = null)
        {
            var betTypes = _dataService.QueryBetTypes();
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/Live22.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult WE(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 10300; betTypeId <= 10399; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/WE.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult UUSlots(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 10000; betTypeId <= 10299; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/UUSlots.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult FastSpin(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 11100; betTypeId <= 11299; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }
            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/FastSpin.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        
        public ActionResult FACHAI(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 11300; betTypeId <= 11399; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/FACHAI.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Nextspin(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 10450; betTypeId <= 10750; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }
            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/Nextspin.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

	   public ActionResult ONCasino(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 11850; betTypeId <= 11899; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/ONCasino.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult GPILC(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 11500; betTypeId <= 11599; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/GPILC.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult Betby(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 13000; betTypeId <= 13011; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/VS6Betby.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        public ActionResult ASKMESLOT(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 15000; betTypeId <= 15199; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/ASKMESLOT.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        public ActionResult WCasino(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 12000; betTypeId <= 12099; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/WCasino.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }
        public ActionResult AIAndClot(BetListData model = null)
        {
            var betTypeIds = new List<int>();

            for (var betTypeId = 16100; betTypeId <= 16299; betTypeId++)
            {
                betTypeIds.Add(betTypeId);
            }

            var betTypes = _dataService.QueryBetTypes(betTypeIds.ToArray());
            ViewBag.BetTypes = CreateSelectListItem(betTypes);
            DataSourcePath = "~/App_Data/AIClot.xls";
            model = GetDataSource(model, sourcePath: DataSourcePath);
            model.DataFilePath = DataSourcePath;

            return BetListView(model);
        }

        private ViewResult BetListView(BetListData model)
        {
            return View("All", model);
        }

        private IEnumerable<SelectListItem> CreateSelectListItem(IEnumerable<BetType> betTypes)
        {
            return betTypes.Select(betType => new SelectListItem { Text = betType.Name, Value = betType.Id.ToString() });
        }

        private BetListData GetDataSource(BetListData model, string sourcePath)
        {
            if (model != null && !string.IsNullOrWhiteSpace(model.Language) && model.Language != DefaultLanguage)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(model.Language);
            }

            BetListData betListData = _dataService.GetBetListData(sourcePath, model.BetTypeId);

            // check whether request filter by bet type id, and does not contain select all
            if (model.BetTypeId != 0)
            {
                model.Tickets = betListData
                    .Tickets
                    .Where(ticket => model.BetTypeId == ticket.BetTypeId)
                    .Select(ticket => ticket)
                    .ToList();

                model.TicketData = betListData
                  .TicketData
                  .Where(ticketData => model.BetTypeId == ticketData.BetTypeId)
                  .Select(ticket => ticket)
                  .ToList();

                model.TicketData = model.TicketData ?? new List<ITicketData>();
                model.Tickets = model.Tickets ?? new List<ITicket>();
            }
            else
            {
                model.TicketData = betListData.TicketData ?? new List<ITicketData>();
                model.Tickets = betListData.Tickets ?? new List<ITicket>();
            }

            return model;
        }

        public void ExportExcel(BetListData query)
        {
            var factory = new BetListExcelFactory();
            BetListExcelBuilder builder = factory.CreateBuilder();
            ITicketHelper ticketHelper = new TicketHelper();
            ticketHelper.HideSabaText = query.HideSabaText;
            GetCachePropertyById funcGetOddsTypeName = ticketHelper.GetOddsTypeNameById;
            const short excelColumns = 12;
            builder.BuildWorksheet("BetList", excelColumns);
            var index = 0;

            var model = GetDataSource(query, sourcePath: DataSourcePath);
            var tickets = model.Tickets;
            var ticketData = model.TicketData;

            if (tickets != null)
            {
                foreach (ITicket ticket in tickets)
                {
                    index++;
                    ticketHelper.Index = index;

                    builder.AddNo(index)
                           .AddTrans(ticket, ticketHelper)
                           .AddTransTime(ticket, ticketHelper)
                           .AddRefNo(ticket, ticketHelper)
                           .AddLicenseeTxId(ticket, ticketHelper)
                           .AddUsername(ticket, ticketHelper)
                           .AddChoice(ticket, ticketHelper, ticketData)
                           .AddOdds(ticket, ticketData, funcGetOddsTypeName)
                           .AddOddsType(ticket, ticketData, funcGetOddsTypeName)
                           .AddOddsValue(ticket, ticketData, funcGetOddsTypeName)
                           .AddStatus(ticket, ticketHelper, ticketData, true)
                           .AddStake(ticket, ticketHelper);
                }

                var ms = new MemoryStream();
                var resultWorkbook = builder.GetBetList();
                resultWorkbook.Write(ms, true);
                ExportToExcel(ms, "BetList.xls");
            }
        }

        private void ExportToExcel(MemoryStream excelStream, string fileName)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
            Response.Clear();
            Response.BinaryWrite(excelStream.GetBuffer());
            Response.End();
        }

        public void DownloadExcelFile(string dataFilePath)
        {
            FileInfo file = new FileInfo(Server.MapPath(dataFilePath));

            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Type", "application/Excel");
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.End();
            }
        }
    }
}