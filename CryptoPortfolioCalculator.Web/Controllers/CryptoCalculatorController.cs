using CryptoPortfolio.Domain.Models;
using CryptoPortfolio.Service.Contracts;
using CryptoPortfolioCalculator.Web.Helpers;
using CryptoPortfolioCalculator.Web.Hubs;
using CryptoPortfolioCalculator.Web.Models;
using CryptoPortfolioCalculator.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace CryptoPortfolioCalculator.Web.Controllers
{
    public class CryptoCalculatorController : Controller
    {

        private readonly ILogger<CryptoCalculatorController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICryptoMapperService _cryptoMapperService;
        private readonly ICallBackService _callBackService;
        private readonly IHubContext<CalculatePortfolioPercentChangeHub> _calculatorHub;
        private readonly MiddlewareSettings endpoints;
        public CryptoCalculatorController(
            ILogger<CryptoCalculatorController> logger,
            ICryptoMapperService cryptoMapperService,
            ICallBackService callBackService,
            IConfiguration configuration,
            IHubContext<CalculatePortfolioPercentChangeHub> calculatorHub)
        {
            _logger = logger;
            _cryptoMapperService = cryptoMapperService;
            _callBackService = callBackService;
            _configuration = configuration;
            _calculatorHub = calculatorHub;
            endpoints= _configuration?.GetSection("MiddlewareEndPoints")?.Get<MiddlewareSettings>();
        }

        public ActionResult Index()
        { 
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GenerateStats()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction();

            }
            var viewModel = new CalculatorViewModel();

            if (TempData["stats"] != null)
            {
                viewModel = JsonSerializer.Deserialize<CalculatorViewModel>(TempData["stats"].ToString());
            }

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateStats(FileModel model)
        {
            try
            {
                if (!ModelState.IsValid || model.File == null || model.File.Length == 0)
                {
                    return RedirectToAction();
                }

                string[] readedFile = await FileReader.ReadFileAsync(model.File);
                var wallet = _cryptoMapperService.MapCryptoPortfoilioValues(readedFile);

                _logger.LogInformation("Successfully readed and mapped file: " + DateTime.Now);

                var statsResult = await _callBackService.CalculateStatsAsync(wallet, endpoints); 

                _logger.LogInformation("Successfully synced api callbacks at: " + DateTime.Now);

                var viewModel = new CalculatorViewModel
                {
                    InitialValue = statsResult.InitialValue,
                    CoinChange = statsResult.CoinChange,
                    Overall = statsResult.Overall
                };

                TempData["file"] = JsonSerializer.Serialize(wallet);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on application: " + ex.Message + Environment.NewLine + "Thrown at: " + DateTime.Now + Environment.NewLine + "With error details: " + ex.StackTrace + Environment.NewLine + "Additional error information: " + ex.InnerException?.Message);
                throw ex;
            }
         }

        [HttpGet]
        public async Task<IActionResult> GetInitialPortfolioValue()
        {
            var model = JsonSerializer.Deserialize<List<CryptoWalletModel>>(TempData["file"].ToString());
            TempData.Keep("file");

            var statsResult = await _callBackService.CalculateStatsAsync(model, endpoints);

            await _calculatorHub.Clients.All.SendAsync("AppendValues", statsResult);

            return Ok(statsResult);
        }
    }
}
