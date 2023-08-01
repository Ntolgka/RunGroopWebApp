using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        public void MapUserEdit(AppUserModel user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.Name = editVM.Name;
            user.Surname = editVM.Surname;
            user.AboutMe = editVM.AboutMe;
            user.Pace = editVM.Pace;
            user.Mileage = editVM.MileAge;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editVM.City;
            user.State = editVM.State;
        }

        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardVM = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs,
            };
            return View(dashboardVM);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserVM = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                Name = user.Name,
                Surname = user.Surname,
                AboutMe = user.AboutMe,
                Pace = user.Pace,
                MileAge = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State
            };
            return View(editUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View("EditUserProfile", editVM);
            }

            AppUserModel user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if (user.ProfileImageUrl == null)
            {
                try
                {
                    var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                    if (photoResult == null)
                    {
                        ModelState.AddModelError("", "Unsupported image file. Only JPG, JPEG, and PNG formats are allowed.");
                        return View("EditUserProfile", editVM);
                    }

                    MapUserEdit(user, editVM, photoResult);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View("EditUserProfile", editVM);
                }
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete the photo: " + ex.Message);
                }

                try
                {
                    var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                    MapUserEdit(user, editVM, photoResult);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View("EditUserProfile", editVM);
                }
            }

            _dashboardRepository.Update(user);
            return RedirectToAction("Index");
        }
    }
}
