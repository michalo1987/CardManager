using CardManager.MapingActions.Interfaces;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CardManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IMapingControllerActions _maping;

        public UserController(IApplicationUserService applicationUserService, IMapingControllerActions maping)
        {
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
