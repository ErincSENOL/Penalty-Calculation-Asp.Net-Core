using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PenaltyCalculation.Abstract;
using PenaltyCalculation.Data;
using PenaltyCalculation.Input;
using PenaltyCalculation.Models;
using PenaltyCalculation.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            SetDropDownListForCountry();
            ViewBag.Result = false;
            ViewBag.Today = DateTime.Today.ToString("yyyy-MM-dd");
            return View();
            
        }

        [HttpPost]
        public  IActionResult Index (PenaltyInputModel model)
        {

            if (ModelState.IsValid)
            {
                SetDropDownListForCountry();
                var _country = _context.Countries.Find(model.Country.Id);
                model.Country = _country;

                //model.Country.Holidays.Where(s => s.CountryId == model.Country.Id).ToList();
                model.Country.Holidays = _context.Holidays.Where(s => s.CountryId == model.Country.Id).ToList();

                PenaltyOutputModel _penaltyOutputModel = new PenaltyOutputModel();

                if (_country.CountryCode == "TR")
                {
                    TurkeyPenaltyCalculation turkeyPenaltyCalculation = new TurkeyPenaltyCalculation();
                    _penaltyOutputModel.BusinessDay = turkeyPenaltyCalculation.GetBusinessDays(model);
                    _penaltyOutputModel.PenaltyCount = Convert.ToDecimal(turkeyPenaltyCalculation.CalculateDay(_penaltyOutputModel.BusinessDay));
                }
                //else if (_country.CountryCode == "UK")
                //{
                //    TurkeyPenaltyCalculation turkeyPenaltyCalculation = new TurkeyPenaltyCalculation();
                //    _penaltyOutputModel.BusinessDay = turkeyPenaltyCalculation.GetBusinessDays(model);
                //    _penaltyOutputModel.PenaltyCount = Convert.ToDecimal(turkeyPenaltyCalculation.CalculateDay(_penaltyOutputModel.BusinessDay);

                //}
                if (_penaltyOutputModel != null)
                {
                    ViewBag.BusinessDays = _penaltyOutputModel.BusinessDay;
                    ViewBag.Penalty = _penaltyOutputModel.PenaltyCount;
                    ViewBag.Result = true;

                }
                return View(model);
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetDropDownListForCountry()
        {
            List<SelectListItem> ct = new List<SelectListItem>();
            foreach (var item in _context.Countries)
            {
                ct.Add(new SelectListItem { Text = item.Id.ToString(), Value = item.CountryName });
            }
            var _coutryList = ct.ToList();
            ViewBag.CoutriesList = _coutryList;
        }
    }
}
