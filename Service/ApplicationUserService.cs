using CardManager.Data;
using CardManager.MapingActions.Interfaces;
using CardManager.Service.Interfaces;
using CardManager.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace CardManager.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapingServiceActions _maping;

        public ApplicationUserService(ApplicationDbContext context, IMapingServiceActions maping)
        {
            _context = context;
            _maping = maping;
        }

        public IEnumerable<ApplicationUserModel> GetAll()
        {
            var userList = _context.ApplicationUser;
            var userRole = _context.UserRoles;
            var roles = _context.Roles;

            foreach (var model in userList)
            {
                var role = userRole.SingleOrDefault(u => u.UserId == model.Id);
                if (role != null)
                {
                    model.Role = roles.Single(r => r.Id == role.RoleId).Name;
                }

                var userModel = _maping.MapApplicationUserModelFromEntity(model);
                yield return userModel;
            }
        }
    }
}
