using CardManager.MapingActions.Interfaces;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CardManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapingControllerActions _maping;

        public UserController(UserManager<IdentityUser> userManager, IApplicationUserService applicationUserService, IMapingControllerActions maping)
        {
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _maping = maping;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var userViewModelList = new List<ApplicationUserViewModel>();
            var userModelList = _applicationUserService.GetAll();

            foreach (var model in userModelList)
            {
                var viewModel = _maping.MapApplicationUserViewModelFromModel(model);
                userViewModelList.Add(viewModel);
            }

            return View(userViewModelList);
        }
    }
}
