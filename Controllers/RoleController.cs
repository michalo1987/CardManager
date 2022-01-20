using CardManager.MapingActions.Interfaces;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CardManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleService _roleService;
        private readonly IMapingControllerActions _mapping;

        public RoleController(IRoleService roleService, IMapingControllerActions maping, RoleManager<IdentityRole> roleManager)
        {
            _roleService = roleService;
            _mapping = maping;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var roleViewModelList = new List<RoleViewModel>();
            var roleModelList = _roleService.GetAll();

            foreach (var model in roleModelList)
            {
                var viewModel = _mapping.MapRoleViewModelFromModel(model);
                roleViewModelList.Add(viewModel);
            }

            return View(roleViewModelList);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult New()
        {
            var viewModel = new RoleViewModel();

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New([FromForm]RoleViewModel viewModel)
        {
            if (await _roleManager.RoleExistsAsync(viewModel.RoleName))
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                ModelState.AddModelError(nameof(viewModel.RoleName), "Role already exists.");

                return View(viewModel);
            }
            if (ModelState.IsValid)
            {
                _roleService.CreateRole(viewModel.RoleName);

                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController));
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var role = _roleService.GetRole(id);

            if (role == null)
            {
                return NotFound($"Role ID = {id} does not exists.");
            }

            var viewModel = _mapping.MapRoleViewModelFromModel(role);

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]RoleViewModel viewModel)
        {
            var model = new RoleModel()
            {
                RoleId = viewModel.RoleId,
                RoleName = viewModel.RoleName
            };

            if (ModelState.IsValid)
            {
                _roleService.UpdateRole(model);
            }

            return RedirectToAction("Index");
        }
    }
}
