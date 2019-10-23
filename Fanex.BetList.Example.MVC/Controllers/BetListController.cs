namespace Fanex.BetList.Example.MVC.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.Example.Extension.BetListExcelBuilderExt;
    using Fanex.BetList.Example.MVC.Models;
    using Fanex.BetList.Example.MVC.Services;
    using Fanex.BetList.Example.MVC.ViewModel;

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
            var betTypes = _dataService.QueryBetTypes(2701, 2702, 2703, 2704, 2705, 2706, 2707, 2799, 2801, 2802, 2803, 2804, 2805, 2806, 2807, 2808, 2809, 2811, 2812);

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

            for (var betTypeId = 4101; betTypeId <= 4120; betTypeId++)
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
            GetCachePropertyById funcGetOddsTypeName = ticketHelper.GetOddsTypeNameById;
            const short excelColumns = 11;
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
                           .AddUsername(ticket, ticketHelper)
                           .AddChoice(ticket, ticketHelper, ticketData)
                           .AddOdds(ticket, ticketData, funcGetOddsTypeName)
                           .AddOddsType(ticket, ticketData, funcGetOddsTypeName)
                           .AddOddsValue(ticket, ticketData, funcGetOddsTypeName)
                           .AddStatus(ticket, ticketHelper, ticketData, true)
                           .AddStake(ticket);
                }

                var ms = new MemoryStream();
                var resultWorkbook = builder.GetBetList();
                resultWorkbook.Write(ms);
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