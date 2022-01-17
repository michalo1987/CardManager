using CardManager.Service.Models;
using System.Collections.Generic;

namespace CardManager.Service.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleModel> GetAll();
    }
}
