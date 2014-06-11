// ---------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.02.12">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Example.MVC.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;
    using System.Web.Mvc;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.Example.Data;
    using Fanex.BetList.Example.Extension.BetListExcelBuilderExt;
    using Fanex.BetList.Example.MVC.ViewModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Class HomeController.
    /// </summary>
    public class HomeController : Controller
    {
        private IList<ITicket> _tickets;
        private List<ITicketData> _ticketData;

        public HomeController()
        {
            string dataFile = HostingEnvironment.MapPath("~/App_Data/BetListDummyData.xls");
            TicketsExcelDAO ticketDAO = new TicketsExcelDAO(dataFile);
            _tickets = ticketDAO.GetTickets();
            _ticketData = ticketDAO.GetTicketData().ToList();
        }

        public ActionResult Index(BetListData model = null)
        {
            model = model == null ? new BetListData() : model;
            model.Tickets = _tickets.Where(x => x.BetTypeId == model.BetTypeId || model.BetTypeId == 0).ToList() ?? new List<ITicket>();
            model.TicketData = _ticketData ?? new List<ITicketData>();

            return View(model);
        }

        /// <summary>
        /// Exports the excel.
        /// </summary>
        public void ExportExcel()
        {
            GetCachePropertyById funcGetOddsTypeName = CacheRef.GetOddsTypeName;
            BetListExcelFactory factory = new BetListExcelFactory();
            BetListExcelBuilder builder = factory.CreateBuilder();
            ITicketHelper ticketHelper = new TicketHelper();
            short excelColumns = 6;

            builder.BuildWorksheet("BetList", excelColumns);
            int index = 0;

            foreach (ITicket ticket in _tickets)
            {
                index++;
                ticketHelper.Index = index;

                builder.AddNo(index)
                       .AddTrans(ticket, ticketHelper)
                       .AddUsername(ticket, ticketHelper)
                       .AddChoice(ticket, ticketHelper, _ticketData)
                       .AddOdds(ticket, _ticketData, funcGetOddsTypeName)
                       .AddStake(ticket);
            }

            MemoryStream ms = new MemoryStream();
            IWorkbook resultWorkbook = builder.GetBetList();
            resultWorkbook.Write(ms);
            ExportToExcel(ms, "BetList.xls");
        }

        private void ExportToExcel(MemoryStream excelStream, string fileName)
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", fileName));
            Response.Clear();
            Response.BinaryWrite(excelStream.GetBuffer());
            Response.End();
        }
    }
}
