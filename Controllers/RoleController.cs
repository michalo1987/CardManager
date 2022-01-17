using CardManager.MapingActions.Interfaces;
using CardManager.Models.ViewModels;
using CardManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CardManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMapingControllerActions _mapping;

        public RoleController(IRoleService roleService, IMapingControllerActions maping)
        {
            _roleService = roleService;
            _mapping = maping;
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
    }
}
