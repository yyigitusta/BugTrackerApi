using BugTrackerApi.Dtos;
using FluentValidation;

namespace BugTrackerApi.Validation;

public class IssueCreateValidator : AbstractValidator<IssueCreateDto>
{
    public IssueCreateValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(200);
        RuleFor(x => x.Body).MaximumLength(4000);
        RuleFor(x => x.ProjectId).GreaterThan(0);
        RuleFor(x => x.Status).IsInEnum();
    }
}

public class IssueUpdateValidator : AbstractValidator<IssueUpdateDto>
{
    public IssueUpdateValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(200);
        RuleFor(x => x.Body).MaximumLength(4000);
        RuleFor(x => x.Status).IsInEnum();
    }
}
