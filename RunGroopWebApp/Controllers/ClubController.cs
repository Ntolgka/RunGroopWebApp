using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ClubModel> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ClubModel club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubVM = new CreateClubViewModel
            {
                AppUserId = curUserId
            };
            return View(createClubVM);
        }
        

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new ClubModel
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = clubVM.AppUserId,
                    Address = new AddressModel
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };
                _clubRepository.Create(club);
                return RedirectToAction("Index");
            } else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(clubVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AdressId = club.AddressId,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
                
            };

            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
                if (userClub != null)
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync(userClub.Image);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo.");
                        return View(clubVM);
                    }
                    var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

                    var club = new ClubModel
                    {
                        Id = id,
                        Title = clubVM.Title,
                        Description = clubVM.Description,
                        Image = photoResult.Url.ToString(),
                        AddressId = clubVM.AdressId,
                        Address = new AddressModel
                        {
                            Street = clubVM.Address.Street,
                            City = clubVM.Address.City,
                            State = clubVM.Address.State
                        }
                    };

                    _clubRepository.Update(club);

                    return RedirectToAction("Index");

                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
                return View("Edit", clubVM);
            }
            return View(clubVM);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetails = await _clubRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
            
            _clubRepository.Delete(clubDetails);
            await _photoService.DeletePhotoAsync(clubDetails.Image);
            return RedirectToAction("Index");   
        }
    }
}
