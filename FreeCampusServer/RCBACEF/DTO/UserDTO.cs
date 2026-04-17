using RCBACEF.Models;

namespace RCBACEF.DTO
{
    public class UserDTO(User user) : UserMinDTO(user)
    {
        public string Email { get; } = user.Email;

        public bool IsActive { get; } = user.IsActive;
        public bool CanLogin { get; } = user.CanLogin;
        public DateTime? LastLogin { get; } = user.LastLogin;

        public DateTime CreatedAt { get; } = user.CreatedAt;
        public DateTime UpdatedAt { get; } = user.UpdatedAt;
        public DateTime? DeletedAt { get; } = user.DeletedAt;

        public UserMinDTO? CreatedBy { get; } = user.CreatedBy != null ? new UserMinDTO(user.CreatedBy) : null;
        public UserMinDTO? UpdatedBy { get; } = user.UpdatedBy != null ? new UserDTO(user.UpdatedBy) : null;
        public UserMinDTO? DeleteBy { get; } = user.DeletedBy != null ? new UserDTO(user.DeletedBy) : null;
    }
}
