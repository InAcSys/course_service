using CourseService.Application.Services.Interfaces;
using CourseService.Application.Validators.Interfaces;
using CourseService.Domain.DTOs.ProgramLevels;
using CourseService.Domain.DTOs.ProgramSubjects;
using CourseService.Domain.Entities.Concretes;
using CourseService.Infrastructure.Repositories.Interfaces;

namespace CourseService.Application.Services.Abstracts
{
    public abstract class AbstractProgramService(
        ICreateValidator<AcademicProgram> createValidator,
        IUpdateValidator<AcademicProgram> updateValidator,
        IProgramRepository programRepository
    )
        : Service<AcademicProgram, int>(createValidator, updateValidator, programRepository),
            IProgramService
    {
        protected readonly IProgramRepository _programRepository = programRepository;

        public async Task<bool> AssignLevels(ProgramLevelsDTO levels, Guid tenantId)
        {
            var levelsList = ConvertLevelsDTOToList(levels, tenantId);
            var result = await _programRepository.AssignLevels(levelsList);
            return result;
        }

        public async Task<bool> AssignSubjects(ProgramSubjectsDTO subjects, Guid tenantId)
        {
            var subjectsList = ConvertSubjectsDTOToList(subjects, tenantId);
            var result = await _programRepository.AssignSubjects(subjectsList);
            return result;
        }

        public async Task<bool> RevokeLevels(ProgramLevelsDTO levels, Guid tenantId)
        {
            var levelsList = ConvertLevelsDTOToList(levels, tenantId);
            var result = await _programRepository.RevokeLevels(levelsList);
            return result;
        }

        public async Task<bool> RevokeSubjects(ProgramSubjectsDTO subjects, Guid tenantId)
        {
            var subjectsList = ConvertSubjectsDTOToList(subjects, tenantId);
            var result = await _programRepository.RevokeSubjects(subjectsList);
            return result;
        }

        private List<ProgramSubject> ConvertSubjectsDTOToList(
            ProgramSubjectsDTO subjects,
            Guid tenantId
        )
        {
            if (subjects.Subjects == null || !subjects.Subjects.Any())
                throw new ArgumentNullException(nameof(subjects), "Empty subjects");

            var list = new List<ProgramSubject>();

            foreach (var subject in subjects.Subjects)
            {
                var auxiliar = new ProgramSubject
                {
                    ProgramId = subjects.ProgramId,
                    SubjectId = subject,
                    TenantId = tenantId,
                };
                list.Add(auxiliar);
            }

            return list;
        }

        private List<ProgramLevel> ConvertLevelsDTOToList(ProgramLevelsDTO levels, Guid tenantId)
        {
            if (levels.Levels == null || !levels.Levels.Any())
                throw new ArgumentNullException(nameof(levels), "Empty levels");

            var list = new List<ProgramLevel>();

            foreach (var subject in levels.Levels)
            {
                var auxiliar = new ProgramLevel
                {
                    ProgramId = levels.ProgramId,
                    LevelId = subject,
                    TenantId = tenantId,
                };
                list.Add(auxiliar);
            }

            return list;
        }
    }
}
