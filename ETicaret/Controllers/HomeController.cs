using ETicaret.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    public class HomeController : Controller
    {
		private readonly IHubContext<ChatHub> _hubContext;

		public HomeController(IHubContext<ChatHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendMessage(string user, string message)
		{
			await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
			return RedirectToAction("Index");
		}
	}
}
