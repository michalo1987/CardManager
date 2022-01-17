using CardManager.Data;
using CardManager.MapingActions.Interfaces;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _contex;
        private readonly IMapingServiceActions _maping;

        public RoleService(ApplicationDbContext context, IMapingServiceActions maping)
        {
            _contex = context;
            _maping = maping;
        }

        public IEnumerable<RoleModel> GetAll()
        {
            var roleModelList = new List<RoleModel>();
            var roleList = _contex.Roles.ToList();

            foreach (var model in roleList)
            {
                var roleModel = _maping.MapRoleModelFromEntity(model);
                roleModelList.Add(roleModel);
            }

            return roleModelList;
        }
    }
}
