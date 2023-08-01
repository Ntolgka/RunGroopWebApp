using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using RunGroopWebApp.Services;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<RaceModel> races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            RaceModel race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createRaceVM = new CreateRaceViewModel
            {
                AppUserId = curUserId
            };
            return View(createRaceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                bool raceExists = _raceRepository.Exists(raceVM.Title, raceVM.Description);

                if (!raceExists)
                {
                    try
                    {
                        var result = await _photoService.AddPhotoAsync(raceVM.Image);

                        var race = new RaceModel
                        {
                            Title = raceVM.Title,
                            Description = raceVM.Description,
                            Image = result.Url.ToString(),
                            AppUserId = raceVM.AppUserId,
                            Address = new AddressModel
                            {
                                Street = raceVM.Address.Street,
                                City = raceVM.Address.City,
                                State = raceVM.Address.State
                            }
                        };

                        _raceRepository.Create(race);

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "An error occurred while creating the race.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The race already exists.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please correct the errors in the form.");
            }

            return View(raceVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AdressId = race.AddressId,
                Address = race.Address,
                URL = race.Image,
                RaceCategory = race.RaceCategory

            };

            return View(raceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);
                if (userRace != null)
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(userRace.Image);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo.");
                        return View(raceVM);
                    }
                    var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);

                    var race = new RaceModel
                    {
                        Id = id,
                        Title = raceVM.Title,
                        Description = raceVM.Description,
                        Image = photoResult.Url.ToString(),
                        AddressId = raceVM.AdressId,
                        Address = new AddressModel
                        {
                            Street = raceVM.Address.Street,
                            City = raceVM.Address.City,
                            State = raceVM.Address.State
                        }       
                    };

                    _raceRepository.Update(race);

                    return RedirectToAction("Index");

                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View("Edit", raceVM);
            }
            return View(raceVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var raceDetails = await _raceRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");
            return View(raceDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var raceDetails = await _raceRepository.GetByIdAsync(id);
            if (raceDetails == null) return View("Error");

            _raceRepository.Delete(raceDetails);
            await _photoService.DeletePhotoAsync(raceDetails.Image);
            return RedirectToAction("Index");
        }
    }
}
