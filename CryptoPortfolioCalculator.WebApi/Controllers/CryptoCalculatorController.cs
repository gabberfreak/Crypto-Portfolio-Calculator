using CryptoPortfolio.Domain.Models;
using CryptoPortfolio.Service.Contracts;
using CryptoPortfolioCalculator.Web.Helpers;
using CryptoPortfolioCalculator.Web.Models;
using CryptoPortfolioCalculator.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CryptoPortfolioCalculator.Web.Controllers
{
    public class CryptoCalculatorController : Controller
    {

        private readonly ILogger<CryptoCalculatorController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICryptoMapperService _cryptoMapperService;
        private readonly ICallBackService _callBackService;
        public CryptoCalculatorController(
            ILogger<CryptoCalculatorController> logger,
            ICryptoMapperService cryptoMapperService,
            ICallBackService callBackService,
            IConfiguration configuration)
        {
            _logger = logger;
            _cryptoMapperService = cryptoMapperService;
            _callBackService = callBackService;
            _configuration = configuration;
        }

        // GET: CryptoCalculator
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

                MiddlewareSettings endpoints = _configuration?.GetSection("MiddlewareEndPoints")?.Get<MiddlewareSettings>();

                string[] readedFile = await FileReader.ReadFileAsync(model.File);
                var wallet = _cryptoMapperService.MapCryptoPortfoilioValues(readedFile);

                _logger.LogInformation("Successfully readed and mapped file: " + DateTime.Now);


                var statsResult = await _callBackService.CalculateStatsAsync(wallet, endpoints); //old

                _logger.LogInformation("Successfully synced api callbacks at: " + DateTime.Now);

                var viewModel = new CalculatorViewModel
                {
                    InitialValue = statsResult.InitialValue,
                    CoinChange = statsResult.CoinChange,
                    Overall = statsResult.Overall
                };

                TempData["stats"] = JsonSerializer.Serialize(viewModel);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on application: " + ex.Message + Environment.NewLine + "Thrown at: " + DateTime.Now + Environment.NewLine + "With error details: " + ex.StackTrace + Environment.NewLine + "Additional error information: " + ex.InnerException?.Message);
                throw ex;
            }
         }
    }
}
