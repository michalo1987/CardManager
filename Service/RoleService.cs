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

        public RoleModel CreateRole(string roleName)
        {
            var role = new IdentityRole()
            {
                Name = roleName
            };

            _contex.Roles.Add(role);
            _contex.SaveChanges();

            return _maping.MapRoleModelFromEntity(role);
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

        public RoleModel GetRole(string roleId)
        {
            var role = _contex.Roles
                .FirstOrDefault(r => r.Id == roleId);

            return role != null
                ? _maping.MapRoleModelFromEntity(role)
                : null;
        }

        public RoleModel UpdateRole(RoleModel model)
        {
            var role = _contex.Roles
                .SingleOrDefault(r => r.Id == model.RoleId);

            role.Name = model.RoleName;

            _contex.Roles.Update(role);
            _contex.SaveChanges();

            return model;
        }
    }
}
