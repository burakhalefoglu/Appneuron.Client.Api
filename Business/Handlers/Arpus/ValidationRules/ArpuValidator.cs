using Business.Handlers.Arpus.Commands;
using FluentValidation;

namespace Business.Handlers.Arpus.ValidationRules
{
    public class CreateArpuValidator : AbstractValidator<CreateArpuCommand>
    {
        public CreateArpuValidator()
        {
            RuleFor(x => x.TotalRevenue).NotNull();
            RuleFor(x => x.TotalPlayer).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.DateTime).NotNull();
        }
    }

    public class UpdateArpuValidator : AbstractValidator<UpdateArpuCommand>
    {
        public UpdateArpuValidator()
        {
            RuleFor(x => x.TotalRevenue).NotNull();
            RuleFor(x => x.TotalPlayer).NotNull();
            RuleFor(x => x.ProjectId).NotNull();
            RuleFor(x => x.DateTime).NotNull();
        }
    }
}