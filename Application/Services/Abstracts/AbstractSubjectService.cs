using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.DTOs.SubjectRequisites;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Abstracts
{
    public abstract class AbstractSubjectService(
        ISubjectRepository subjectRepository,
        ICreateValidator<Subject> createValidator,
        IUpdateValidator<Subject> updateValidator
    ) : Service<Subject, Guid>(createValidator, updateValidator, subjectRepository), ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository = subjectRepository;

        public async Task<bool> AssignPrograms(SubjectProgramsDTO programs, Guid tenantId)
        {
            var programsList = ConvertToProgramList(programs, tenantId);
            var result = await _subjectRepository.AssignPrograms(programsList);
            return result;
        }

        public async Task<bool> AssignRequisites(SubjectRequisitesDTO requisites, Guid tenantId)
        {
            var requisitesList = ConvertToRequisiteList(requisites, tenantId);
            var result = await _subjectRepository.AssignRequisites(requisitesList);
            return result;
        }

        public async Task<bool> RevokePrograms(SubjectProgramsDTO programs, Guid tenantId)
        {
            var programsList = ConvertToProgramList(programs, tenantId);
            var result = await _subjectRepository.RevokePrograms(programsList);
            return result;
        }

        public async Task<bool> RevokeRequisites(SubjectRequisitesDTO requisites, Guid tenantId)
        {
            var requisitesList = ConvertToRequisiteList(requisites, tenantId);
            var result = await _subjectRepository.RevokeRequisites(requisitesList);
            return result;
        }

        private List<SubjectRequisite> ConvertToRequisiteList(
            SubjectRequisitesDTO requisites,
            Guid tenantId
        )
        {
            if (requisites.RequisitesIds == null || !requisites.RequisitesIds.Any())
                throw new ArgumentNullException(nameof(requisites), "Empty requirements");

            var requisitesList = new List<SubjectRequisite>();

            foreach (var requisite in requisites.RequisitesIds)
            {
                if (requisite != requisites.SubjectId)
                {
                    var auxiliar = new SubjectRequisite
                    {
                        SubjectId = requisites.SubjectId,
                        RequisteId = requisite,
                        TenantId = tenantId,
                    };
                    requisitesList.Add(auxiliar);
                }
            }
            return requisitesList;
        }

        private List<SubjectProgram> ConvertToProgramList(
            SubjectProgramsDTO programs,
            Guid tenantId
        )
        {
            if (programs.ProgramsIds == null || !programs.ProgramsIds.Any())
                throw new ArgumentNullException(nameof(programs), "Empty programs");

            var programsList = new List<SubjectProgram>();

            foreach (var program in programs.ProgramsIds)
            {
                var auxiliar = new SubjectProgram
                {
                    SubjectId = programs.SubjectId,
                    AcademicProgramId = program,
                    TenantId = tenantId,
                };
                programsList.Add(auxiliar);
            }
            return programsList;
        }
    }
}
