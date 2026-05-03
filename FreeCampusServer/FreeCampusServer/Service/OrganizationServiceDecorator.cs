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
            var courseEnrollmentStatusService = serviceProvider.GetRequiredService<ICourseEnrollmentStatusService>();
            var programEnrollmentStatusService = serviceProvider.GetRequiredService<IProgramEnrollmentStatusService>();

            var templateId = await organizationService.GetSingleOrDefaultIdByNameAsync(templateName)
                ?? throw new NoOrganizationTemplateFoundException();

            organization = await organizationService.CreateAsync(organization);

            var coursesStatuses = await courseEnrollmentStatusService.GetListByOrganizationIdAsync(templateId);
            foreach (var status in coursesStatuses)
            {
                var newStatus = new CourseEnrollmentStatus
                {
                    OrganizationId = templateId,
                    Name = status.Name,
                    Title = status.Title,
                    Description = status.Description,
                    IsActive = status.IsActive,
                };
                await courseEnrollmentStatusService.CreateAsync(newStatus);
            }

            var programStatuses = await programEnrollmentStatusService.GetListByOrganizationIdAsync(templateId);
            foreach (var status in programStatuses)
            {
                var newStatus = new ProgramEnrollmentStatus
                {
                    OrganizationId = templateId,
                    Name = status.Name,
                    Title = status.Title,
                    Description = status.Description,
                    IsActive = status.IsActive,
                };
                await programEnrollmentStatusService.CreateAsync(newStatus);
            }

            return organization;
        }
    }
}
