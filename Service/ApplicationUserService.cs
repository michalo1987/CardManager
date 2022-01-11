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
            var userModelList = new List<ApplicationUserModel>();
            var userList = _context.ApplicationUser.ToList();
            var userRole = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();

            foreach (var model in userList)
            {
                var role = userRole.FirstOrDefault(u => u.UserId == model.Id);
                if (role == null)
                {
                    model.Role = "None";
                }
                else
                {
                    model.Role = roles.FirstOrDefault(r => r.Id == role.RoleId).Name;
                }

                var userModel = _maping.MapApplicationUserModelFromEntity(model);
                userModelList.Add(userModel);
            }

            return userModelList;
        }
    }
}
