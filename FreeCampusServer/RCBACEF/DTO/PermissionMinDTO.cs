using RCBACEF.Models;

namespace RCBACEF.DTO
{
    public class PermissionMinDTO(Permission permission)
    {
        public Guid Uuid { get; } = permission.Uuid;
        public string Name { get; } = permission.Name;
    }
}
