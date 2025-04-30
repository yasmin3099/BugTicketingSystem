
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUGSystem.BL;

public class ProjectAddDtoValidator : AbstractValidator<ProjectAddDto>
{
    public ProjectAddDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required.")
            .MinimumLength(2)
            .WithMessage("Project name must be at least 2 characters long.")
            .Matches(@"^[a-zA-Z0-9\s]+$")
            .WithMessage("Project name can only contain letters, numbers, and spaces.");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Project description is required.")
            .MinimumLength(2)
            .WithMessage("Project description must be at least 2 characters long.");
    }
}
