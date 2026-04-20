using RCBACEF.Models;

namespace RCBACEF.DTO
{
    public class RoleMinDTO(Role role)
    {
        public Guid Uuid { get; } = role.Uuid;
        public string Name { get; } = role.Name;
        public string DisplayName { get; } = role.DisplayName;
    }
}
