using BugTrackerApi.Dtos;
using FluentValidation;

namespace BugTrackerApi.Validation;

public class ProjectCreateValidator : AbstractValidator<ProjectCreateDto>
{
    public ProjectCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}

public class ProjectUpdateValidator : AbstractValidator<ProjectUpdateDto>
{
    public ProjectUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}
