using RCBACEF.Models;

namespace RCBACEF.DTO
{
    public class CompanyMinDTO(Company company)
    {
        public Guid Uuid { get; } = company.Uuid;
        public string Name { get; } = company.Name;
    }
}
