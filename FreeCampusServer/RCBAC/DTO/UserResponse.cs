using RCBAC.Models;

namespace RCBAC.DTO
{
    public class UserResponse(User user)
    {
        public Guid Uuid { get; } = user.Uuid;

        public string Username { get; } = user.Username;

        public string DisplayName { get; } = user.DisplayName;

        public string Email { get; } = user.Email;
    }
}
