using Microsoft.AspNetCore.Mvc;

namespace Fanex.BetList.Example.MVC.Controllers
{
    public class WhoAmIController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ServerName = Request.Host.Host;
            ViewBag.Port = Request.Host.Port;
            ViewBag.ServerIP = Request.HttpContext.Connection.LocalIpAddress?.ToString();
            ViewBag.ServerTime = DateTime.Now;
            ViewBag.ClientIP = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            ViewBag.ForwardedFor = Request.Headers["X-Forwarded-For"].FirstOrDefault();
            ViewBag.RealIP = Request.Headers["X-Real-IP"].FirstOrDefault();
            ViewBag.Via = Request.Headers["Via"].FirstOrDefault();
            ViewBag.ProxyConnection = Request.Headers["Proxy-Connection"].FirstOrDefault();
            
            return View();
        }
    }
}
