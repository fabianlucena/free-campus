using FreeCampusServer.Entities;
using FreeCampusServer.Exceptions;
using FreeCampusServer.IServices;
using RFRGOBACEntities.Entities;
using RFRGOBACIServices.IServices;
using RFRGOBACServices.Decorators;

namespace FreeCampusServer.Service
{
    public class OrganizationServiceDecorator(
        IOrganizationService _organizationService,
        IServiceProvider serviceProvider
    )
        : OrganizationServiceDecoratorBase(_organizationService),
        IOrganizationService
    {
        private readonly IOrganizationService organizationService = _organizationService;

        public async Task<Organization> CreateAsync(Organization organization, string templateName)
        {
            var courseStatusService = serviceProvider.GetRequiredService<ICourseStatusService>();

            var templateId = await organizationService.GetSingleOrDefaultIdByNameAsync(templateName)
                ?? throw new NoOrganizationTemplateFoundException();

            organization = (Organization)await organizationService.CreateAsync(organization);

            var statuses = await courseStatusService.GetListByOrganizationIdAsync(templateId);
            foreach (var status in statuses)
            {
                var newStatus = new CourseStatus
                {
                    OrganizationId = templateId,
                    Name = status.Name,
                    Title = status.Title,
                    Description = status.Description,
                    IsActive = status.IsActive,
                };
                await courseStatusService.CreateAsync(newStatus);
            }

            return organization;
        }
    }
}
